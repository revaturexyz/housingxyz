using System;
using System.Collections.Generic;

namespace Revature.Complex.DataAccess.Entities
{
  /// <summary>
  /// Entity Amenity model. Repository use it to CRUD amenity data from database
  /// </summary>
  public class Amenity
  {

    public Guid AmenityId { get; set; }
    public string AmenityType { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// it is for amenity Id to behave as FK in AmenityRoom table
    /// </summary>
    public virtual ICollection<AmenityRoom> AmenityRoom { get; set; }

    /// <summary>
    /// It is for complex Id behave as FK in AmenityRoom table
    /// </summary>
    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
