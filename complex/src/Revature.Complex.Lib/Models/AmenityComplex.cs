using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic AmenityComplex Model. Use it as parameter to pass into/received from Repository
  /// This object model defines a complex amenity. It has the amenity name and GUID (id)
  /// </summary>
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid ComplexId { get; set; }
  }
}
