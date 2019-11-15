using System;

namespace Revature.Tenant.Api.Models
{
  public class ApiTenant
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public Guid Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
