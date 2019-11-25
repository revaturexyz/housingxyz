using System;
using System.Collections.Generic;

namespace Revature.Complex.DataAccess.Entities
{
  /// <summary>
  /// Entity Complex model. Repository use it to CRUD complex data from database
  /// </summary>
  public partial class Complex
  {
    public Guid ComplexId { get; set; }

    public Guid AddressId { get; set; }

    public Guid ProviderId { get; set; }

    public string ComplexName { get; set; }

    public string ContactNumber { get; set; }


    /// <summary>
    /// Complex id should behave as FK in AmenityComplex table
    /// </summary>
    public string ContactNumber { get; set; }

    /// <summary>
    /// The Complex model has a collection of Amenityomplex models that
    /// represent the amenities offered by the complex.
    /// </summary>
    public virtual ICollection<AmenityComplex> AmenityComplex { get; set; }
  }
}
