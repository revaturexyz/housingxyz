using System;

namespace Revature.Complex.Api.Models
{

  /// <summary>
  /// Api Amenity model, use it as parameter from front-end
  /// or as return type to send back to front-end
  /// </summary>
  public class ApiAmenity
  {
    public Guid AmenityId { get; set; }
    public string AmenityType { get; set; }
    public string Description { get; set; }
  }
}
