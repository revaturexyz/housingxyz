using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Tenant.DataAccess.Entities
{
  class Cars
  {
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Year { get; set; }
  }
}
