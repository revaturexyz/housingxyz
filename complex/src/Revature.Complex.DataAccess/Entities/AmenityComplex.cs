using System;

namespace Revature.Complex.DataAccess.Entities
{
  /// <summary>
  /// Entity AmenityComplex model. Repository use it to CRUD amenity of complex data from database
  /// </summary>
  public class AmenityComplex
  {

    public Guid AmenityComplexId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid ComplexId { get; set; }

    /// <summary>
    /// for FK: complex Id
    /// </summary>
    public Complex Complex { get; set; }

    /// <summary>
    /// for FK: amenity Id
    /// </summary>
    public Amenity Amenity { get; set; }
  }
}
