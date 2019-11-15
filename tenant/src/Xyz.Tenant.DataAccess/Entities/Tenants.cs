using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Tenant.DataAccess.Entities
{
  class Tenants
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set;}
    public int AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
