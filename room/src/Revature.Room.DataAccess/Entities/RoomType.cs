using System.ComponentModel.DataAnnotations;

namespace Revature.Room.DataAccess.Entities
{
  public class RoomType
  {
    [Key]
    public string Type { get; set; }
  }
}
