#!/bin/bash

# Exit on any error
set -e

echo "🚀 Starting .NET build for Vercel..."

# Install .NET 8.0 SDK
echo "📦 Installing .NET 8.0 SDK..."
wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Verify .NET installation
echo "✅ .NET version:"
dotnet --version

# Restore dependencies
echo "📚 Restoring dependencies..."
dotnet restore

# Build the project
echo "🔨 Building project..."
dotnet build -c Release --no-restore

# Publish the project
echo "📦 Publishing to dist directory..."
dotnet publish -c Release -o dist --no-build

echo "✅ Build completed successfully!" 