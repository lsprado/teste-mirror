# Deployment Bicep File

´´´ps
$rgName = 'RG_ContosoExample'
$location = 'brazilsouth'

az bicep upgrade

az account list-locations -o table

az webapp list-runtimes --os-type linux

az group create --name $rgName --location $location

az group list -o table

az deployment group create `
  --name ContosoExample `
  --resource-group $rgName `
  --template-file .\ContosoUniversity.AzureResourceGroup\Bicep\main.bicep `
  --parameters hostingPlanName=plan-ContosoExample skuName='S1' websiteName=app-ContosoExample webapiName=api-ContosoExample linuxFxVersion='"DOTNETCORE|6.0"' sqlserverName=sql-ContosoExample databaseName=db-ContosoExample sqlAdministratorLogin=leandro sqlAdministratorLoginPassword=#P@ssw0rd2022# workspaceName=log-ContosoExample appInsightsName=appi-ContosoExample `
  --debug
´´´