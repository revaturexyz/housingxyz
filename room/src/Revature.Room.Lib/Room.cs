using System;
using System.Text.RegularExpressions;

namespace Revature.Room.Lib
{
  /// <summary>
  /// Room class for holding basic information about a Room temporarily
  /// </summary>
  ///<exception cref="ArgumentException"> Thrown when any room constraints have been violated</exception>

  public class Room
  {
    /// <summary>
    /// Number of beds in a Room, can also be interpreted as Room's full capacity
    /// </summary>
    private int _numberOfBeds;

    /// <summary>
    /// Numebr of occupants in a room
    /// </summary>
    private int _numberOfOccupants;

    /// <summary>
    /// End of the room lease
    /// </summary>
    private DateTime _leaseEnd;

    /// <summary>
    /// Start of room lease
    /// </summary>
    private DateTime _leaseStart;

    /// <summary>
    /// Gender of a particular room
    /// </summary>
    private string _gender;
    /// <summary>
    /// Room Number of the room
    /// </summary>
    private string _roomNumber;
    /// <summary>
    /// Room Type, could either be Apartment, Dorm, TownHouse etc
    /// </summary>
    private string _roomType;

    /// <summary>
    /// Unique Identifier for each Room, assigned by complex service
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Another way to uniquely identify a Room, assigned by complex service
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when room number has null or no value</exception>
    public string RoomNumber
    {
      get => _roomNumber;
      set
      {
        if(value == null || Regex.IsMatch(value, @"\s*"))
        {
          throw new ArgumentException("Room Number should have a value");
        }
        _roomNumber = value;
      }
    }

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
    /// <exception cref="ArgumentException">Thrown when the gender is null or has no value just whitespace</exception>
    public string Gender
    {
      get => _gender;
      set
      {
        if(value == null || Regex.IsMatch(value, @"\s*"))
        {
          throw new ArgumentException("Gender must not be empty");
        }
        _gender = value;
      }
    }

    /// <summary>
    /// Type of Room, for example: apartment, dorm, house, etc.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the gender is null or has no value just whitespace</exception>
    public string RoomType
    {
      get => _roomType;
      set
      {
        if (value == null || Regex.IsMatch(value, @"\s*"))
        {
          throw new ArgumentException("Room type must not be empty");
        }
        _roomType = value;
      }
    }

    /// <summary>
    /// Date for the start of the lease, assigned by complex service
    /// </summary>
    public DateTime LeaseStart
    {
      get => _leaseStart;
    }

    /// <summary>
    /// Date for end of lease, assgined by complex service
    /// </summary>
    public DateTime LeaseEnd
    {
      get => _leaseEnd;
    }
    /// <summary>
    /// Method that sets the lease of the room
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <exception cref="ArgumentException">Thrown when lease period is invalid, i.e the lease ends before it even begins</exception>
    public void SetLease(DateTime start, DateTime end)
    {
      if (start.CompareTo(end) >= 0)
      {
        throw new ArgumentException("Lease should start before it ends");
      }
      _leaseEnd = end;
      _leaseStart = start;
    }

    /// <summary>
    /// Complex where Room belongs in, assigned by complex service
    /// </summary>
    public Guid ComplexId { get; set; }
  }
}
