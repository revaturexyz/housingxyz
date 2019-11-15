using System.Collections.Generic;

namespace Revature.Tenant.DataAccess.Entities
{
  public class Cars
  {
    public Cars()
    {
      Tenants = new HashSet<Tenants>();
    }
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Year { get; set; }

    public virtual ICollection<Tenants> Tenants { get; set; }
  }
}
