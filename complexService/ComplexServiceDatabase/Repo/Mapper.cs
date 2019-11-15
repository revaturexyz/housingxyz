using System;
using System.Collections.Generic;
using System.Text;
using Logic = ComplexServiceLogic.Model;
using Entity = ComplexServiceDatabase.Model;

namespace ComplexServiceDatabase.Repo
{
    public static class Mapper : IMapper
    {
        public static Entity.Amenity MapAmenitytoE(Logic.Amenity amenity)
        {
            return new Entity.Amenity
            {
                AmenityId = amenity.AmenityId,
                AmenityType = amenity.AmenityType,
                Description = amenity.Description
            };
        }

        public static Logic.Amenity MapEtoAmenity(Entity.Amenity amenity)
        {
            return new Logic.Amenity
            {
                AmenityId = amenity.AmenityId,
                AmenityType = amenity.AmenityType,
                Description = amenity.Description
            };
        }

        public static Entity.AmenityComplex MapAmenityComplextoE(Logic.AmenityComplex ac)
        {
            return new Entity.AmenityComplex
            {
                AmenityComplexId = ac.AmenityComplexId,
                AmenityId = ac.AmenityId,
                ComplexId = ac.ComplexId
            };
        }

        public static Logic.AmenityComplex MapEtoAmenityComplex(Entity.AmenityComplex ac)
        {
            return new Logic.AmenityComplex
            {
                AmenityComplexId = ac.AmenityComplexId,
                AmenityId = ac.AmenityId,
                ComplexId = ac.ComplexId
            };
        }

        public static Entity.AmenityRoom MapAmenityRoomtoE(Logic.AmenityRoom ar)
        {
            return new Entity.AmenityRoom
            {
                AmenityRoomId = ar.AmenityRoomId,
                AmenityId = ar.AmenityId,
                RoomId = ar.RoomId
            };
        }

        public static Logic.AmenityRoom MapEtoAmenityRoom(Entity.AmenityRoom ar)
        {
            return new Logic.AmenityRoom
            {
                AmenityRoomId = ar.AmenityRoomId,
                AmenityId = ar.AmenityId,
                RoomId = ar.RoomId
            };
        }

        public static Entity.Complex MapComplextoE(Logic.Complex c)
        {
            return new Entity.Complex
            {
                ComplexId = c.ComplexId,
                AddressId = c.AddressId,
                ProviderId = c.ProviderId,
                ComplexName = c.ComplexName,
                ContactNumber = c.ContactNumber
            };
        }

        public static Logic.Complex MapEtoComplex(Entity.Complex c)
        {
            return new Logic.Complex
            {
                ComplexId = c.ComplexId,
                AddressId = c.AddressId,
                ProviderId = c.ProviderId,
                ComplexName = c.ComplexName,
                ContactNumber = c.ContactNumber
            };
        }
    }
}
