using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class Amenity
  {
    public int AmenityId { get; set; }
    public string AmenityType { get; set; }
    public string Description { get; set; }

    public virtual ICollection<AmenityRoom> AmenityRoom { get; set; }

    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
