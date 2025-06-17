# Fireblocks Integration Dashboard

A modern Blazor Server application for managing cryptocurrency portfolios with Fireblocks integration.

## Features

- 📊 **Portfolio Dashboard** - Real-time portfolio overview
- 🚀 **Onboarding** - Easy wallet creation
- 🔐 **Fireblocks Integration** - Secure crypto management
- 📈 **Modern UI** - Beautiful, responsive design

## Deployment

This app is deployed on Azure App Service using GitHub Actions with OIDC authentication.

### Live Demo
Visit: https://bitcoin-concierge-bbhufygyf4dvdngp.westeurope-01.azurewebsites.net

## Local Development

```bash
cd Noodles.Dashboard
dotnet run
```

Visit `http://localhost:5126` to see the app running locally.

## GitHub Actions

This repository uses GitHub Actions for automated deployment to Azure App Service with:
- OIDC authentication using Azure service principal
- Windows hosting for optimal .NET performance
- Automated builds and deployments on push to main branch

## Project Structure

```
fireblock-integration/
├── .github/workflows/     # GitHub Actions deployment
├── Noodles.Dashboard/     # Blazor Server application
└── README.md             # This file
```
