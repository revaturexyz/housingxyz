namespace Xyz.Provider.DataAccess.Entities
{
  public partial class RoomAmenity
  {
    /// <summary>
    /// Primary key of RoomAmenity row
    /// </summary>
    public int RoomAmenityId { get; set; }

    /// <summary>
    /// RoomId foreign key cell of RoomAmenity row
    /// </summary>
    public int RoomId { get; set; }

    /// <summary>
    /// AmenityId foreign key cell of RoomAmenity row
    /// </summary>
    public int AmenityId { get; set; }

    public virtual Amenity Amenity { get; set; }
    public virtual Room Room { get; set; }
  }
}
