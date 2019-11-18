using System;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Some tenants will arrive to training with cars.
  /// This defines their vehicle information for housing purposes.
  /// Not all tenants will have cars.
  /// </summary>
  public class Car
  {
    private int _id;
    private string _licensePlate;
    private string _make;
    private string _model;
    private string _color;
    private string _year;

    public int Id
    {
      get => _id;
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Id must not be negative", nameof(value));
        }

        _id = value;
      }
    }

    public string LicensePlate
    {
      get
      {
        if (_licensePlate == null)
        {
          throw new ArgumentException("License plate is not set", nameof(_licensePlate));
        }
        return _licensePlate;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("License plate must not be empty", nameof(value));
        }

        _licensePlate = value;
      }
    }

    public string Make
    {
      get
      {
        if (_make == null)
        {
          throw new ArgumentException("Make is not set", nameof(_make));
        }
        return _make;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Make must not be empty", nameof(value));
        }

        _make = value;
      }
    }

    public string Model
    {
      get
      {
        if (_model == null)
        {
          throw new ArgumentException("Model is not set", nameof(_model));
        }

        return _model;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Model must not be empty", nameof(value));
        }

        _model = value;
      }
    }
    public string Color
    {
      get
      {
        if (_color == null)
        {
          throw new ArgumentException("Color is not set", nameof(_color));
        }

        return _color;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Color must not be empty", nameof(value));
        }

        _color = value;
      }
    }
    public string Year
    {
      get
      {
        if (_year == null)
        {
          throw new ArgumentException("Year is not set", nameof(_year));
        }

        return _year;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Year must not be empty", nameof(value));
        }

        _year = value;
      }
    }
  }
}
