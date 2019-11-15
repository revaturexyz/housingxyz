using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiAmenity
  {
    public int AmenityId { get; set; }

    [StringLength(100)]
    public string Amenity { get; set; }
  }
}
