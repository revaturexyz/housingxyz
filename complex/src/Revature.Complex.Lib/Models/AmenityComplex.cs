using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public class AmenityComplex
  {
    /// <summary>
    /// Amenity Complex id of entity AmenityComplex model
    /// </summary>
    public Guid AmenityComplexId { get; set; }

    /// <summary>
    /// Amenity id of entity AmenityComplex model(FK)
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Complex Id of entity AmenityComplex model(FK)
    /// </summary>
    public Guid ComplexId { get; set; }
  }
}
