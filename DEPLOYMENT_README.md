# Game Library API - Azure Deployment

This project contains the Azure Developer CLI (azd) configuration to deploy the Game Library API to Azure.

## Prerequisites

1. Install [Azure Developer CLI (azd)](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd)
2. Install [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
3. Have an active Azure subscription

## Deployment

### First Time Setup

1. **Initialize the Azure Developer CLI**:
   ```bash
   azd auth login
   ```

2. **Set your SQL Admin Password**:
   - Edit the `.env` file and change `SQL_ADMIN_PASSWORD` to a secure password
   - Or set it as an environment variable:
     ```bash
     $env:SQL_ADMIN_PASSWORD="YourSecurePassword123!"
     ```

3. **Initialize the environment**:
   ```bash
   azd init
   ```

4. **Deploy to Azure**:
   ```bash
   azd up
   ```

   This command will:
   - Create a new resource group
   - Deploy Azure App Service (Free tier)
   - Deploy Azure SQL Server with Basic tier database
   - Deploy your .NET API to App Service
   - Configure connection strings automatically

### Subsequent Deployments

For code updates, you can deploy just the application:
```bash
azd deploy
```

For infrastructure changes, run the full deployment:
```bash
azd up
```

## What Gets Deployed

- **Azure App Service**: Free tier (F1) for hosting the API
- **Azure SQL Server**: With Basic tier database (2GB storage)
- **SQL Database**: Named "GameLibraryDB" with sample data seeding
- **Firewall Rules**: Configured to allow Azure services to access the database

## Environment Variables

The deployment uses these environment variables:

- `AZURE_ENV_NAME`: Environment name (default: "dev")
- `AZURE_LOCATION`: Azure region (default: "southafrica-north")
- `SQL_ADMIN_PASSWORD`: SQL Server admin password (required)

## Accessing Your Deployed API

After deployment, azd will output the URL of your deployed API. You can:

1. Access the Swagger UI at: `https://your-app-name.azurewebsites.net/swagger`
2. Test the API endpoints
3. View the app in the Azure Portal

## Clean Up

To remove all deployed resources:
```bash
azd down
```

## Troubleshooting

- If deployment fails, check the `.env` file has a secure SQL password set
- Ensure you have the necessary Azure permissions to create resources
- Check Azure CLI authentication: `az account show`