using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Tenant.Api.Models
{
  /// <summary>
  /// All Tenants must have a permanent address.
  /// This will be used more heavily when the service buss to Address Service exists.
  /// </summary>
  public class ApiAddress
  {
    [Required]
    public Guid AddressId { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public string Country { get; set; }
  }
}
