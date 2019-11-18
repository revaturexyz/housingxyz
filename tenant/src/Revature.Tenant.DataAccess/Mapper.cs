using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess
{
  public class Mapper : IMapper
  {
    public Lib.Models.Tenant MapTenant(Entities.Tenants tenant)
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

        Car = new Lib.Models.Car
        {
          Id = tenant.Cars.Id,
          LicensePlate = tenant.Cars.LicensePlate,
          Make = tenant.Cars.Make,
          Model = tenant.Cars.Model,
          Color = tenant.Cars.Color,
          Year = tenant.Cars.Year,
        },
      };
    }

    public Entities.Tenants MapTenant(Lib.Models.Tenant tenant)
    {
      return new Entities.Tenants
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

        Cars = new Entities.Cars
        {
          Id = tenant.Car.Id,
          LicensePlate = tenant.Car.LicensePlate,
          Make = tenant.Car.Make,
          Model = tenant.Car.Model,
          Color = tenant.Car.Color,
          Year = tenant.Car.Year,
        },
      };
    }
    public Lib.Models.Car MapCar(Entities.Cars car)
    {
      return new Lib.Models.Car
      {
        Id = car.Id,
        LicensePlate = car.LicensePlate,
        Make = car.Make,
        Model = car.Model,
        Color = car.Color,
        Year = car.Year,
      };
    }
    public Entities.Cars MapCar(Lib.Models.Car car)
    {
      return new Entities.Cars
      {
        Id = car.Id,
        LicensePlate = car.LicensePlate,
        Make = car.Make,
        Model = car.Model,
        Color = car.Color,
        Year = car.Year,
      };
    }

    public Entities.Batches MapBatch(Lib.Models.Batch batch)
    {
      return new Entities.Batches
      {
        Id = batch.Id,
        BatchLanguage = batch.BatchLanguage,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate
      };
    }
    public Lib.Models.Batch MapBatch(Entities.Batches batches)
    {
      return new Lib.Models.Batch
      {
        Id = batches.Id,
        BatchLanguage = batches.BatchLanguage,
        StartDate = batches.StartDate,
        EndDate = batches.EndDate
      };
    }
  }
}
