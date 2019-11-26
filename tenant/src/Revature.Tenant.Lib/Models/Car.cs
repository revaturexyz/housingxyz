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
    private string _state;

    public int Id
    {
      get => _id;
      set
      {
        _id = value;
      }
    }

    public string LicensePlate
    {
      get => _licensePlate;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("License plate must not be empty");
        }

        _licensePlate = value;
      }
    }

    public string Make
    {
      get => _make;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Make must not be empty");
        }

        _make = value;
      }
    }

    public string Model
    {
      get => _model;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Model must not be empty");
        }

        _model = value;
      }
    }
    public string Color
    {
      get => _color;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Color must not be empty");
        }

        _color = value;
      }
    }
    public string Year
    {
      get => _year;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Year must not be empty");
        }

        _year = value;
      }
    }

    public string State
    {
      get => _state;
      set {
        if (value == "")
        {
          throw new ArgumentException("State must not be empty");
        }

        _state = value;
      }
    }
  }
}
