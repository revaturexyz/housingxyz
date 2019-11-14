using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  /// <summary>
  /// Class for representing rows in Amenity table, configured in RevatureHousingDbContext
  /// </summary>
  public partial class Amenity
  {
    /// <summary>
    /// Constructs Amenity and initializes navigation properties to default values
    /// </summary>
    public Amenity()
    {
      RoomAmenity = new HashSet<RoomAmenity>();
    }

    /// <summary>
    /// Primary key of Amenity row
    /// </summary>
    public int AmenityId { get; set; }

    /// <summary>
    /// AmenityType cell of Amenity row
    /// </summary>
    public string AmenityType { get; set; }

    public virtual ICollection<RoomAmenity> RoomAmenity { get; set; }
  }
}
