using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This model serves to connect the Amenity with the Complex that has that amenity. Both FK's
  /// </summary>
  public class AmenityComplex
  {
    [Required]
    public Guid AmenityComplexId { get; set; }

    [Required]
    public Guid AmenityId { get; set; }

    [Required]
    public Guid ComplexId { get; set; }
  }
}
