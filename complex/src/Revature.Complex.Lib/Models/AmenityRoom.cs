using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic Amenity Model. Use it as parameter to pass into/received from Repository
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }

    public Guid AmenityId { get; set; }

    public Guid RoomId { get; set; }
  }
}
