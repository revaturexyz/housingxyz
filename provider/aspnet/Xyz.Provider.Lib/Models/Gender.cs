using System;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Gender
  {
    // three genders: male, female and undefined.
    private string _genderType;
    private int _genderId;

    public int GenderId
    {
      get => _genderId;
      set
      // ID 0 is a new object and ID should not be negative
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "Gender ID must not be negative.");
        }
        _genderId = value;
      }
    }

    public string GenderType
    {
      get => _genderType;
      set
      {
        if (value == null || value.Trim().Length <= 0 || !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
        {
          throw new ArgumentException(
            $"Invalid value \"{value}\": Gender must not contain non-ASCII-letters or be empty.", nameof(value));
        }
        _genderType = value;
      }
    }
  }
}
