using System;
using System.Text.RegularExpressions;

namespace Revature.Address.Lib
{

  /// <summary>
  /// This is an address object for business logic purposes,
  /// specifies a guid id as well as a street,
  /// city, state, country, and zip code
  /// </summary>
  public class Address
  {

    private string _street { get; set; }
    private string _city { get; set; }
    private string _state { get; set; }
    private string _country { get; set; }
    private string _zipCode { get; set; }

    /// <summary>
    /// Guid for identifying addresses
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Specifies that a street must not be null and
    /// not whitespace or a series of special characters
    /// </summary>
    public string Street
    {
      get => _street;
      set
      {
        if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        var trimmed = value.Trim('.', '+', '*', '\'', ' ', '%', '^', '&', '!', '@', '#', '$', '(', ')');
        if (trimmed.Length > 0)
        {
          _street = trimmed;
        }
        else
        {
          throw new ArgumentException($"\"{value}\" is not a valid street address", nameof(value));
        }
      }
    }

    /// <summary>
    /// Specifies that a city must not be null or whitespace
    /// </summary>
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

    /// <summary>
    /// Specifies that a state must not be null and
    /// can only be a series of letters and spaces
    /// </summary>
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

    /// <summary>
    /// Specifies that a country must not be null or whitespace
    /// </summary>
    public string Country
    {
      get => _country;
      set
      {
        if (!string.IsNullOrWhiteSpace(value))
        {
          _country = value.Trim();
        }
        else if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\" Country name cannot be whitespace.", nameof(value));
        }
      }
    }

    /// <summary>
    /// Specifies that a zip code must be a string of 5 integers
    /// </summary>
    public string ZipCode
    {
      get => _zipCode;
      set
      {
        if (Regex.IsMatch(value, @"^[0-9]+$") && value.Length == 5)
        {
          _zipCode = value;
        }
        else
        {
          throw new ArgumentException($"\"{value}\" is not a string of 5 integers.", nameof(value));
        }
      }
    }
  }
}
