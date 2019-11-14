using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiAddress
  {
    public int AddressId { get; set; }

    [MaxLength(256), MinLength(6)]
    public string StreetAddress { get; set; }

    [MaxLength(60), MinLength(4)]
    public string City { get; set; }

    [MaxLength(60), MinLength(2)]
    public string State { get; set; }

    [MaxLength(5), MinLength(5), RegularExpression(@"^[0-9]+$", ErrorMessage = "Non-digits not allowed")]
    public string ZipCode { get; set; }
  }
}
