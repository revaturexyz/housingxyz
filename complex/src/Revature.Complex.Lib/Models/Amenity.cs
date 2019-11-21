using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This is the template object for the Amenity. Some amenities are scoped to complexes
  /// and some are scoped to rooms.
  /// </summary>
    public class Amenity
    {
      public Guid AmenityId { get; set; }
      public string AmenityType { get; set; }
      public string Description { get; set; }
    }

}
