using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityRoom
  {
    /// <summary>
    /// Primary key of AmenityRoom table
    /// </summary>
    public Guid AmenityRoomId { get; set; }

    /// <summary>
    /// Foreign key of Amenity table
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Room Id cell of AmenityRoom table
    /// </summary>
    public Guid RoomId { get; set; }

    public Amenity Amenity { get; set; }
  }
}
