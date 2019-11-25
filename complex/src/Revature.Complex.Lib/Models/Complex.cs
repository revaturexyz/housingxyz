using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
<<<<<<< Updated upstream
=======
  /// <summary>
  /// This model repesents the complex. It holds the GUID of it's provider (FK-provider service) and
  /// Address(FK- Address service).
  /// It's ComplexId is held as ComplexId(FK) in AmenityComplex.
  /// </summary>
>>>>>>> Stashed changes
  public partial class Complex
  {
    public Guid ComplexId { get; set; }
    public Guid AddressId { get; set; }
    public Guid ProviderId { get; set; }
    public string ComplexName { get; set; }
    public string ContactNumber { get; set; }
  }
}
