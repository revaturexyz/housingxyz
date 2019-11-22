using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  /// <summary>
  /// Entity AmenityRoom model. Repository use it to CRUD amenity of room data from database
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid RoomId { get; set; }

    /// <summary>
    /// for FK: Amenity Id
    /// </summary>
    public Amenity Amenity { get; set; }
  }
}
