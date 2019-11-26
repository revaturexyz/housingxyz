using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.Models
{
  public class ApiSearchParameters
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Gender { get; set; }

    public string TrainingCenter { get; set; }
  }
}
