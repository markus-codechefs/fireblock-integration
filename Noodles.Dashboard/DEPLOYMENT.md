# 🚀 Deploying to Vercel

## Overview
This guide will help you deploy your Blazor Server app to Vercel. While Blazor Server typically requires a persistent connection, we'll use Vercel's serverless functions to make it work.

## Prerequisites
- Vercel account connected to GitHub
- .NET 8.0 SDK installed locally
- GitHub repository with your code

## Step 1: Prepare Your Repository

1. **Push your code to GitHub** (if not already done):
   ```bash
   git add .
   git commit -m "Prepare for Vercel deployment"
   git push origin main
   ```

## Step 2: Connect to Vercel

1. **Go to [Vercel Dashboard](https://vercel.com/dashboard)**
2. **Click "New Project"**
3. **Import your GitHub repository**
4. **Configure the project settings**:
   - **Framework Preset**: Other
   - **Build Command**: `dotnet publish -c Release -o dist`
   - **Output Directory**: `dist`
   - **Install Command**: Leave empty

## Step 3: Environment Variables

Add these environment variables in your Vercel project settings:

### For Production (Real Fireblocks API):
```
FIREBLOCKS_API_KEY=your_api_key_here
FIREBLOCKS_PRIVATE_KEY_PATH=/tmp/private_key.pem
FIREBLOCKS_BASE_URL=https://api.fireblocks.io/
```

### For Development (Mock API):
```
FIREBLOCKS_API_KEY=
FIREBLOCKS_BASE_URL=https://sandbox-api.fireblocks.io/
```

## Step 4: Deploy

1. **Click "Deploy"**
2. **Wait for the build to complete**
3. **Your app will be available at**: `https://your-project-name.vercel.app`

## Step 5: Custom Domain (Optional)

1. **Go to your project settings in Vercel**
2. **Click "Domains"**
3. **Add your custom domain**
4. **Follow the DNS configuration instructions**

## Troubleshooting

### Common Issues:

1. **Build Failures**:
   - Check that all dependencies are properly referenced
   - Ensure .NET 8.0 is specified in the project file

2. **Runtime Errors**:
   - Check environment variables are set correctly
   - Verify Fireblocks API credentials

3. **Performance Issues**:
   - Blazor Server on Vercel may have cold start delays
   - Consider using Blazor WebAssembly for better performance

## Alternative: Blazor WebAssembly

For better performance on Vercel, consider converting to Blazor WebAssembly:

1. **Create new WebAssembly project**:
   ```bash
   dotnet new blazorwasm -n Noodles.Dashboard.Wasm
   ```

2. **Copy your components and services**
3. **Update for client-side rendering**
4. **Deploy as static site**

## Security Notes

- **Never commit API keys** to your repository
- **Use environment variables** for all secrets
- **Enable HTTPS** for production deployments
- **Set up proper CORS** if needed

## Monitoring

- **Use Vercel Analytics** to monitor performance
- **Set up error tracking** with services like Sentry
- **Monitor API usage** with Fireblocks dashboard

## Support

If you encounter issues:
1. Check Vercel deployment logs
2. Review .NET build output
3. Test locally with `dotnet run`
4. Check environment variable configuration 