using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic Amenity Model. Use it as parameter to pass into/received from Repository
  /// This object model defines a room amenity. It has the amenity name and GUID (id)
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid RoomId { get; set; }
  }
}
