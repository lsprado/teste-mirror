// https://github.com/Azure/azure-quickstart-templates/blob/master/quickstarts/microsoft.web/webapp-basic-linux/main.bicep

@description('Describes plan\'s pricing tier and instance size. Check details at https://azure.microsoft.com/en-us/pricing/details/app-service/')
@allowed([
  'F1'
  'D1'
  'B1'
  'B2'
  'B3'
  'S1'
  'S2'
  'S3'
  'P1'
  'P2'
  'P3'
  'P4'
])
param skuName string = 'F1'

@description('Describes plan\'s instance count')
@minValue(1)
@maxValue(3)
param skuCapacity int = 1

@description('Hosting Plan Name')
param hostingPlanName string

@description('WebSite Name')
param websiteName string

@description('WebApi Name')
param webapiName string

@description('Framework Version')
param linuxFxVersion string = 'DOTNETCORE|6.0'

@description('SQL Server Name')
param sqlserverName string

@description('Database Name')
param databaseName string = 'ContosoUniversityDb'

@description('The admin user of the SQL Server')
param sqlAdministratorLogin string = 'leandro'

@description('The password of the admin user of the SQL Server')
@secure()
param sqlAdministratorLoginPassword string = '#P@ssw0rd2022#'

@description('Location for all resources.')
param location string = resourceGroup().location

@description('Workspace Name for Application Insights')
param workspaceName string

@description('Application Insights Name')
param appInsightsName string

resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
  name: sqlserverName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  properties: {
    administratorLogin: sqlAdministratorLogin
    administratorLoginPassword: sqlAdministratorLoginPassword
    version: '12.0'
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
  parent: sqlServer
  name: databaseName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  sku: {
    name: 'Basic'
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 1073741824
  }
}

resource allowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2021-02-01-preview' = {
  parent: sqlServer
  name: 'AllowAllWindowsAzureIps'
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: hostingPlanName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  kind: 'linux'
  sku: {
    name: skuName
    capacity: skuCapacity
  }
  properties: {
    reserved: true
  }
}

resource webSite 'Microsoft.Web/sites@2022-03-01' = {
  name: websiteName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'Recommended'
        }
      ]
    }
    httpsOnly: true
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource websiteHmlSlot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: 'hml'
  parent: webSite
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
    }
  }
}

resource websiteDevSlot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: 'dev'
  parent: webSite
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
    }
  }
}

resource webApi 'Microsoft.Web/sites@2022-03-01' = {
  name: webapiName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'Recommended'
        }
      ]
    }
    httpsOnly: true
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource webapiHmlSlot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: 'hml'
  parent: webApi
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
    }
  }
}

resource webapiDevSlot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: 'dev'
  parent: webApi
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'FtpsOnly'
    }
  }
}

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2020-08-01' = {
  name: workspaceName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 120
    features: {
      searchVersion: 1
      legacy: 0
      enableLogAccessUsingOnlyResourcePermissions: true
    }
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  tags: {
    AppName: 'ContosoUniversity'
  }
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}