using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  /// <summary>
  /// Entity AmenityRoom model. Repository use it to CRUD complex data from database
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }

    public Guid AmenityId { get; set; }

    /// <summary>
    /// This is a Fk representing a room in the Room service
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// for FK: amenity Id
    /// </summary>
    public Amenity Amenity { get; set; }
  }
}
