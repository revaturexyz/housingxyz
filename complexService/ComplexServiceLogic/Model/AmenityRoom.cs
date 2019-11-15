using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexServiceLogic.Model
{
    public class AmenityRoom
    {
        public int AmenityRoomId { get; set; }
        public int AmenityId { get; set; }
        public Guid RoomId { get; set; }

    }
}
