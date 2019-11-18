using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public class AmenityRoom
  {
    public Guid AmenityRoomId { get; set; }
    public Guid AmenityId { get; set; }
    public Guid RoomId { get; set; }

    public Amenity Amenity { get; set; }
  }
}
