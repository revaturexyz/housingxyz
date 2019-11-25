using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This model serves to connect the Amenity with the Room that has that amenity. Both are FK's
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid RoomId { get; set; }
  }
}
