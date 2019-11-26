using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// This object model defines a complex. It has the complex name, address, Provider GUID and GUID (id) along with other info.
  /// </summary>
  public partial class Complex
  {
    public Guid ComplexId { get; set; }
    public Guid AddressId { get; set; }
    public Guid ProviderId { get; set; }
    public string ComplexName { get; set; }
    public string ContactNumber { get; set; }
  }
}
