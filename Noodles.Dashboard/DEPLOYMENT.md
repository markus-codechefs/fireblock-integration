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