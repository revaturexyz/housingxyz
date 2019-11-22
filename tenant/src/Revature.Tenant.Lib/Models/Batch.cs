using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Tenant.Lib.Models
{
  public class Batch
  {
    private string _batchLanguage;
    private int _id;
    private DateTime _startDate;
    private DateTime _endDate;
    private Guid _trainingCenter;

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
        if (value == null)
        {
          throw new ArgumentNullException("Start Date must not be empty", nameof(value));
        }
        _startDate = value;
      }
    }
    public DateTime EndDate

    {
      get => _endDate;
      set
      {
        if (value == null)
        {
          throw new ArgumentNullException("End Date must not be empty", nameof(value));
        }
        _endDate = value;
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
