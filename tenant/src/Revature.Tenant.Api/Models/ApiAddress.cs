using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.Models
{
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
