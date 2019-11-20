using System;

namespace Revature.Tenant.DataAccess.Entities
{
  /// <summary>
  /// This class defines a data access entity tenant.
  /// This is the tenant object we access through our database.
  /// </summary>
  public class Tenants
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AddressId { get; set; }
    public Guid RoomId { get; set; }
    public int CarId { get; set; }
    public virtual Car Car { get; set; }
    public int BatchId { get; set; }
  }
}
