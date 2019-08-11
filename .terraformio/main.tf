# housingxyz :: terraform

## BACKENDS
terraform {
  backend "remote" {
    hostname = "${var.terraform_backend["hostname"]}"
    organization = "${var.terraform_backend["organization"]}"

    workspaces {
      name = "${var.terraform_backend["workspace"]}"
    }
  }
}

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
resource "azurerm_app_service" "housingxyz" {
  app_service_plan_id = "${azurerm_app_service_plan.housingxyz.id}"
  https_only = "${var.app_service["https"]}"
  location = "${azurerm_resource_group.housingxyz.location}"
  name = "${var.app_service["name"]}"
  resource_group_name = "${azurerm_resource_group.housingxyz.name}"

  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }

  site_config {
    app_command_line = ""
    linux_fx_version = "${var.app_service["command"]}"
  }
}

resource "azurerm_app_service_custom_hostname_binding" "housingxyz" {
  app_service_name = "${azurerm_app_service.housingxyz.name}"
  hostname = "${var.app_service_custom["hostname"]}"
  resource_group_name = "${azurerm_resource_group.housingxyz.name}"

  depends_on = ["cloudflare_record.housingxyz"]
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

resource "azurerm_resource_group" "housingxyz" {
  name = "${var.az_resource_group["name"]}"
  location = "${var.az_resource_group["location"]}"

  tags = {
    owner = "${var.az_resource_group["owner"]}"
  }
}

resource "cloudflare_record" "housingxyz" {
  domain = "${var.cloudflare_record["domain"]}"
  name = "${var.cloudflare_record["name"]}"
  proxied = "${var.cloudflare_record["proxied"]}"
  ttl = "${var.cloudflare_record["ttl"]}"
  type = "${var.cloudflare_record["type"]}"
  value = "${var.cloudflare_record["value"]}"
}
