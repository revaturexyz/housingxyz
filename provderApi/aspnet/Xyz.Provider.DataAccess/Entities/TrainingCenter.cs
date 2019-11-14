using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class TrainingCenter
  {
    /// <summary>
    /// Constructs TrainingCenter and initializes navigation properties to default values
    /// </summary>
    public TrainingCenter()
    {
      Provider = new HashSet<Provider>();
    }

    /// <summary>
    /// Primary key of TrainingCenter row
    /// </summary>
    public int CenterId { get; set; }

    /// <summary>
    /// AddressId foreign key cell of TrainingCenter row
    /// </summary>
    public int? AddressId { get; set; }

    /// <summary>
    /// CenterName cell of TrainingCenter row
    /// </summary>
    public string CenterName { get; set; }

    /// <summary>
    /// ContactNumber cell of TrainingCenter row
    /// </summary>
    public string ContactNumber { get; set; }

    public virtual Address Address { get; set; }
    public virtual ICollection<Provider> Provider { get; set; }
  }
}
