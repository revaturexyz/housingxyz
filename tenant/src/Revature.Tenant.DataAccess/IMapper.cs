namespace Revature.Tenant.Lib.Interface
{
  public interface IMapper
  {
    public Models.Tenant MapTenant(DataAccess.Entities.Tenant tenant);
    public DataAccess.Entities.Tenant MapTenant(Models.Tenant tenant);
    public Models.Car MapCar(DataAccess.Entities.Car car);
    public DataAccess.Entities.Car MapCar(Models.Car car);
  }
}
