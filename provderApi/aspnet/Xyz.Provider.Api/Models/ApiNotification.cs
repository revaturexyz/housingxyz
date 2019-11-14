using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiNotification
  {
    public int NotificationId { get; set; }

    public int ProviderId { get; set; }

    public int RoomId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; }

    [StringLength(512)]
    public string Reason { get; set; }
  }
}
