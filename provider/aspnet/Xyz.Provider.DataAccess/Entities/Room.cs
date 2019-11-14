using System;
using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class Room
  {
    /// <summary>
    /// Constructs Room and initializes navigation properties to default values
    /// </summary>
    public Room()
    {
      Notification = new HashSet<Notification>();
      RoomAmenity = new HashSet<RoomAmenity>();
    }

    /// <summary>
    /// Primary key of Room row
    /// </summary>
    public int RoomId { get; set; }

    /// <summary>
    /// ComplexId foreign key cell of Address row
    /// </summary>
    public int ComplexId { get; set; }

    /// <summary>
    /// AddressId foreign key cell of Address row
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// TypeId foreign key cell of Address row
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// GenderId foreign key cell of Address row
    /// </summary>
    public int GenderId { get; set; }

    /// <summary>
    /// RoomNumber cell of Address row
    /// </summary>
    public string RoomNumber { get; set; }

    /// <summary>
    /// NumberOfBeds cell of Address row
    /// </summary>
    public int NumberOfBeds { get; set; }

    /// <summary>
    /// NumberOfOccupants cell of Address row
    /// </summary>
    public int NumberOfOccupants { get; set; }

    /// <summary>
    /// StartDate cell of Address row
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// EndDate cell of Address row (nullable)
    /// </summary>
    public DateTime? EndDate { get; set; }

    public virtual Address Address { get; set; }
    public virtual Complex Complex { get; set; }
    public virtual Gender Gender { get; set; }
    public virtual RoomType Type { get; set; }
    public virtual ICollection<Notification> Notification { get; set; }
    public virtual ICollection<RoomAmenity> RoomAmenity { get; set; }
  }
}
