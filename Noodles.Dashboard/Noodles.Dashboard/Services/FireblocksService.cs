using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Noodles.Dashboard.Configuration;

namespace Noodles.Dashboard.Services
{
    public class FireblocksService
    {
        private readonly HttpClient _httpClient;
        private readonly FireblocksConfig _config;
        private readonly FireblocksJwtService _jwtService;
        private readonly ILogger<FireblocksService> _logger;

        public FireblocksService(
            HttpClient httpClient, 
            FireblocksConfig config, 
            FireblocksJwtService jwtService,
            ILogger<FireblocksService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _jwtService = jwtService;
            _logger = logger;
            
            // Configure base address for Fireblocks API
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task<WalletCreationResult> CreateVaultAccountAsync(string name, string? customerRefId = null)
        {
            try
            {
                var request = new
                {
                    name = name,
                    customerRefId = customerRefId ?? Guid.NewGuid().ToString(),
                    hiddenOnUI = false,
                    autoFuel = false
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Creating vault account: {Name}", name);

                if (_config.UseMockApi || string.IsNullOrEmpty(_config.ApiKey))
                {
                    // Use mock mode
                    await Task.Delay(2000); // Simulate API delay
                    
                    return new WalletCreationResult
                    {
                        Success = true,
                        VaultAccountId = Random.Shared.Next(1, 1000),
                        Name = name,
                        CustomerRefId = request.customerRefId,
                        Message = "Vault account created successfully (Mock Mode)"
                    };
                }

                // Use real API
                var jwt = _jwtService.GenerateJwtToken("/v1/vault/accounts", json);
                if (string.IsNullOrEmpty(jwt))
                {
                    _logger.LogWarning("Failed to generate JWT token, falling back to mock mode");
                    return await CreateVaultAccountAsync(name, customerRefId); // Recursive call with mock
                }

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/v1/vault/accounts")
                {
                    Content = content
                };
                
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var response = await _httpClient.SendAsync(httpRequest);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<FireblocksVaultResponse>(responseContent);
                    return new WalletCreationResult
                    {
                        Success = true,
                        VaultAccountId = result?.Id ?? 0,
                        Name = name,
                        CustomerRefId = request.customerRefId,
                        Message = "Vault account created successfully"
                    };
                }
                else
                {
                    _logger.LogError("Fireblocks API error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                    return new WalletCreationResult
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode} - {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vault account");
                return new WalletCreationResult
                {
                    Success = false,
                    Message = $"Error creating vault account: {ex.Message}"
                };
            }
        }

        public async Task<AssetCreationResult> CreateVaultAssetAsync(int vaultAccountId, string assetId)
        {
            try
            {
                _logger.LogInformation("Creating asset {AssetId} for vault {VaultId}", assetId, vaultAccountId);

                if (_config.UseMockApi || string.IsNullOrEmpty(_config.ApiKey))
                {
                    // Use mock mode
                    await Task.Delay(1500); // Simulate API delay
                    
                    return new AssetCreationResult
                    {
                        Success = true,
                        AssetId = assetId,
                        VaultAccountId = vaultAccountId,
                        Address = GenerateMockAddress(assetId),
                        Message = $"{assetId} asset created successfully (Mock Mode)"
                    };
                }

                // Use real API
                var uri = $"/v1/vault/accounts/{vaultAccountId}/{assetId}";
                var jwt = _jwtService.GenerateJwtToken(uri);
                
                if (string.IsNullOrEmpty(jwt))
                {
                    _logger.LogWarning("Failed to generate JWT token, falling back to mock mode");
                    return await CreateVaultAssetAsync(vaultAccountId, assetId); // Recursive call with mock
                }

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var response = await _httpClient.SendAsync(httpRequest);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<FireblocksAssetResponse>(responseContent);
                    return new AssetCreationResult
                    {
                        Success = true,
                        AssetId = assetId,
                        VaultAccountId = vaultAccountId,
                        Address = result?.Address ?? "",
                        Message = $"{assetId} asset created successfully"
                    };
                }
                else
                {
                    _logger.LogError("Fireblocks API error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                    return new AssetCreationResult
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode} - {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vault asset");
                return new AssetCreationResult
                {
                    Success = false,
                    Message = $"Error creating asset: {ex.Message}"
                };
            }
        }

        private string GenerateMockAddress(string assetId)
        {
            // Generate mock addresses for different assets
            return assetId switch
            {
                "BTC" => "bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh",
                "ETH" => "0x742d35Cc6634C0532925a3b8D4C9db96C4b4d8b6",
                "USDC" => "0x742d35Cc6634C0532925a3b8D4C9db96C4b4d8b6",
                _ => "0x" + Convert.ToHexString(Guid.NewGuid().ToByteArray()).ToLower()[..40]
            };
        }
    }

    public class WalletCreationResult
    {
        public bool Success { get; set; }
        public int VaultAccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerRefId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class AssetCreationResult
    {
        public bool Success { get; set; }
        public string AssetId { get; set; } = string.Empty;
        public int VaultAccountId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    // Fireblocks API response models
    public class FireblocksVaultResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerRefId { get; set; } = string.Empty;
    }

    public class FireblocksAssetResponse
    {
        public string Address { get; set; } = string.Empty;
        public string AssetId { get; set; } = string.Empty;
        public int VaultAccountId { get; set; }
    }
} 