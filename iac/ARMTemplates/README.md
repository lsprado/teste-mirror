az login

az account show

Conta do Azure
az account set --subscription "3323e547-1651-47e7-a768-6931436e3314"

az account list-locations -o table

$rgName = "rg-ContosoUniversityArmTemplate"

az group create -l brazilsouth -n $rgName

# WebApp
az deployment group create --resource-group $rgName --template-file WebSite.json --parameters WebSite.parameters.json

# API Management
az deployment group create --resource-group $rgName --template-file APIManagement.json --parameters APIManagement.parameters.json