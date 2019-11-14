using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Provider
  {
    private string _password;
    private string _username;
    private string _companyName;
    private string _contactNumber;
    private TrainingCenter _center;
    private int _providerId;
    private Address _address;

    public int ProviderId
    {
      get => _providerId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value >= 0)
        {
          _providerId = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "ID cannot be less than 0.");
        }
      }
    }

    public TrainingCenter Center
    {
      get => _center;
      set => _center = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Username
    {
      // string of letters numbers or special characters 
      get => _username;
      set
      {
        if (Regex.IsMatch(value, @"^[a-zA-Z0-9_!@#\$%\^&\*]+$"))
        {
          _username = value;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\" for username: invalid characters.", nameof(value));
        }
      }
    }

    public string CompanyName
    {
      get => _companyName;
      set
      {
        if (!string.IsNullOrWhiteSpace(value))
        {
          _companyName = value;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\": company name cannot be null or empty.", nameof(value));
        }
      }
    }

    public string ContactNumber
    {
      get => _contactNumber;
      set
      {
        if (Regex.IsMatch(value, @"^[0-9-]+$"))
        {
          _contactNumber = value;
        }
        else
        {
          throw new ArgumentException(
            $"Invalid value \"{value}\": contact number must be a string of numbers and dashes.", nameof(value));
        }
      }
    }

    public Address Address
    {
      get => _address;
      set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
      get => _password;
      // requires lowercase and uppercase letters, numbers and special characters
      set
      {
        if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})"))
        {
          _password = value;
        }
        else
        {
          throw new ArgumentException(
            $"\"{value}\" is not a valid password: " +
            $"must be at least 8 characters and contain lowercase and uppercase letters, numbers and special characters.",
            nameof(value));
        }
      }
    }

    public ICollection<Complex> Complexes { get; set; }
  }
}
