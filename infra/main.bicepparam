using 'main.bicep'

// These values will be automatically read from .env file by azd
param environmentName = readEnvironmentVariable('AZURE_ENV_NAME', 'dev')
param location = readEnvironmentVariable('AZURE_LOCATION', 'southafricanorth')
param sqlAdminPassword = readEnvironmentVariable('SQL_ADMIN_PASSWORD', 'GameLibrary2024!')