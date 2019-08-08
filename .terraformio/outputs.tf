# housingxyz :: terraform-outputs

## OUTPUTS
output "app_service_plan" {
  value = "${azurerm_app_service_plan.housingxyz}"
}

output "resource_group" {
  value = "${azurerm_resource_group.housingxyz}"
}
