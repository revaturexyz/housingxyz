using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This is the complex amenity. Each complex has a separate set of amenities designated by the provider.
  /// </summary>
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid ComplexId { get; set; }
  }
}
