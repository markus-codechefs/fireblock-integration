# 🚀 Deploying Your Blazor Server App to Azure

## 🎯 **Recommended: Azure App Service**

Azure App Service is the **perfect platform** for your Blazor Server app because:
- ✅ **Native .NET support**
- ✅ **Persistent connections** (required for Blazor Server)
- ✅ **Easy GitHub integration**
- ✅ **Free tier available**
- ✅ **Automatic HTTPS**
- ✅ **Global CDN**

## 📁 **Project Structure**

Your project has the following structure:
```
fireblock-integration/
├── Noodles.Dashboard/
│   ├── .deployment          # Azure deployment config
│   ├── web.config           # Azure web server config
│   ├── DEPLOYMENT.md        # This guide
│   └── Noodles.Dashboard/   # Main project directory
│       ├── Noodles.Dashboard.csproj
│       ├── Program.cs
│       ├── Pages/
│       ├── Shared/
│       └── Services/
```

## 🚀 **Step-by-Step Azure Deployment**

### **Prerequisites**
- Azure account (free tier available)
- GitHub repository with your code
- .NET 8.0 SDK (for local testing)

### **Step 1: Create Azure App Service**

1. **Go to [Azure Portal](https://portal.azure.com)**
2. **Click "Create a resource"**
3. **Search for "App Service"**
4. **Click "Create"**

### **Step 2: Configure App Service**

Fill in the basics:
- **Subscription**: Your Azure subscription
- **Resource Group**: Create new (e.g., `noodles-dashboard-rg`)
- **App name**: `noodles-dashboard` (or your preferred name)
- **Publish**: Code
- **Runtime stack**: `.NET 8 (LTS)`
- **Operating System**: Windows
- **Region**: Choose closest to your users

### **Step 3: Plan Configuration**

- **Plan**: Choose "Free F1" for testing
- **SKU and size**: F1 (Free) - 1 GB RAM, 60 minutes/day
- **Click "Review + create"**
- **Click "Create"**

### **Step 4: Deploy from GitHub**

1. **After App Service is created, go to your app**
2. **In the left menu, click "Deployment Center"**
3. **Choose "GitHub" as source**
4. **Authorize Azure to access your GitHub**
5. **Select your repository**: `markus-codechefs/fireblock-integration`
6. **Branch**: `main`
7. **Build Provider**: App Service Build Service
8. **Click "Save"**

### **Step 5: Configure Environment Variables**

1. **Go to "Configuration" in your App Service**
2. **Add these Application settings**:

```
FIREBLOCKS_API_KEY=your_api_key_here
FIREBLOCKS_BASE_URL=https://sandbox-api.fireblocks.io/
```

3. **Click "Save"**

### **Step 6: Deploy and Test**

1. **Go back to "Deployment Center"**
2. **Click "Sync"** to trigger deployment
3. **Wait for build to complete** (2-3 minutes)
4. **Click "Browse"** to test your app

## 🔧 **Azure Configuration Files**

Your project includes these Azure-specific files:

### **`.deployment`** (Root directory)
```
[config]
command = dotnet publish Noodles.Dashboard/Noodles.Dashboard.csproj -c Release -o %DEPLOYMENT_TARGET%
```

### **`web.config`** (Root directory)
- Configures IIS to run your .NET app
- Handles routing and process management

## 🌐 **Your App URL**

Once deployed, your app will be live at:
`https://your-app-name.azurewebsites.net`

## 🔧 **Alternative Hosting Options**

### **Railway (Great Alternative)**
- ✅ **Excellent .NET support**
- ✅ **Simple deployment**
- ✅ **Good free tier**

**Steps:**
1. Go to [Railway](https://railway.app)
2. Connect your GitHub repo
3. Auto-detects .NET
4. Add environment variables
5. Deploy

### **Render**
- ✅ **Supports .NET**
- ✅ **Easy setup**
- ✅ **Free tier available**

**Steps:**
1. Go to [Render](https://render.com)
2. New Web Service
3. Connect GitHub repo
4. Choose .NET runtime
5. Deploy

## ❌ **Not Recommended: Vercel**

**Vercel has limitations for Blazor Server:**
- ❌ **No .NET runtime by default**
- ❌ **Serverless function timeouts**
- ❌ **No persistent connections**
- ❌ **Cold start delays**

## 📊 **Platform Comparison**

| Platform | Blazor Server | Free Tier | Ease | Performance |
|----------|---------------|-----------|------|-------------|
| **Azure** | ✅ Perfect | ✅ Yes | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Railway** | ✅ Great | ✅ Yes | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Render** | ✅ Good | ✅ Yes | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Vercel** | ❌ Limited | ✅ Yes | ⭐⭐⭐ | ⭐⭐ |

## 🔐 **Security Best Practices**

- **Never commit API keys** to your repository
- **Use environment variables** for all secrets
- **HTTPS is automatic** on Azure
- **Enable authentication** if needed

## 🐛 **Troubleshooting**

### **Deployment Issues:**
1. **Check "Deployment Center" logs**
2. **Verify .NET 8.0 runtime is selected**
3. **Ensure environment variables are set**
4. **Check web.config is in the root**
5. **Verify .deployment file points to correct project path**

### **Runtime Issues:**
1. **Check "Log stream" for errors**
2. **Verify Configuration settings**
3. **Test locally with `dotnet run`**

### **Common Errors:**
- **Build fails**: Check .NET version compatibility
- **App won't start**: Verify web.config configuration
- **API errors**: Check environment variables
- **MSB1003 error**: Ensure .deployment file points to correct .csproj path

## 📈 **Monitoring & Scaling**

### **Azure App Service Features:**
- **Application Insights** for monitoring
- **Auto-scaling** based on demand
- **Custom domains** support
- **SSL certificates** management

### **Upgrade Path:**
- **Start with Free F1** for testing
- **Upgrade to Basic B1** for production
- **Scale to Premium** for high traffic

## 🎯 **Next Steps After Deployment**

1. **Test all features** (Portfolio, Onboarding)
2. **Add custom domain** if needed
3. **Set up monitoring** with Application Insights
4. **Configure backup** and disaster recovery
5. **Set up CI/CD** for automatic deployments

## 📞 **Support Resources**

- **Azure Documentation**: https://docs.microsoft.com/azure/app-service/
- **Blazor Documentation**: https://docs.microsoft.com/aspnet/core/blazor/
- **Azure Support**: Available in Azure Portal

## 🎉 **Success Checklist**

- [ ] App Service created successfully
- [ ] GitHub repository connected
- [ ] Environment variables configured
- [ ] Deployment completed
- [ ] App accessible via URL
- [ ] All features working
- [ ] HTTPS working
- [ ] Custom domain (optional)

**Your Blazor Server app is now ready for production on Azure!** 🚀 