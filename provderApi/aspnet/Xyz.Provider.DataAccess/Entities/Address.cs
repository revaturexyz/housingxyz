using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  /// <summary>
  /// Class for representing rows in Address table, configured in RevatureHousingDbContext
  /// </summary>
  public partial class Address
  {
    /// <summary>
    /// Constructs Address and initializes navigation properties to default values
    /// </summary>
    public Address()
    {
      Complex = new HashSet<Complex>();
      Provider = new HashSet<Provider>();
      Room = new HashSet<Room>();
      TrainingCenter = new HashSet<TrainingCenter>();
    }

    /// <summary>
    /// Primary key of Address row
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// StreetAddress cell of Address row
    /// </summary>
    public string StreetAddress { get; set; }

    /// <summary>
    /// City cell of Address row
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// State cell of Address row
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// ZipCode cell of Address row
    /// </summary>
    public string Zip { get; set; }

    /// <summary>
    /// Navigation property for Complex rows w/ this AddressId
    /// </summary>
    public virtual ICollection<Complex> Complex { get; set; }

    /// <summary>
    /// Navigation property for Provider rows w/ this AddressId
    /// </summary>
    public virtual ICollection<Provider> Provider { get; set; }

    /// <summary>
    /// Navigation property for Room rows w/ this AddressId
    /// </summary>
    public virtual ICollection<Room> Room { get; set; }

    /// <summary>
    /// Navigation property for TrainingCenter rows w/ this AddressId
    /// </summary>
    public virtual ICollection<TrainingCenter> TrainingCenter { get; set; }
  }
}
