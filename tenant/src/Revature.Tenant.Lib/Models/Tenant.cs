using System;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Defines a tenant object that has a name and several properties that include their basic information.
  /// </summary>
  public class Tenant
  {
    private int _id;
    private string _email;
    private string _gender;
    private string _firstName;
    private string _lastName;
    private Guid _addressId;
    private int _roomId;
    private int _carId;

    public Car Car { get; set; }

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


    public string Email
    {
      get
      {
        if (_email == null)
        {
          throw new ArgumentException("Email is not set", nameof(_email));
        }
        return _email;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Email must not be empty", nameof(value));
        }

        _email = value;
      }
    }


    public string FirstName
    {
      get
      {
        if (_firstName == null)
        {
          throw new ArgumentException("First name is not set", nameof(_firstName));
        }
        return _firstName;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("First name must not be empty", nameof(value));
        }

        _firstName = value;
      }
    }
    public string LastName
    {
      get
      {
        if (_lastName == null)
        {
          throw new ArgumentException("Last name is not set", nameof(_lastName));
        }

        return _lastName;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Last name must not be empty", nameof(value));
        }

        _lastName = value;
      }
    }


    public int RoomId
    {
      get => _roomId;
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Room Id must not be negative", nameof(value));
        }

        _roomId = value;
      }
    }

    public int CarId
    {
      get => _carId;
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Car Id must not be negative", nameof(value));
        }

        _carId = value;
      }
    }

    public string Gender { get; set; }
    public string FullName { get => FirstName + " " + LastName; }
    public Guid AddressId { get; set; }
  }
}
