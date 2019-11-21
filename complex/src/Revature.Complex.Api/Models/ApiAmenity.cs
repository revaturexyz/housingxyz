using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Models
{
  public class ApiAmenity
  {
    /// <summary>
    /// Amenity Id of Api Amenity model
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Name of Api Amenity model
    /// </summary>
    public string AmenityType { get; set; }

    /// <summary>
    /// Description of Api Amenity model
    /// </summary>
    public string Description { get; set; }
    
  }
}
