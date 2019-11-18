using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid ComplexId { get; set; }

    public Complex Complex { get; set; }

    public Amenity Amenity { get; set; }
  }
}
