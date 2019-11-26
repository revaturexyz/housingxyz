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
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
  }
}
