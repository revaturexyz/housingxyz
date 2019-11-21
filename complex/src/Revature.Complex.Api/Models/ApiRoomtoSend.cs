using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Api.Models
{
  /// <summary>
  /// Room model to send to Room service
  /// </summary>
  public class ApiRoomtoSend
  {
    /// <summary>
    /// Room Id of Api RoomtoSend model
    /// </summary>
    [Required]
    public Guid RoomId { get; set; }

    /// <summary>
    /// Room number of Api RoomtoSend model
    /// </summary>
    public string RoomNumber { get; set; }

    /// <summary>
    /// Complex Id of Api RoomtoSend model
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// Gender of Api RoomtoSend model
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Number of beds in a room of Api RoomtoSend model
    /// </summary>
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }

    /// <summary>
    /// type of room of Api RoomtoSend model
    /// </summary>
    public string RoomType { get; set; }

    /// <summary>
    /// Lease start date of Api RoomtoSend model
    /// </summary>
    public DateTime LeaseStart { get; set; }

    /// <summary>
    /// Lease End date of Api RoomtoSend model
    /// </summary>
    public DateTime LeaseEnd { get; set; }
  }
}
