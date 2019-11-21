using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Revature.Complex.Lib.Models;

namespace Revature.Complex.Api.Models
{
  public class ApiComplex
  {
    /// <summary>
    /// Complex Id of Api Complex model
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// Address object of Api Complex model 
    /// </summary>
    public ApiComplexAddress Address { get; set; }

    /// <summary>
    /// Provider Id of Api Complex model
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Complex name of Api Complex model
    /// </summary>
    [StringLength(100)]
    public string ComplexName { get; set; }

    /// <summary>
    /// Contact number of complex of Api Complex model
    /// </summary>
    [StringLength(20)]
    public string ContactNumber { get; set; }

    /// <summary>
    /// list amenity in complex of Api Complex model
    /// </summary>
    public List<Amenity> ComplexAmentiy { get; set; }
  }
}
