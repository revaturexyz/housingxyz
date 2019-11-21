using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Entity = Revature.Complex.DataAccess.Entities;
using Revature.Complex.DataAccess;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Tests.DataTests
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
      Guid arId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid rId = Guid.NewGuid();
      Mapper mapper = new Mapper();

      Logic.AmenityRoom ar = new Logic.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      Entity.AmenityRoom eAr = mapper.MapAmenityRoomtoE(ar);

      Assert.Equal(arId, eAr.AmenityRoomId);
      Assert.Equal(amId, eAr.AmenityId);
      Assert.Equal(rId, eAr.RoomId);
    }

    [Fact]
    public void EtoAmenityRoom()
    {
      Guid arId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid rId = Guid.NewGuid();
      Mapper mapper = new Mapper();

      Entity.AmenityRoom ar = new Entity.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      Logic.AmenityRoom lAr = mapper.MapEtoAmenityRoom(ar);

      Assert.Equal(arId, lAr.AmenityRoomId);
      Assert.Equal(amId, lAr.AmenityId);
      Assert.Equal(rId, lAr.RoomId);
    }

    [Fact]
    public void AmenityComplextoETest()
    {
      Guid acId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid cId = Guid.NewGuid();
      Mapper mapper = new Mapper();

      Logic.AmenityComplex ac = new Logic.AmenityComplex
      {
        AmenityComplexId = acId,
        AmenityId = amId,
        ComplexId = cId
      };

      Entity.AmenityComplex eAc = mapper.MapAmenityComplextoE(ac);

      Assert.Equal(acId, eAc.AmenityComplexId);
      Assert.Equal(amId, eAc.AmenityId);
      Assert.Equal(cId, eAc.ComplexId);
    }

    [Fact]
    public void EtoAmenityComplexTest()
    {
      Guid acId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid cId = Guid.NewGuid();
      Mapper mapper = new Mapper();

      Entity.AmenityComplex ac = new Entity.AmenityComplex
      {
        AmenityComplexId = acId,
        AmenityId = amId,
        ComplexId = cId
      };

      Logic.AmenityComplex lAc = mapper.MapEtoAmenityComplex(ac);

      Assert.Equal(acId, lAc.AmenityComplexId);
      Assert.Equal(amId, lAc.AmenityId);
      Assert.Equal(cId, lAc.ComplexId);
    }

    [Fact]
    public void AmenitytoETest()
    {
      Guid amId = Guid.NewGuid();
      Mapper mapper = new Mapper();
      Logic.Amenity a = new Logic.Amenity
      {
        AmenityId = amId,
        AmenityType = "Fridge",
        Description = "To freeze items"
      };

      Entity.Amenity ea = mapper.MapAmenitytoE(a);

      Assert.Equal(amId, ea.AmenityId);
      Assert.Equal("Fridge", ea.AmenityType);
      Assert.Equal("To freeze items", ea.Description);
    }

    [Fact]
    public void EtoAmenityTest()
    {
      Guid amId = Guid.NewGuid();
      Mapper mapper = new Mapper();
      Entity.Amenity a = new Entity.Amenity
      {
        AmenityId = amId,
        AmenityType = "Fridge",
        Description = "To freeze items"
      };

      Logic.Amenity la = mapper.MapEtoAmenity(a);

      Assert.Equal(amId, la.AmenityId);
      Assert.Equal("Fridge", la.AmenityType);
      Assert.Equal("To freeze items", la.Description);
    }
  }
}
