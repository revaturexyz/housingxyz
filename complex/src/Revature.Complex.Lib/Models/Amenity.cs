using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic Amenity Model. Use it as parameter to pass into/received from Repository
  /// </summary>
  public class Amenity
  {
    public Guid AmenityId { get; set; }

    public string AmenityType { get; set; }

    public string Description { get; set; }
  }

}
