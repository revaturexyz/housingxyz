using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This is the room amenity. Each room has a separate set of amenities designated by the provider.
  /// </summary>
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid RoomId { get; set; }
  }
}
