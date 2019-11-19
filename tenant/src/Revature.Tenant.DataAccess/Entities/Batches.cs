using System;

namespace Revature.Tenant.DataAccess.Entities
{
  /// <summary>
  /// This class defines a data access entity Batch.
  /// This is the batch object we access through our database.
  /// </summary>
  public class Batches
  {
    public int Id { get; set; }
    public string BatchLanguage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
