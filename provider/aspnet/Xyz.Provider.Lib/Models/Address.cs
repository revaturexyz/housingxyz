using System;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Address
  {
    // 5 digit zipcode
    private string _zip;

    private string _state;
    private string _city;
    private string _streetAddress;
    private int _addressId;

    public int AddressId
    {
      get => _addressId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value >= 0)
        {
          _addressId = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "ID cannot be less than 0.");
        }
      }
    }

    public string StreetAddress
    {
      get => _streetAddress;
      set
      {
        if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        var trimmed = value.Trim('.', '+', '*', '\'', ' ', '%', '^', '&', '!', '@', '#', '$', '(', ')');
        if (trimmed.Length > 0)
        {
          _streetAddress = trimmed;
        }
        else
        {
          throw new ArgumentException($"\"{value}\" is not a valid street address", nameof(value));
        }
      }
    }

    public string City
    {
      get => _city;
      set
      {
        if (!string.IsNullOrWhiteSpace(value))
        {
          _city = value.Trim();
        }
        else if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\" City name cannot be whitespace.", nameof(value));
        }
      }
    }

    public string State
    {
      get => _state;
      set
      {
        if (value != null && Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
        {
          _state = value;
        }
        else if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else
        {
          throw new ArgumentException(
            $"Invalid value \"{value}\": State name must be a string of only letters and spaces.", nameof(value));
        }
      }
    }

    public string Zip
    {
      get => _zip;
      set
      {
        if (Regex.IsMatch(value, @"^[0-9]+$") && value.Length == 5)
        {
          _zip = value;
        }
        else
        {
          throw new ArgumentException($"\"{value}\" is not a string of 5 integers.", nameof(value));
        }
      }
    }
  }
}
