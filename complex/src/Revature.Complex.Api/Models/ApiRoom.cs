using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revature.Complex.Api.Models
{
  public class ApiRoom
  {
    [Required]
    public string RoomNumber { get; set; }
    // Must have between 1 and 10 beds
    public Guid ComplexId { get; set; }
    public string Gender { get; set; }
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }
    public List<ApiAmenity> Amenities { get; set; }
    public string ApiRoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }
  }
}
