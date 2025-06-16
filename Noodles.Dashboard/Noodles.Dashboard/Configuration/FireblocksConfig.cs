namespace Noodles.Dashboard.Configuration
{
    public class FireblocksConfig
    {
        public string ApiKey { get; set; } = string.Empty;
        public string PrivateKeyPath { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://sandbox-api.fireblocks.io/";
        public bool UseMockApi { get; set; } = true; // Default to mock for development
    }
} 