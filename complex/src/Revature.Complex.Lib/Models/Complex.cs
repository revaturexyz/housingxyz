using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This model repesents the complex. It holds the GUID of it's provider (FK-provider service) and
  /// Address(FK- Address service).
  /// It's ComplexId is held as ComplexId(FK) in AmenityComplex.
  /// </summary>
  public partial class Complex
  {
    [Required]
    public Guid ComplexId { get; set; }

    [Required]
    public Guid AddressId { get; set; }

    [Required]
    public Guid ProviderId { get; set; }

    [Required, MaxLength(50)]
    public string ComplexName { get; set; }

    [MaxLength(20)]
    public string ContactNumber { get; set; }
  }
}
