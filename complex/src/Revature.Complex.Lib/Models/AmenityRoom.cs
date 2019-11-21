using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public class AmenityRoom
  {
    /// <summary>
    /// AmenityRoom Id of Entity AmenityRoom model
    /// </summary>
    public Guid AmenityRoomId { get; set; }

    /// <summary>
    /// Amenity Id of Entity AmenityRoom model
    /// </summary>
    public Guid AmenityId { get; set; }

    /// <summary>
    /// Room Id of Entity AmenityRoom model
    /// </summary>
    public Guid RoomId { get; set; }
  }
}
