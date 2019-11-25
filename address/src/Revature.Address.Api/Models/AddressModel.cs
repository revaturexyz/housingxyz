using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Address.Api.Models
{

  /// <summary>
  /// This class is model of the Business Library
  /// address object for use in the address controller
  /// </summary>
  public class AddressModel
  {
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Street is required.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Postal Code is required.")]
    [DataType(DataType.PostalCode)]
    [MaxLength(5)]
    public string ZipCode { get; set; }
  }
}
