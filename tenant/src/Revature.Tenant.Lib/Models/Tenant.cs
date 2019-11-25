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
    private int? _carId;
    private int? _batchId;
    private Guid _trainingCenter;

    public Batch Batch { get; set; }
    public Car Car { get; set; }
    public Guid Id
    {
      get => _id;
      set
      {
        if (value == Guid.Empty)
        {
          throw new ArgumentException("Tenant ID must not be empty");
        }

        _id = value;
      }
    }
    public string Email
    {
      get => _email;
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Email must not be null");
        }
        if (value == "")
        {
          throw new ArgumentException("Email must not be empty", nameof(value));
        }
        try
        {
          new System.Net.Mail.MailAddress(value);
        }
        catch (FormatException ex)
        {
          throw new FormatException("Email must be correct format", ex);
        }
        _email = value;
      }
    }
    public string FirstName
    {
      get => _firstName;
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("First name must not be null");
        }
        if (value == "")
        {
          throw new ArgumentException("First name must not be empty", nameof(value));
        }

        _firstName = value;
      }
    }
    public string LastName
    {
      get => _lastName;
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Last name must not be null");
        }
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
      set
      {
        if (value == Guid.Empty)
        {
          throw new ArgumentException("Room Id must not be empty", nameof(value));
        }

        _roomId = value;
      }
    }
    public int? CarId
    {
      get => _carId;
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), "Car Id must not be negative");
        }

        _carId = value;
      }
    }
    public string Gender
    {
      get => _gender;
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Gender must not be null");
        }
        if (value == "")
        {
          throw new ArgumentException("Gender must not be empty", nameof(value));
        }

        _gender = value;
      }
    }
    public string FullName
    {
      get => FirstName + " " + LastName;
    }
    public Guid AddressId
    {
      get => _addressId;
      set => _addressId = value;
    }
    public int? BatchId
    {
      get => _batchId;
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value), "Batch Id must not be negative");
        }

        _batchId = value;
      }
    }
    public Guid TrainingCenter
    {
      get => _trainingCenter;
      set
      {
        if (value == Guid.Empty)
        {
          throw new ArgumentException("Address Id must not be empty", nameof(value));
        }

        _trainingCenter = value;
      }
    }
  }
}
