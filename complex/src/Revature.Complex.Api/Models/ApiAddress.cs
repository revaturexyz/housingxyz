using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Models
{
  public class ApiAddress
  {
    public ApiComplexAddress ComplexAddress { get; set; }

    private Guid AddressGuid;
    public Guid AddressId { get => AddressGuid; set => AddressGuid = new Guid(); }
  }
}
