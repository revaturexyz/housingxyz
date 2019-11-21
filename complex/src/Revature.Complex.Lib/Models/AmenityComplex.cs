using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic AmenityComplex Model. Use it as parameter to pass into/received from Repository
  /// </summary>
  public class AmenityComplex
  {
    public Guid AmenityComplexId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid ComplexId { get; set; }
  }
}
