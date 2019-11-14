using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class Complex
  {
    /// <summary>
    /// Constructs Complex and initializes navigation properties to default values
    /// </summary>
    public Complex()
    {
      Room = new HashSet<Room>();
    }

    /// <summary>
    /// Primary key of Complex row
    /// </summary>
    public int ComplexId { get; set; }

    /// <summary>
    /// AddressId foreign key cell of Complex row
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// ProviderId foreign key cell of Complex row
    /// </summary>
    public int ProviderId { get; set; }

    /// <summary>
    /// ComplexName cell of Complex row
    /// </summary>
    public string ComplexName { get; set; }

    /// <summary>
    /// ContactNumber cell of Complex row
    /// </summary>
    public string ContactNumber { get; set; }

    public virtual Address Address { get; set; }
    public virtual Provider Provider { get; set; }
    public virtual ICollection<Room> Room { get; set; }
  }
}
