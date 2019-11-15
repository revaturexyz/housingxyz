using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class TrainingCenter
  {
    private string _contactNumber;
    private string _centerName;
    private Address _address;
    private int _centerId;

    public int CenterId
    {
      get => _centerId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value >= 0)
        {
          _centerId = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "ID cannot be less than 0.");
        }
      }
    }

    public Address Address
    {
      get => _address;
      set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string CenterName
    {
      get => _centerName;
      set
      {
        if (!string.IsNullOrWhiteSpace(value))
        {
          _centerName = value.Trim();
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\": Center name cannot be null or empty.", nameof(value));
        }
      }
    }

    public string ContactNumber
    {
      get => _contactNumber;
      set
      {
        if (Regex.IsMatch(value, @"^[0-9]+$"))
        {
          _contactNumber = value;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\": contact number must be a string of only numbers.", nameof(value));
        }
      }
    }

    public ICollection<Provider> Providers { get; set; }
  }
}
