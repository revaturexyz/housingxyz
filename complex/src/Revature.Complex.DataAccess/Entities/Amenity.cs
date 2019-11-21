using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class Amenity
  {
    /// <summary>
    /// Primary key of Amenity table
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Amentity name cell of Amenity table
    /// </summary>
    public string AmenityType { get; set; }

    /// <summary>
    /// Amenity Description cell of Amenity table
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Amenity id should behave as FK in AmenityRoom table
    /// </summary>
    public virtual ICollection<AmenityRoom> AmenityRoom { get; set; }

    /// <summary>
    /// Amenity id should behave as FK in AmenityComplex table
    /// </summary>
    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
