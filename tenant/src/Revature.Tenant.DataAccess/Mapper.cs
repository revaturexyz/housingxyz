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
      };
    }

    public Entities.Tenants MapTenants(Lib.Models.Tenant tenant)
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
      };
    }
    public Lib.Models.Car mapCar(Entities.Cars car)
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
    public Entities.Cars mapCar(Lib.Models.Car car)
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
  }
}
