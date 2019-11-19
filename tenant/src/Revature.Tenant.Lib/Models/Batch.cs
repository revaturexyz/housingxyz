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
      get
      {
        if (_batchLanguage == null)
        {
          throw new ArgumentNullException("Batch Language is not set", nameof(_batchLanguage));
        }

        return _batchLanguage;
      }
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
      get
      {
        if (_startDate == null)
        {
          throw new ArgumentNullException("Start Date is not set", nameof(_startDate));
        }

        return _startDate;
      }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("Start Date must not be empty", nameof(value));
        }
        _startDate = value;
      }
    }
    public DateTime EndDate

    {
      get
      {
        if (_endDate == null)
        {
          throw new ArgumentNullException("Start Date is not set", nameof(_endDate));
        }

        return _endDate;
      }
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("End Date must not be empty", nameof(value));
        }
        _endDate = value;
      }

    }

  }
}
