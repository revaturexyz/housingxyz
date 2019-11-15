using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexServiceDatabase.Model
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string AmenityType { get; set; }
        public string Description { get; set; }

        public ICollection<AmenityRoom> AmenityRoom { get; set; }

        public ICollection<AmenityComplex> AmenityComplex { get; set; }
    }
}
