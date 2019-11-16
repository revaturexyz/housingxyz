using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public class AmenityComplex
  {
    public int AmenityComplexId { get; set; }
    public int AmenityId { get; set; }
    public Guid ComplexId { get; set; }
  }
}
