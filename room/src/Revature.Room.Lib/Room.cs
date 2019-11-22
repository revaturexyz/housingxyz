using System;

namespace Revature.Room.Lib
{
  /// <summary>
  /// Room class for holding basic information about a Room temporarily
  /// </summary>
  public class Room
  {
    /// <summary>
    /// Number of beds in a Room, can also be interpreted as Room's full capacity
    /// </summary>
    private int _numberOfBeds;
    private int _numberOfOccupants;

    /// <summary>
    /// Unique Identifier for each Room, assigned by complex service
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Another way to uniquely identify a Room, assigned by complex service
    /// </summary>
    public string RoomNumber { get; set; }

    /// <summary>
    /// Logic for checking that the Number of Beds is greater than zero
    /// Server side validation
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when given an invalid number of beds</exception>
    public int NumberOfBeds
    {
      get => _numberOfBeds;
      set
      {
        if (value > 0) _numberOfBeds = value;
        else throw new ArgumentException("Number of beds must be greater than zero");
      }
    }

    /// <summary>
    /// Number of occupants per Room, used to check for Room vacancy, updated whenever a tenant is assigned or leaves a Room
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when occupants are greater than number number of beds</exception>
    public int NumberOfOccupants
    {
      get => _numberOfOccupants;
      set
      {
        if (value > _numberOfBeds || value < 0) throw new ArgumentException("Number of Occupants must be less than number of beds");
        else _numberOfOccupants = value;
      }
    }

    /// <summary>
    /// Gender of the Room, when assigning a tenant to a Room, their roommates should be of the same gender
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Type of Room, for example: apartment, dorm, house, etc.
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
    /// Complex where Room belongs in, assigned by complex service
    /// </summary>
    public Guid ComplexId { get; set; }
  }
}
