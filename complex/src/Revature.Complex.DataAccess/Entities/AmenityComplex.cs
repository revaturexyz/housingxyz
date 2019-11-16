using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityComplex
  {
    public int AmenityComplexId { get; set; }
    public int AmenityId { get; set; }
    public Guid ComplexId { get; set; }

    public Complex Complex { get; set; }

    public Amenity Amenity { get; set; }
  }
}
