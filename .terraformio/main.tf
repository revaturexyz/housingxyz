# housingxyz :: terraform

## PROVIDERS
provider "azuread" {
  version = "~>0.4.0"
}

provider "azurerm" {
  version = "~>1.30.0"
}

provider "cloudflare" {
  version = "~>1.16.0"
}

## RESOURCES
resource "azurerm_resource_group" "housingxyz" {
  name = "${var.az_resource_group["name"]}"
  location = "${var.az_resource_group["location"]}"

  tags = {
    owner = "${var.az_resource_group["owner"]}"
  }
}

resource "azurerm_app_service_plan" "housingxyz" {
  kind = "${var.az_app_service_plan["kind"]}"
  location = "${azurerm_resource_group.revaturexyz.location}"
  name = "${var.az_app_service_plan["name"]}"
  resource_group_name = "${azurerm_resource_group.revaturexyz.name}"
  reserved = "${var.az_app_service_plan["reserved"]}"

  sku {
    size = "${var.az_app_service_plan["size"]}"
    tier = "${var.az_app_service_plan["tier"]}"
  }
}
