using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexServiceDatabase.Model
{
    public class AmenityRoom
    {
        public int AmenityRoomId { get; set; }
        public int AmenityId { get; set; }
        public Guid RoomId { get; set; }

        public Amenity Amenity { get; set; }
    }
}
