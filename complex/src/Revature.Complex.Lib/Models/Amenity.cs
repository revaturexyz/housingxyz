using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This object defines an amenity. It has the amenity name and GUID (id).
  /// Some amenities are scoped to a particular room, while others apply to a whole complex.
  /// </summary>
  public class Amenity
    {
      public Guid AmenityId { get; set; }
      public string AmenityType { get; set; }
      public string Description { get; set; }
    }

}
