# 🚀 GitHub Actions Deployment to Azure

## Overview
Since SCM basic authentication is disabled on your Azure App Service, we'll use GitHub Actions for deployment instead.

## Step 1: Get Azure Publish Profile

1. **Go to your Azure App Service**
2. **Click "Get publish profile"** (download button)
3. **Save the file** - you'll need its contents

## Step 2: Add GitHub Secrets

1. **Go to your GitHub repository**
2. **Click "Settings"** → **"Secrets and variables"** → **"Actions"**
3. **Click "New repository secret"**
4. **Add these secrets:**

### **AZURE_APP_NAME**
- **Name**: `AZURE_APP_NAME`
- **Value**: Your Azure App Service name (e.g., `noodles-dashboard`)

### **AZURE_PUBLISH_PROFILE**
- **Name**: `AZURE_PUBLISH_PROFILE`
- **Value**: Copy the entire content of the publish profile file you downloaded

## Step 3: Configure Azure App Service

1. **In Azure Portal, go to your App Service**
2. **Click "Configuration"**
3. **Add these Application settings:**

```
FIREBLOCKS_API_KEY=your_api_key_here
FIREBLOCKS_BASE_URL=https://sandbox-api.fireblocks.io/
```

4. **Click "Save"**

## Step 4: Deploy

1. **Push any change to main branch**
2. **GitHub Actions will automatically:**
   - Build your .NET app
   - Deploy to Azure App Service
3. **Check the Actions tab** in GitHub to monitor deployment

## Step 5: Verify Deployment

1. **Go to your Azure App Service**
2. **Click "Browse"** to see your app
3. **Test the Portfolio and Onboarding pages**

## Troubleshooting

### **If deployment fails:**
1. **Check GitHub Actions logs** in the Actions tab
2. **Verify secrets are set correctly**
3. **Ensure Azure App Service name matches**
4. **Check environment variables in Azure**

### **Common Issues:**
- **Secret not found**: Make sure secrets are added to GitHub
- **Build fails**: Check .NET version compatibility
- **Deploy fails**: Verify publish profile content

## Benefits of GitHub Actions

- ✅ **No SCM authentication issues**
- ✅ **Better build logs**
- ✅ **More control over build process**
- ✅ **Automatic deployments on push**
- ✅ **Rollback capabilities**

Your app will now deploy automatically whenever you push to main! 🎉 