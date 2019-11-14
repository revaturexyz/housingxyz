using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Complex
  {
    private int _complexId;

    // object references rather than object IDs
    private Address _address;
    private TrainingCenter _center;
    private Provider _provider;

    private string _complexName;
    private string _contactNumber;

    public int ComplexId
    {
      get => _complexId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Complex ID must not be negative.");
        }
        _complexId = value;
      }
    }

    public Address Address
    {
      get => _address;
      set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TrainingCenter Center
    {
      get => _center;
      set => _center = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Provider Provider
    {
      get => _provider;
      set => _provider = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string ComplexName
    {
      get => _complexName;
      set
      {
        if (value != null && value.Trim().Length > 0)
        {
          _complexName = value;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\" for complex name.", nameof(value));
        }
      }
    }

    public string ContactNumber
    {
      get => _contactNumber;
      set
      {
        if (value != null && value.Trim().Length > 0 && Regex.IsMatch(value, @"^[0-9-+]+$"))
        {
          _contactNumber = value;
        }
        else
        {
          throw new ArgumentException(
            "Invalid value \"{value}\": Contact number must not be empty or invalid characters.", nameof(value));
        }
      }
    }

    public ICollection<Room> Rooms { get; set; }
  }
}
