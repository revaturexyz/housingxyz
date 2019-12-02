using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Revature.Complex.Lib.Models;

namespace Revature.Complex.Api.Models
{
  /// <summary>
  /// Api Complex model. Use it as parameter from front-end
  /// and/or as a return type to front-end
  /// Also contains a Api ComplexAddress object and list of amenity
  /// </summary>
  public class ApiComplex
  {
    public Guid ComplexId { get; set; }
    public ApiComplexAddress Address { get; set; }
    public Guid ProviderId { get; set; }
    [StringLength(100)]
    public string ComplexName { get; set; }
    [StringLength(20)]
    public string ContactNumber { get; set; }
    public List<Amenity> ComplexAmenity { get; set; }
  }
}
