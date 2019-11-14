using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xyz.Tenant.Api.Models
{
  public class ApiTenant
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
