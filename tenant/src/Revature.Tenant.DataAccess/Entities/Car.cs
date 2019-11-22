using System.Collections.Generic;

namespace Revature.Tenant.DataAccess.Entities
{
  /// <summary>
  /// This class defines a data access entity car.
  /// This is the car object we access through our database.
  /// Not all entity tenants will have cars.
  /// </summary>
  public class Car
  {
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Year { get; set; }
    public string State { get; set; }

    public virtual Tenant Tenant { get; set; }
  }
}
