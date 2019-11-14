namespace Xyz.Provider.DataAccess.Entities
{
  public partial class Notification
  {
    /// <summary>
    /// Primary key of Notification row
    /// </summary>
    public int NotificationId { get; set; }

    /// <summary>
    /// ProviderId foreign key cell of Notification row
    /// </summary>
    public int ProviderId { get; set; }

    /// <summary>
    /// RoomId foreign key cell of Notification row
    /// </summary>
    public int RoomId { get; set; }

    /// <summary>
    /// Title cell of Notification row
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Reason cell of Notification row
    /// </summary>
    public string Reason { get; set; }

    public virtual Provider Provider { get; set; }
    public virtual Room Room { get; set; }
  }
}
