using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Tenant.Lib.Interface
{
  public interface IMapper
  {
    public Models.Tenant MapTenant(DataAccess.Entities.Tenants tenant);
    public DataAccess.Entities.Tenants MapTenant(Models.Tenant tenant);
    public Models.Car mapCar(DataAccess.Entities.Cars car);
    public DataAccess.Entities.Cars mapCar(Models.Car car);
  }
}
