using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
<<<<<<< Updated upstream
    public class Amenity
    {
      public Guid AmenityId { get; set; }
      public string AmenityType { get; set; }
      public string Description { get; set; }
    }
=======
  /// <summary>
  /// This model represents an amenity.
  /// AmenityRoom and AmenityComplex show the sets of amenities that rooms and complexes have.
  /// It only has a type and description.
  /// </summary>
  public class Amenity
  {
    public Guid AmenityId { get; set; }
    public string AmenityType { get; set; }
    public string Description { get; set; }
  }
>>>>>>> Stashed changes

}
