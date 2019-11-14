using System;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class RoomType
  {
    private string _type;
    private int _typeId;

    public int TypeId
    {
      get => _typeId;
      // ID 0 is a new object and ID should not be negative
      set
      {
        if (value >= 0)
        {
          _typeId = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "ID must not be negative.");
        }
      }
    }

    public string Type
    {
      get => _type;
      set
      {
        if (value is null)
        {
          throw new ArgumentNullException(nameof(value));
        }
        var trimmed = value.Trim();
        if (trimmed.Length > 0 && Regex.IsMatch(value, @"^[a-zA-Z\s-]+$"))
        {
          _type = trimmed;
        }
        else
        {
          throw new ArgumentException($"Invalid value \"{value}\": doesn't follow the format.", nameof(value));
        }
      }
    }
  }
}
