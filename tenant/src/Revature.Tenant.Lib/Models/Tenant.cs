using System;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Defines a tenant object that has a name and several properties that include their basic information.
  /// </summary>
  public class Tenant
  {
    private Guid _id;
    private string _email;
    private string _gender;
    private string _firstName;
    private string _lastName;
    private Guid _addressId;
    private Guid _roomId;
    private int _carId;
    private int _batchId;

    public Car Car { get; set; }

    public Guid Id
    {
      get => _id;
      set => _id = value;
      
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


    public Guid RoomId
    {
      get => _roomId;
      set => _roomId = value;
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

    public string Gender
    {
      get
      {
        if (_gender == null)
        {
          throw new ArgumentException("Gender is not set", nameof(_gender));
        }

        return _gender;
      }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Gender Must not be empty", nameof(value));
        }

        _gender = value;
      }
    }
    public string FullName { get => FirstName + " " + LastName; }
    public Guid AddressId { get => _addressId; set => _addressId = value; }
    public int BatchId
    {
      get => _batchId;
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Batch Id must not be negative", nameof(value));
        }

        _batchId = value;
      }
    }

  }
}
