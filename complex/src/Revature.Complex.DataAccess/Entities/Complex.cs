using System;
using System.Collections.Generic;

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

    /// <summary>
    /// The Complex model has a collection of Amenityomplex models that
    /// represent the amenities offered by the complex.
    /// </summary>
    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
