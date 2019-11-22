using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.Models
{
  public class ApiCar
  {
    /// <summary>
    /// Some tenants will arrive to training with cars.
    /// This defines their vehicle information for housing purposes.
    /// Not all tenants will have cars.
    /// </summary>
    public int Id { get; set; }
    public string LicensePlate { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public string Year { get; set; }

  }
}
