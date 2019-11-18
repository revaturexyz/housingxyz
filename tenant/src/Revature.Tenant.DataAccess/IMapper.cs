namespace Revature.Tenant.Lib.Interface
{
  public interface IMapper
  {
    public Models.Tenant MapTenant(DataAccess.Entities.Tenants tenant);
    public DataAccess.Entities.Tenants MapTenant(Models.Tenant tenant);
    public Models.Car MapCar(DataAccess.Entities.Cars car);
    public DataAccess.Entities.Cars MapCar(Models.Car car);
  }
}
