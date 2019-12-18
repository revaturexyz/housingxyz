using System;

namespace Revature.Tenant.Api.Models
{
  /// <summary>
  /// This is a REST Api model for our tenant object.
  /// This is the tenant that will interact with our front-end.
  /// </summary>
  public class ApiTenant
  {
    public Guid? Id { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid? AddressId { get; set; }
    public Guid? RoomId { get; set; }
    public int? CarId { get; set; }
    public int? BatchId { get; set; }
    public Guid TrainingCenter { get; set; }
    public ApiBatch ApiBatch { get; set; }
    public ApiCar ApiCar { get; set; }
    public ApiAddress ApiAddress { get; set; }
  }
}
