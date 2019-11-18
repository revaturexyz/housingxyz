using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Complex.DataAccess.Entities
{
  public partial class Complex
  {
    /// <summary>
    /// Primary key of Complex row
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// AddressId foreign key cell of Complex row
    /// </summary>
    public Guid AddressId { get; set; }

    /// <summary>
    /// ProviderId foreign key cell of Complex row
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// ComplexName cell of Complex row
    /// </summary>
    public string ComplexName { get; set; }

    /// <summary>
    /// ContactNumber cell of Complex row
    /// </summary>
    public string ContactNumber { get; set; }

    public virtual ICollection<AmenityComplex> AmentityComplex { get; set; }
  }
}
