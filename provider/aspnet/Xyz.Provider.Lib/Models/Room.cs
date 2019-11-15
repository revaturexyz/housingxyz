using System;
using System.Collections.Generic;

namespace Xyz.Provider.Lib.Models
{
  public class Room
  {
    private int _roomId;

    // object references rather than object IDs
    private Complex _complex;
    private Address _address;

    // room type: dorm, apartment....
    private RoomType _roomType;

    private Gender _gender;
    private string _roomNumber;
    private int _numberOfBeds;
    private int _numberOfOccupants;
    private DateTime _startDate;
    private DateTime? _endDate;

    public int RoomId
    {
      get => _roomId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Room ID must not be negative.");
        }
        _roomId = value;
      }
    }

    public Complex Complex
    {
      get => _complex;
      set => _complex = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Address Address
    {
      get => _address;
      set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public RoomType RoomType
    {
      get => _roomType;
      set => _roomType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Gender Gender
    {
      get => _gender;
      set
      {
        if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else if (NumberOfOccupants > 0)
        {
          throw new InvalidOperationException("Room has been already occupied. Cannot change the gender.");
        }
        _gender = value;
      }
    }

    public string RoomNumber
    {
      get => _roomNumber;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          throw new ArgumentException($"Invalid value \"{value}\": Room number must not be null or just white space.", nameof(value));
        }
        var trimmed = value.Trim('.', '+', '*', '\'', ' ', '%', '^', '&', '!', '@', '#', '$', '(', ')');
        if (value.Length > 10 || trimmed.Length == 0)
        {
          throw new ArgumentException(
            $"Invalid value \"{value}\": Room number must not be longer than 10 characters or lacking alphanumeric content.",
            nameof(value));
        }
        _roomNumber = trimmed;
      }
    }

    public int NumberOfBeds
    {
      get => _numberOfBeds;
      set
      {
        if (value <= 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Number of beds must not be negative or zero.");
        }
        _numberOfBeds = value;
      }
    }

    public int NumberOfOccupants
    {
      get => _numberOfOccupants;
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Number of occupants cannot be less than 0.");
        }
        else if (value > NumberOfBeds)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value,
            $"Number of occupants cannot exceed number of beds ({NumberOfBeds}).");
        }
        _numberOfOccupants = value;
      }
    }

    public bool HasAmenity { get; set; }

    public DateTime StartDate
    {
      get => _startDate;
      set
      {
        if (value >= _endDate)
        {
          throw new ArgumentException($"Invalid value {value}: Start date cannot be later than or the same as end date.", nameof(value));
        }
        _startDate = value;
      }
    }

    public DateTime? EndDate
    {
      get => _endDate;
      set
      {
        if (value <= _startDate)
        {
          throw new ArgumentException("End date cannot be earlier than or the same as start date", nameof(value));
        }
        else
        {
          _endDate = value;
        }
      }
    }

    public ICollection<Amenity> Amenities { get; set; }
  }
}
