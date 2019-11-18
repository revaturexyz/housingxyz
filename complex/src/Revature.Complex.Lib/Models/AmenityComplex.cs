using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid ComplexId { get; set; }
  }
}
