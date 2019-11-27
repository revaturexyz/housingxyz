using System;

namespace Revature.Tenant.Api.Models
{
  /// <summary>
  /// This defines batch information for a tenant.
  /// Not all tenants will have a batch.
  /// </summary>
  public class ApiBatch
  {
    public int Id { get; set; }
    public string BatchCurriculum { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid TrainingCenter { get; set; }
  }
}
