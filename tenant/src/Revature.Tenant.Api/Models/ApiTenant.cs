using System;

namespace Revature.Tenant.Api.Models
{
  /// <summary>
  /// This is a REST Api model for our tenant object.
  /// This is the tenant that will interact with our front-end.
  /// </summary>
  public class ApiTenant
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
