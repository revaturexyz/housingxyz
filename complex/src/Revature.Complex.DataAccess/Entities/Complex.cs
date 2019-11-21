using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public partial class Complex
  {
    /// <summary>
    /// Primary key of Complex table
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// Address Id cell of Complex table
    /// </summary>
    public Guid AddressId { get; set; }

    /// <summary>
    /// Provider Id cell of Complex table
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Complex name cell of Complex table
    /// </summary>
    public string ComplexName { get; set; }

    /// <summary>
    /// Contact number cell of Complex table
    /// </summary>
    public string ContactNumber { get; set; }

    public virtual ICollection<AmenityComplex> AmentityComplex { get; set; }
  }
}
