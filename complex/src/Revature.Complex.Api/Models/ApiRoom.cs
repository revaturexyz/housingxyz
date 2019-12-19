using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Api.Models
{
  /// <summary>
  /// Api Room model. it is received from front-end when
  /// creating new rooms (as enumarable collections)
  /// (not for sending to Room service)
  /// </summary>
  public class ApiRoom
  {
    public Guid RoomId { get; set; }
    [Required]
    public string RoomNumber { get; set; }
    public Guid ComplexId { get; set; }
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }
    public List<ApiAmenity> Amenities { get; set; }
    public string ApiRoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }
  }
}
