namespace Revature.Tenant.Api.Models
{
  /// <summary>
  /// Some tenants will arrive to training with cars.
  /// This defines their vehicle information for housing purposes.
  /// Not all tenants will have cars.
  /// </summary>
  public class ApiCar
  {
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Year { get; set; }
    public string State { get; set; }
  }
}
