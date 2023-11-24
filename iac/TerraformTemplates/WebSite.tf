terraform {
    required_version = ">= 0.11" 
    backend "azurerm" {
        storage_account_name = "__terraformstorageaccount__"
        container_name       = "terraform"
        key                  = "terraform.tfstate"
	    access_key  ="__storagekey__"
        features{}
	}
}

variable "ResourceName" {
  type = string
  default = "RG_WebAppDemo"
}

# Configure the Azure provider
provider "azurerm" { 
    # The "feature" block is required for AzureRM provider 2.x. 
    # If you are using version 1.x, the "features" block is not allowed.
    version = "~>2.0"
    features {}
}

resource "azurerm_resource_group" "WebAppDemo" {
    name = var.ResourceName
    location = "brazilsouth"
}

resource "azurerm_app_service_plan" "WebAppDemo" {
    name                = "PlanWebAppDemoTerraForm"
    location            = azurerm_resource_group.WebAppDemo.location
    resource_group_name = azurerm_resource_group.WebAppDemo.name
    sku {
        tier = "Standard"
        size = "S2"
    }
}

resource "azurerm_app_service" "WebAppDemo" {
    name                = "MyWebAppDemoTerraForm"
    location            = azurerm_resource_group.WebAppDemo.location
    resource_group_name = azurerm_resource_group.WebAppDemo.name
    app_service_plan_id = azurerm_app_service_plan.WebAppDemo.id
}

resource "azurerm_app_service_slot" "WebAppDemoStaging" {
    name                = "Staging"
    location            = azurerm_resource_group.WebAppDemo.location
    resource_group_name = azurerm_resource_group.WebAppDemo.name
    app_service_plan_id = azurerm_app_service_plan.WebAppDemo.id
    app_service_name    = azurerm_app_service.WebAppDemo.name
}