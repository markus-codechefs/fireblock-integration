using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Noodles.Dashboard.Configuration;

namespace Noodles.Dashboard.Services
{
    public class FireblocksJwtService
    {
        private readonly FireblocksConfig _config;
        private readonly ILogger<FireblocksJwtService> _logger;

        public FireblocksJwtService(FireblocksConfig config, ILogger<FireblocksJwtService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GenerateJwtToken(string uri, string requestBody = "")
        {
            try
            {
                if (string.IsNullOrEmpty(_config.ApiKey) || string.IsNullOrEmpty(_config.PrivateKeyPath))
                {
                    _logger.LogWarning("Fireblocks API credentials not configured, using mock mode");
                    return string.Empty;
                }

                var privateKey = LoadPrivateKey();
                if (privateKey == null)
                {
                    _logger.LogError("Failed to load private key from {PrivateKeyPath}", _config.PrivateKeyPath);
                    return string.Empty;
                }

                var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var nonce = now;

                var payload = new Dictionary<string, object>
                {
                    ["uri"] = uri,
                    ["nonce"] = nonce,
                    ["iat"] = now,
                    ["exp"] = now + 55, // Token expires in 55 seconds
                    ["sub"] = _config.ApiKey,
                    ["bodyHash"] = ComputeSha256Hash(requestBody)
                };

                var header = new Dictionary<string, object>
                {
                    ["alg"] = "RS256",
                    ["typ"] = "JWT"
                };

                var jwt = new JwtSecurityToken(
                    claims: payload.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)),
                    signingCredentials: new SigningCredentials(
                        new RsaSecurityKey(privateKey),
                        SecurityAlgorithms.RsaSha256
                    )
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating JWT token");
                return string.Empty;
            }
        }

        private RSA? LoadPrivateKey()
        {
            try
            {
                if (!File.Exists(_config.PrivateKeyPath))
                {
                    _logger.LogError("Private key file not found at {PrivateKeyPath}", _config.PrivateKeyPath);
                    return null;
                }

                var privateKeyPem = File.ReadAllText(_config.PrivateKeyPath);
                var rsa = RSA.Create();

                // Remove PEM headers and decode
                var privateKeyBase64 = privateKeyPem
                    .Replace("-----BEGIN PRIVATE KEY-----", "")
                    .Replace("-----END PRIVATE KEY-----", "")
                    .Replace("\n", "")
                    .Replace("\r", "");

                var privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

                return rsa;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading private key");
                return null;
            }
        }

        private string ComputeSha256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("")));

            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
} 