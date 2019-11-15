using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexServiceApi.Controllers.Model
{
    public class APIRoom
    {
        [Required]
        public int RoomGUID { get; set; }
        // Must have between 1 and 10 beds
        [Range(1, 10)]
        public int NumberOfBeds { get; set; }
        public bool HasAmenity { get; set; }
        public string ApiRoomType { get; set; }

        public string StartDate { get; set; }
        // Check to see that end date is after start date
        public DateTime? EndDate { get; set; }
    }
}
