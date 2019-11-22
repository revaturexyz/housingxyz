using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This object model defines a complex amenity. It has the amenity name and GUID (id)
  /// </summary>
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid ComplexId { get; set; }
  }
}
