using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Revature.Address.Lib
{
  public class Address
  {

    private Guid _id { get; set; }
    private string _street { get; set; }
    private string _city { get; set; }
    private string _state { get; set; }
    private string _country { get; set; }
    private string _zipCode { get; set; }

    public Guid Id
    {
      get => _id;
      set
      {
        if (value == null)
          throw new ArgumentException("Id must be set", nameof(value));

        _id = value;
      }
    }
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
          throw new ArgumentException($"Invalid value \"{value}\" City name cannot be whitespace.", nameof(value));
        }
      }
    }


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
