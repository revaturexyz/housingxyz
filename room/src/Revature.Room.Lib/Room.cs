using System;

namespace Revature.Room.Lib
{
  /// <summary>
  /// Room class for holding basic information about a room temporarily
  /// </summary>
  public class Room
  {
    /// <summary>
    /// Number of beds in a room, can also be interpreted as room's full capacity
    /// </summary>
    private int _numberOfBeds;

    /// <summary>
    /// Unique Identifier for each room, assigned by complex service
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Another way to uniquely identify a room, assigned by complex service
    /// </summary>
    public string RoomNumber { get; set; }

    /// <summary>
    /// Logic for checking that the Number of Beds is greater than zero
    /// Server side validation
    /// </summary>
    public int NumberOfBeds
    {
      get => _numberOfBeds;
      set
      {
        if (value > 0) _numberOfBeds = value;
      }
    }

    /// <summary>
    /// Number of occupants per room, used to check for room vacancy, updated whenever a tenant is assigned or leaves a room
    /// </summary>
    public int NumberOfOccupants { get; set; }

    /// <summary>
    /// Gender of the room, when assigning a tenant to a room, their roommates should be of the same gender
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Type of room, for example: apartment, dorm, house, etc.
    /// </summary>
    public string RoomType { get; set; }

    /// <summary>
    /// Date for the start of the lease, assigned by complex service
    /// </summary>
    public DateTime LeaseStart { get; set; }

    /// <summary>
    /// Date for end of lease, assgined by complex service
    /// </summary>
    public DateTime LeaseEnd { get; set; }

    /// <summary>
    /// Complex where room belongs in, assigned by complex service
    /// </summary>
    public Guid ComplexId { get; set; }
  }
}
