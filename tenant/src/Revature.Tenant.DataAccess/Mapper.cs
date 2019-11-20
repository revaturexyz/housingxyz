using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess
{
  public class Mapper : IMapper
  {
    public Lib.Models.Tenant MapTenant(Entities.Tenant tenant)
    {
      return new Lib.Models.Tenant
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

        Car = new Lib.Models.Car
        {
          Id = tenant.Car.Id,
          LicensePlate = tenant.Car.LicensePlate,
          Make = tenant.Car.Make,
          Model = tenant.Car.Model,
          Color = tenant.Car.Color,
          Year = tenant.Car.Year,
          State = tenant.Car.State
        },

        Batch = new Lib.Models.Batch
        {
          Id = tenant.Batch.Id,
          BatchLanguage = tenant.Batch.BatchLanguage,
          StartDate = tenant.Batch.StartDate,
          EndDate = tenant.Batch.EndDate,
          TrainingCenter = tenant.Batch.TrainingCenter
        }
      };
    }

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
          BatchLanguage = tenant.Batch.BatchLanguage,
          StartDate = tenant.Batch.StartDate,
          EndDate = tenant.Batch.EndDate,
          TrainingCenter = tenant.Batch.TrainingCenter
        }
      };
    }
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

    public Entities.Batch MapBatch(Lib.Models.Batch batch)
    {
      return new Entities.Batch
      {
        Id = batch.Id,
        BatchLanguage = batch.BatchLanguage,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate,
        TrainingCenter = batch.TrainingCenter
      };
    }
    public Lib.Models.Batch MapBatch(Entities.Batch batch)
    {
      return new Lib.Models.Batch
      {
        Id = batch.Id,
        BatchLanguage = batch.BatchLanguage,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate,
        TrainingCenter = batch.TrainingCenter
      };
    }
  }
}
