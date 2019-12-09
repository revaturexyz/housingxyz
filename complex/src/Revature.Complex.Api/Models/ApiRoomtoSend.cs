using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Api.Models
{
  /// <summary>
  /// Api RoomtoSend model Created from Api Room model
  /// Its purpose is to send to Room service as enumarable collections
  /// </summary>
  public class ApiRoomtoSend
  {
    [Required]
    public Guid RoomId { get; set; }
    public string RoomNumber { get; set; }
    public Guid ComplexId { get; set; }
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }
    public string RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    /// <summary>
    /// it is an identifier for Room service to recongnize which method should they act(CUD)
    /// 0: create room, 1: update room, 2: delete single room, 3: delete all rooms belongs to same complex
    /// </summary>
    public int QueOperator { get; set; }
  }
}
