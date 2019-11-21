using System;

namespace Revature.Complex.Lib.Models
{
  public partial class Complex
  {
    public Guid ComplexId { get; set; }
    public Guid AddressId { get; set; }
    public Guid ProviderId { get; set; }
    public string ComplexName { get; set; }
    public string ContactNumber { get; set; }
  }
}
