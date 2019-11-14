using System;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Amenity
  {
    // a single amenity
    private string _amenity;
    private int _amenityId;

    public int AmenityId
    {
      get => _amenityId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Amenity ID must be greater than or equal to zero.");
        }
        _amenityId = value;
      }
    }

    public string AmenityType
    {
      get => _amenity;
      set
      {
        if (value != null && value.Trim().Length > 0 && Regex.IsMatch(value, @"^[0-9a-zA-Z-,/\s]+$"))
        {
          _amenity = value;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\" for amenity type.", nameof(value));
        }
      }
    }
  }
}
