using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.Lib.Models
{
  public partial class Complex
  {
    /// <summary>
    /// Complex Id of Entity Complex model
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// Address Id of Entity Complex model
    /// </summary>
    public Guid AddressId { get; set; }

    /// <summary>
    /// Provider Id of Entity Complex model
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Complex name of Entity Complex model
    /// </summary>
    public string ComplexName { get; set; }

    /// <summary>
    /// Contact number of Entity Complex model
    /// </summary>
    public string ContactNumber { get; set; }
  }
}
