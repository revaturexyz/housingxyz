using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityComplex
  {

    /// <summary>
    /// Primary key of AmenityComplex table
    /// </summary>
    public Guid AmenityComplexId { get; set; }

    /// <summary>
    /// Foreign key of Amenity table
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Foreign key of Complex table
    /// </summary>
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
