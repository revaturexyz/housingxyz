using System;
using System.Collections.Generic;

namespace Revature.Tenant.DataAccess.Entities
{
  /// <summary>
  /// This class defines a data access entity Batch.
  /// This is the batch object we access through our database.
  /// </summary>
  public class Batch
  {
    public Batch()
    {
      Tenant = new HashSet<Tenant>();
    }
    public int Id { get; set; }
    public string BatchLanguage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<Tenant> Tenant { get; set; }
  }
}
