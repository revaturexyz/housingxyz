using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
<<<<<<< Updated upstream
=======
  /// <summary>
  /// This model serves to connect the Amenity with the Complex that has that amenity. Both FK's
  /// </summary>
>>>>>>> Stashed changes
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid ComplexId { get; set; }
  }
}
