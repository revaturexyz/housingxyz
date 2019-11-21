using System;

namespace Revature.Complex.Lib.Models
{
  /// <summary>
  /// Logic Amenity Model. Use it as parameter to pass into/received from Repository
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
