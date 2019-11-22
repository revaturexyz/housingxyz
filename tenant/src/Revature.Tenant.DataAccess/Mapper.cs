using Revature.Tenant.Lib.Interface;
using System;

namespace Revature.Tenant.DataAccess
{

  public class Mapper : IMapper
  {
    /// <summary>
    /// Map a Model Tenant from a Entity Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Entity who may have a nested Car Model and/or a Batch Model</param>
    /// <returns>A Tenant Model who may have a nested Car Model and/or a Batch Model</returns>
    public Lib.Models.Tenant MapTenant(Entities.Tenant tenant)
    {
      Lib.Models.Batch batch;
      int? batchId;
      if (tenant.Batch != null)
      {
        batch = new Lib.Models.Batch
        {
          Id = tenant.Batch.Id,
          BatchCurriculum = tenant.Batch.BatchCurriculum,
          TrainingCenter = tenant.Batch.TrainingCenter
        };
        batch.SetStartAndEndDate(tenant.Batch.StartDate, tenant.Batch.EndDate);
        batchId = tenant.BatchId;
      }
      else
      {
        batch = null;
        batchId = null;
      }

      Lib.Models.Car car;
      int? carId;
      if (tenant.Car != null)
      {
        car = new Lib.Models.Car()
        {
          Id = tenant.Car.Id,
          LicensePlate = tenant.Car.LicensePlate,
          Make = tenant.Car.Make,
          Model = tenant.Car.Model,
          Color = tenant.Car.Color,
          Year = tenant.Car.Year,
          State = tenant.Car.State
        };
        carId = tenant.CarId;
      }
      else
      {
        car = null;
        carId = null;
      }


      return new Lib.Models.Tenant
      {
        Id = tenant.Id,
        Email = tenant.Email,
        Gender = tenant.Gender,
        FirstName = tenant.FirstName,
        LastName = tenant.LastName,
        AddressId = tenant.AddressId,
        RoomId = tenant.RoomId,
        CarId = carId,
        BatchId = batchId,
        TrainingCenter = tenant.TrainingCenter,

        Car = car,
        Batch = batch
      };
    }

    /// <summary>
    /// Map a Entity Tenant from a Model Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Model who may have a nested Car Model and/or a Batch Model</param>
    /// <returns>A Tenant Entity who may have a nested Car Model and/or a Batch Model</returns>
    public Entities.Tenant MapTenant(Lib.Models.Tenant tenant)
    {
      return new Entities.Tenant
      {
        Id = tenant.Id,
        Email = tenant.Email,
        Gender = tenant.Gender,
        FirstName = tenant.FirstName,
        LastName = tenant.LastName,
        AddressId = tenant.AddressId,
        RoomId = tenant.RoomId,
        CarId = tenant.CarId,
        BatchId = tenant.BatchId,
        TrainingCenter = tenant.TrainingCenter,

        Car = new Entities.Car
        {
          Id = tenant.Car.Id,
          LicensePlate = tenant.Car.LicensePlate,
          Make = tenant.Car.Make,
          Model = tenant.Car.Model,
          Color = tenant.Car.Color,
          Year = tenant.Car.Year,
          State = tenant.Car.State
        },

        Batch = new Entities.Batch
        {
          Id = tenant.Batch.Id,
          BatchCurriculum = tenant.Batch.BatchCurriculum,
          StartDate = tenant.Batch.StartDate,
          EndDate = tenant.Batch.EndDate,
          TrainingCenter = tenant.Batch.TrainingCenter
        }
      };
    }

    /// <summary>
    /// Map a Model Car from a Entity Car
    /// </summary>
    /// <param name="car">A Car Entity</param>
    /// <returns>A Car Model</returns>
    public Lib.Models.Car MapCar(Entities.Car car)
    {
      return new Lib.Models.Car
      {
        Id = car.Id,
        LicensePlate = car.LicensePlate,
        Make = car.Make,
        Model = car.Model,
        Color = car.Color,
        Year = car.Year,
        State = car.State
      };
    }

    /// <summary>
    /// Map a Entity Car from a Model Car
    /// </summary>
    /// <param name="car">A Car Model</param>
    /// <returns>A Car Entity</returns>
    public Entities.Car MapCar(Lib.Models.Car car)
    {
      return new Entities.Car
      {
        Id = car.Id,
        LicensePlate = car.LicensePlate,
        Make = car.Make,
        Model = car.Model,
        Color = car.Color,
        Year = car.Year,
        State = car.State
      };
    }

    /// <summary>
    /// Map a Model Batch from a Entity Batch
    /// </summary>
    /// <param name="batch">A Batch Entity</param>
    /// <returns>A Batch Model</returns>
    /// <exception cref="System.ArgumentException">Thrown when start date is after end date</exception>
    public Entities.Batch MapBatch(Lib.Models.Batch batch)
    {
      //checks that start date is before end date
      if (batch.StartDate.CompareTo(batch.EndDate) >= 0)
      {
        throw new ArgumentException($"Start date must be before end date. Start Date: {batch.StartDate}, End Date: {batch.EndDate}");
      }

      return new Entities.Batch
      {
        Id = batch.Id,
        BatchCurriculum = batch.BatchCurriculum,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate,
        TrainingCenter = batch.TrainingCenter
      };
    }

    /// <summary>
    /// Map a Entity Batch from a Model Batch
    /// </summary>
    /// <param name="batch">A Batch Model</param>
    /// <returns>A Batch Entity</returns>
    /// <exception cref="System.ArgumentException">Thrown when start date is after end date</exception>

    public Lib.Models.Batch MapBatch(Entities.Batch batch)
    {
      var newBatch = new Lib.Models.Batch()
      {
        Id = batch.Id,
        BatchCurriculum = batch.BatchCurriculum,
        TrainingCenter = batch.TrainingCenter
      };
      newBatch.SetStartAndEndDate(batch.StartDate, batch.EndDate);
      return newBatch;
    }
  }
}
