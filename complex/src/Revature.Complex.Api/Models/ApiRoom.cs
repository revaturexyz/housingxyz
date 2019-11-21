using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Api.Models
{
  /// <summary>
  /// Room model received from Front-End
  /// </summary>
  public class ApiRoom
  {
    /// <summary>
    /// Room number of Api Room model
    /// </summary>
    [Required]
    public string RoomNumber { get; set; }

    /// <summary>
    /// Complex id of Api Room model
    /// </summary>
    public Guid ComplexId { get; set; }
    /// <summary>
    /// Gender of Api Room model
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// number of beds per room of Api Room model
    /// </summary>
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }

    /// <summary>
    /// list of amenity in a room of Api Room model
    /// </summary>
    public List<ApiAmenity> Amenities { get; set; }

    /// <summary>
    /// Room type of Api Room model
    /// </summary>
    public string ApiRoomType { get; set; }

    /// <summary>
    /// Lease start date of Api Room model
    /// </summary>
    public DateTime LeaseStart { get; set; }

    /// <summary>
    /// Lease End date of Api Room model
    /// </summary>
    public DateTime LeaseEnd { get; set; }
  }
}
