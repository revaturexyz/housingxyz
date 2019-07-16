# housingxyz :: terraform

## BACKENDS
terraform {
  backend "azurerm" {
    resource_group_name = "revaturexyzgroup"
    storage_account_name = "revaturexyzaccount"
    container_name = "terraformxyz"
    key = "housingxyz.tfstate"
  }
}

## PROVIDERS
provider "azuread" {
  version = "~>0.4.0"
}

provider "azurerm" {
  version = "~>1.30.0"
}

provider "random" {
  version = "~>2.1.0"
}

## RESOURCES
resource "azurerm_resource_group" "housingxyz" {
  name = "housingxyzgroup"
  location = "southcentralus"

  tags = {
    owner = "fred belotte"
  }
}

resource "azurerm_app_service_plan" "housingxyz" {
  kind = "Linux"
  location = "${azurerm_resource_group.housingxyz.location}"
  name = "housingxyzplan"
  resource_group_name = "${azurerm_resource_group.housingxyz.name}"
  reserved = true

  sku {
    size = "B1"
    tier = "Basic"
  }
}

resource "azurerm_app_service" "housingxyz" {
  app_service_plan_id = "${azurerm_app_service_plan.housingxyz.id}"
  location            = "${azurerm_resource_group.housingxyz.location}"
  name                = "housingxyzapp"
  resource_group_name = "${azurerm_resource_group.housingxyz.name}"

  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }

  site_config {
    app_command_line = ""
    linux_fx_version = "COMPOSE|${filebase64("../.docker/dockerup.yaml")}"
  }
}

resource "azurerm_app_service_custom_hostname_binding" "housingxyz" {
  app_service_name = "${azurerm_app_service.housingxyz.name}"
  hostname = "housing.revature.xyz"
  resource_group_name = "${azurerm_resource_group.revaturexyz.name}"
}
