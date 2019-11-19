using System;

namespace Revature.Tenant.Lib.Models
{
  public class Batch
  {
    private string _batchLanguage;
    private int _id;
    private DateTime _startDate;
    private DateTime _endDate;

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
    public string BatchLanguage
    {
      get => _batchLanguage;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Batch Language Must not be empty", nameof(value));
        }

        _batchLanguage = value;
      }
    }
    public DateTime StartDate
    {
      get => _startDate;
      set
      {
        _startDate = value;
      }
    }
    public DateTime EndDate

    {
      get => _endDate;
      set
      {
        _endDate = value;
      }

    }

  }
}
