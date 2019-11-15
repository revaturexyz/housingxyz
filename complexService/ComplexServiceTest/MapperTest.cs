using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Entity = ComplexServiceDatabase.Model;
using ComplexServiceDatabase.Repo;
using Logic = ComplexServiceLogic.Model;

namespace ComplexServiceTest
{
    public class MapperTest
    {
        [Fact]
        public void ComplextoETest()
        {
            Guid cId = Guid.NewGuid();
            Guid aId = Guid.NewGuid();
            Guid pId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Logic.Complex c = new Logic.Complex
            {
                ComplexId = cId,
                AddressId = aId,
                ProviderId = pId,
                ComplexName = "Liv+",
                ContactNumber = "(123)456-7890"
            };

            Entity.Complex ce = mapper.MapComplextoE(c);

            Assert.Equal(cId, ce.ComplexId);
            Assert.Equal(aId, ce.AddressId);
            Assert.Equal(pId, ce.ProviderId);
            Assert.Equal("Liv+", ce.ComplexName);
            Assert.Equal("(123)456-7890", ce.ContactNumber);
        }
        
        [Fact]
        public void EtoComplexTest()
        {
            Guid cId = Guid.NewGuid();
            Guid aId = Guid.NewGuid();
            Guid pId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Entity.Complex c = new Entity.Complex
            {
                ComplexId = cId,
                AddressId = aId,
                ProviderId = pId,
                ComplexName = "Liv+",
                ContactNumber = "(123)456-7890"
            };

            Logic.Complex cl = mapper.MapEtoComplex(c);

            Assert.Equal(cId, cl.ComplexId);
            Assert.Equal(aId, cl.AddressId);
            Assert.Equal(pId, cl.ProviderId);
            Assert.Equal("Liv+", cl.ComplexName);
            Assert.Equal("(123)456-7890", cl.ContactNumber);
        }

        [Fact]
        public void AmenityRoomtoETest()
        {
            Guid rId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Logic.AmenityRoom ar = new Logic.AmenityRoom
            {
                AmenityRoomId = 1,
                AmenityId = 1,
                RoomId = rId
            };

            Entity.AmenityRoom eAr = mapper.MapAmenityRoomtoE(ar);

            Assert.Equal(1, eAr.AmenityRoomId);
            Assert.Equal(1, eAr.AmenityId);
            Assert.Equal(rId, eAr.RoomId);
        }

        [Fact]
        public void EtoAmenityRoom()
        {
            Guid rId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Entity.AmenityRoom ar = new Entity.AmenityRoom
            {
                AmenityRoomId = 1,
                AmenityId = 1,
                RoomId = rId
            };

            Logic.AmenityRoom lAr = mapper.MapEtoAmenityRoom(ar);

            Assert.Equal(1, lAr.AmenityRoomId);
            Assert.Equal(1, lAr.AmenityId);
            Assert.Equal(rId, lAr.RoomId);
        }

        [Fact]
        public void AmenityComplextoETest()
        {
            Guid cId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Logic.AmenityComplex ac = new Logic.AmenityComplex
            {
                AmenityComplexId = 1,
                AmenityId = 1,
                ComplexId = cId
            };

            Entity.AmenityComplex eAc = mapper.MapAmenityComplextoE(ac);

            Assert.Equal(1, eAc.AmenityComplexId);
            Assert.Equal(1, eAc.AmenityId);
            Assert.Equal(cId, eAc.ComplexId);
        }

        [Fact]
        public void EtoAmenityComplexTest()
        {
            Guid cId = Guid.NewGuid();
            Mapper mapper = new Mapper();

            Entity.AmenityComplex ac = new Entity.AmenityComplex
            {
                AmenityComplexId = 1,
                AmenityId = 1,
                ComplexId = cId
            };

            Logic.AmenityComplex lAc = mapper.MapEtoAmenityComplex(ac);

            Assert.Equal(1, lAc.AmenityComplexId);
            Assert.Equal(1, lAc.AmenityId);
            Assert.Equal(cId, lAc.ComplexId);
        }

        [Fact]
        public void AmenitytoETest()
        {
            Mapper mapper = new Mapper();
            Logic.Amenity a = new Logic.Amenity
            {
                AmenityId = 1,
                AmenityType = "Fridge",
                Description = "To freeze items"
            };

            Entity.Amenity ea = mapper.MapAmenitytoE(a);

            Assert.Equal(1, ea.AmenityId);
            Assert.Equal("Fridge", ea.AmenityType);
            Assert.Equal("To freeze items", ea.Description);
        }

        [Fact]
        public void EtoAmenityTest()
        {
            Mapper mapper = new Mapper();
            Entity.Amenity a = new Entity.Amenity
            {
                AmenityId = 1,
                AmenityType = "Fridge",
                Description = "To freeze items"
            };

            Logic.Amenity la = mapper.MapEtoAmenity(a);

            Assert.Equal(1, la.AmenityId);
            Assert.Equal("Fridge", la.AmenityType);
            Assert.Equal("To freeze items", la.Description);
        }
    }
}
