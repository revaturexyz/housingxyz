using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.Models
{
  public class ApiBatch
  {
    public int Id { get; set; }
    public string BatchLanguage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid TrainingCenter { get; set; }
  }
}
