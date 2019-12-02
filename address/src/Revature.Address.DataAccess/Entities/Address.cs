using System;
using System.ComponentModel.DataAnnotations;


namespace Revature.Address.DataAccess.Entities
{

  /// <summary>
  /// This is an address object for data access purposes,
  /// specifies a guid id for the primary key, as well as a
  /// street, city, state, country, and zip code
  /// </summary>
  public partial class Address
  {
    [Key]
    public Guid? Id { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string ZipCode { get; set; }
  }
}
