using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Models
{
  public class ApiComplex
  {
    public ApiComplexAddress Address { get; set; }
    public List<string> ComplexAmentiy { get; set; }

    [StringLength(100)]
    public string ComplexName { get; set; }

    [StringLength(20)]
    public string ContactNumber { get; set; }

    public Guid ProviderID { get; set; }
  }
}
