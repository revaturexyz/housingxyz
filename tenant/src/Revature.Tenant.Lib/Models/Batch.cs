using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Tenants will arrive to training with batches.
  /// This defines their batch details.
  /// </summary>
  public class Batch
  {
    private string _batchCurriculum;
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
    public string BatchCurriculum
    {
      get => _batchCurriculum;
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Batch Curriculum must not be empty", nameof(value));
        }

        _batchCurriculum = value;
      }
    }

    public DateTime StartDate
    {
      get => _startDate;
    }
    public DateTime EndDate

    {
      get => _endDate;
    }

    /// <summary>
    /// This is a Set method that checks that the start date is earlier than the end date before allow the backing field to be set.
    /// </summary>
    /// <param name="startDate">The value of the start date</param>
    /// <param name="endDate">The value of the end date</param>
    /// <returns>true if start date is before end date and _startdate and _enddate are set, else false</returns>
    public void SetStartAndEndDate(DateTime startDate, DateTime endDate)
    {
      //Checks if start date is after end date
      if(startDate.CompareTo(endDate) >= 0)
      {
        throw new ArgumentException($"Start date must be before End date. Start Date: {startDate}, End Date: {endDate}");
      }
      _startDate = startDate;
      _endDate = endDate;

    }

    public Guid TrainingCenter
    {
      get => _trainingCenter;
      set
      {
        if (value == Guid.Empty)
        {
          throw new ArgumentException("Batch Id must not be empty", nameof(value));
        }

        _trainingCenter = value;
      }
    }
  }
}
