using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public class Amenity
  {
    /// <summary>
    /// Amenity Id of Entity Amenity model
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// name of amenity of Entity Amenity model
    /// </summary>
    public string AmenityType { get; set; }

    /// <summary>
    /// desciption of amenity of Entity Amenity model
    /// </summary>
    public string Description { get; set; }
  }

}
