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
    public int RoomGUID { get; set; }
    // Must have between 1 and 10 beds
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }
    public bool HasAmenity { get; set; }
    public string ApiRoomType { get; set; }
  }
}
