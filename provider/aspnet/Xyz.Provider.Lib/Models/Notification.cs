using System;
using System.Text.RegularExpressions;

namespace Xyz.Provider.Lib.Models
{
  public class Notification
  {
    private string _reason;
    private string _title;
    private Room _room;
    private Provider _provider;
    private int _notificationId;

    public int NotificationId
    {
      get => _notificationId;
      set
      // ID 0 is a new object and ID should not be negative
      {
        if (value >= 0)
        {
          _notificationId = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(nameof(value), value, "ID must be greater than or equal to 0");
        }
      }
    }

    public Provider Provider
    {
      get => _provider;
      set => _provider = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Room Room
    {
      get => _room;
      set => _room = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Title
    {
      get => _title;
      set
      {
        if (value != null && value.Trim().Length > 0 && Regex.IsMatch(value, @"^[a-zA-Z\s-0-9]+$"))
        {
          _title = value;
        }
        else
        {
          throw new ArgumentException(
            $"Invalid value \"{value}\": Title must be a string of letters, numbers, hyphens, or spaces.", nameof(value));
        }
      }
    }

    public string Reason
    {
      get => _reason;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          throw new ArgumentException($"Invalid value \"{value}\": Reason cannot be null or empty.", nameof(value));
        }
        _reason = value.Trim();
      }
    }
  }
}
