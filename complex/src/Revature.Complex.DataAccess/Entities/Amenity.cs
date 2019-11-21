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

    public virtual ICollection<AmenityRoom> AmenityRoom { get; set; }

    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
