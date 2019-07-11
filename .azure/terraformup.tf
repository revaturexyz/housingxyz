# housingxyz :: terraform

## providers
provider "azuread" {
  version = "~>0.4.0"
}

provider "azurerm" {
  version = "~>1.30.0"
}

provider "random" {
  version = "~>2.1.0"
}

## variables
variable "az_locations" {
  type = "map"
  default = {
    main = "southcentralus"
  }
}

## resource groups
resource "azurerm_resource_group" "az_housingxyz" {
  name = "housingxyz-group"
  location = "${var.az_locations["main"]}"
}
