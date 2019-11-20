using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This object defines an amenity. It have the amenity name and GUID (id).
  /// </summary>
    public class Amenity
    {
      public Guid AmenityId { get; set; }
      public string AmenityType { get; set; }
      public string Description { get; set; }
    }

}
