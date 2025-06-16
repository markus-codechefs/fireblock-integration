# Fireblocks API Setup Guide

## Environment Variables Configuration

To use the real Fireblocks API, you need to set the following environment variables:

### Required Variables

```bash
# Your Fireblocks API Key (from Fireblocks Console)
export FIREBLOCKS_API_KEY="your-api-key-here"

# Path to your private key file (downloaded from Fireblocks Console)
export FIREBLOCKS_PRIVATE_KEY_PATH="/path/to/your/private-key.pem"

# Fireblocks API Base URL
export FIREBLOCKS_BASE_URL="https://sandbox-api.fireblocks.io/"
```

### Optional Variables

```bash
# Set to false to use real API, true for mock mode
export FIREBLOCKS_USE_MOCK_API="false"
```

## Getting Fireblocks API Credentials

1. **Sign up for Fireblocks**: Go to [fireblocks.com](https://fireblocks.com) and create an account
2. **Access the Console**: Log into your Fireblocks Console
3. **Generate API Key**: 
   - Go to Settings → API Keys
   - Click "Generate New API Key"
   - Save the API Key ID
4. **Download Private Key**:
   - Download the private key file (.pem format)
   - Store it securely on your server
   - Set the path in `FIREBLOCKS_PRIVATE_KEY_PATH`

## Development vs Production

### Development (Mock Mode)
- No API credentials needed
- Uses simulated responses
- Perfect for testing and development

### Production (Real API)
- Requires valid Fireblocks API credentials
- Makes real API calls to Fireblocks
- Use sandbox environment for testing
- Use production environment for live transactions

## Security Best Practices

1. **Never commit API keys to source control**
2. **Use environment variables for all sensitive data**
3. **Store private key files securely**
4. **Use different API keys for development and production**
5. **Regularly rotate API keys**

## Testing the Integration

1. Set the environment variables
2. Restart the application
3. Go to the Onboarding page
4. Create a wallet - you should see "(Mock Mode)" or real API responses

## Troubleshooting

- **"Mock Mode" messages**: API credentials not configured
- **JWT errors**: Check private key file path and format
- **API errors**: Verify API key and permissions in Fireblocks Console 