using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityRoom
  {
    /// <summary>
    /// Primary key of AmenityRoom table.
    /// This is a foereign key to the RoomId Guid of the room service.
    /// </summary>
    public Guid AmenityRoomId { get; set; }

    /// <summary>
    /// Foreign key of Amenity table
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// This is a Fk representing a room in the Room service
    /// </summary>
    public Guid RoomId { get; set; }

    public Amenity Amenity { get; set; }
  }
}
