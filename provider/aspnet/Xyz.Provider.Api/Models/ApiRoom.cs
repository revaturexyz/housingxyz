using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiRoom
  {
    public int RoomId { get; set; }

    public ApiComplex ApiComplex { get; set; }

    public ApiAddress ApiAddress { get; set; }

    public ApiRoomType ApiRoomType { get; set; }

    public ApiGender ApiGender { get; set; }

    // Must have a room number
    [Required]
    public string RoomNumber { get; set; }

    // Must have between 1 and 10 beds
    [Range(1, 10)]
    public int NumberOfBeds { get; set; }

    public bool IsOccupied { get; set; }

    public bool HasAmenity { get; set; }

    public DateTime StartDate { get; set; }

    // Check to see that end date is after start date
    public DateTime? EndDate { get; set; }
    public ICollection<ApiAmenity> ApiAmenity { get; set; }
  }
}
