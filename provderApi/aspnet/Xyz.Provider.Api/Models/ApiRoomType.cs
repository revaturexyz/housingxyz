using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiRoomType
  {
    public int TypeId { get; set; }

    [StringLength(20), RegularExpression(@"[a-zA-Z]+$", ErrorMessage = "Non-ASCII-letters not allowed")]
    public string RoomType { get; set; }
  }
}
