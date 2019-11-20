using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess
{
  public class Mapper : IMapper
  {
    /// <summary>
    /// Map a Model Tenant from a Entity Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Entity</param>
    /// <returns>A Tenant Model</returns>
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
          Id = tenant.Car.Id,
          LicensePlate = tenant.Car.LicensePlate,
          Make = tenant.Car.Make,
          Model = tenant.Car.Model,
          Color = tenant.Car.Color,
          Year = tenant.Car.Year,
          State = tenant.Car.State
        },
      };
    }

    /// <summary>
    /// Map a Entity Tenant from a Model Tenant
    /// </summary>
    /// <param name="tenant">A Tenant Model</param>
    /// <returns>A Tenant Entity</returns>
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
    public Lib.Models.Batch MapBatch(Entities.Batch batch)
    {
      return new Lib.Models.Batch
      {
        Id = batch.Id,
        BatchLanguage = batch.BatchLanguage,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate
      };
    }

    /// <summary>
    /// Map a Entity Batch from a Model Batch
    /// </summary>
    /// <param name="batch">A Batch Model</param>
    /// <returns>A Batch Entity</returns>
    public Entities.Batch MapBatch(Lib.Models.Batch batch)
    {
      return new Entities.Batch
      {
        Id = batch.Id,
        BatchLanguage = batch.BatchLanguage,
        StartDate = batch.StartDate,
        EndDate = batch.EndDate
      };
    }
  }
}
