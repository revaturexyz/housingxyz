using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class RoomType
  {
    /// <summary>
    /// Constructs RoomType and initializes navigation property to default value
    /// </summary>
    public RoomType()
    {
      Room = new HashSet<Room>();
    }

    /// <summary>
    /// Primary key of RoomType row
    /// </summary>
    [Key]
    public int TypeId { get; set; }

    /// <summary>
    /// Type cell of RoomType row
    /// </summary>
    public string Type { get; set; }

    public virtual ICollection<Room> Room { get; set; }
  }
}
