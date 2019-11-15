# addressxyz :: terraform

## BACKENDS
terraform {
  backend "remote" {}
}

## PROVIDERS
provider "azuread" {
  version = "~>0.6.0"
}

provider "azurerm" {
  version = "~>1.30.0"
}

provider "cloudflare" {
  version = "~>1.16.0"
}

## RESOURCES
resource "azurerm_app_service" "addressxyz" {
  app_service_plan_id = "${azurerm_app_service_plan.addressxyz.id}"
  https_only = "${var.app_service["https"]}"
  location = "${azurerm_resource_group.addressxyz.location}"
  name = "${var.app_service["name"]}"
  resource_group_name = "${azurerm_resource_group.addressxyz.name}"

  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }

  site_config {
    app_command_line = ""
    linux_fx_version = "COMPOSE|${filebase64(var.app_service["linux"])}"
  }
}

resource "azurerm_app_service_custom_hostname_binding" "addressxyz" {
  app_service_name = "${azurerm_app_service.addressxyz.name}"
  hostname = "${var.app_service_custom["hostname"]}"
  resource_group_name = "${azurerm_resource_group.addressxyz.name}"

  depends_on = ["cloudflare_record.addressxyz"]
}

resource "azurerm_app_service_plan" "addressxyz" {
  kind = "${var.app_service_plan["kind"]}"
  location = "${azurerm_resource_group.addressxyz.location}"
  name = "${var.app_service_plan["name"]}"
  resource_group_name = "${azurerm_resource_group.addressxyz.name}"
  reserved = "${var.app_service_plan["reserved"]}"

  sku {
    size = "${var.app_service_plan["size"]}"
    tier = "${var.app_service_plan["tier"]}"
  }
}

resource "azurerm_resource_group" "addressxyz" {
  name = "${var.resource_group["name"]}"
  location = "${var.resource_group["location"]}"

  tags = {
    owner = "${var.resource_group["owner"]}"
  }
}

resource "cloudflare_record" "addressxyz" {
  domain = "${var.cloudflare_record["domain"]}"
  name = "${var.cloudflare_record["name"]}"
  proxied = "${var.cloudflare_record["proxied"]}"
  ttl = "${var.cloudflare_record["ttl"]}"
  type = "${var.cloudflare_record["type"]}"
  value = "${var.cloudflare_record["value"]}"
}
