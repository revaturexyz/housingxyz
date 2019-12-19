using System;
using Revature.Complex.DataAccess;
using Xunit;
using Entity = Revature.Complex.DataAccess.Entities;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Tests.DataTests
{
  public class MapperTest
  {
    /// <summary>
    /// This test is to test ComplextoE in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void ComplextoETest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();
      var mapper = new Mapper();

      var c = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };

      var ce = mapper.MapComplextoE(c);

      Assert.Equal(cId, ce.ComplexId);
      Assert.Equal(aId, ce.AddressId);
      Assert.Equal(pId, ce.ProviderId);
      Assert.Equal("Liv+", ce.ComplexName);
      Assert.Equal("(123)456-7890", ce.ContactNumber);
    }

    /// <summary>
    /// This test is to test EtoComplex in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void EtoComplexTest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();
      var mapper = new Mapper();

      var c = new Entity.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };

      var cl = mapper.MapEtoComplex(c);

      Assert.Equal(cId, cl.ComplexId);
      Assert.Equal(aId, cl.AddressId);
      Assert.Equal(pId, cl.ProviderId);
      Assert.Equal("Liv+", cl.ComplexName);
      Assert.Equal("(123)456-7890", cl.ContactNumber);
    }

    /// <summary>
    /// This test is to test AmenityRoomtoE in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void AmenityRoomtoETest()
    {
      var arId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var rId = Guid.NewGuid();
      var mapper = new Mapper();

      var ar = new Logic.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      var eAr = mapper.MapAmenityRoomtoE(ar);

      Assert.Equal(arId, eAr.AmenityRoomId);
      Assert.Equal(amId, eAr.AmenityId);
      Assert.Equal(rId, eAr.RoomId);
    }

    /// <summary>
    /// This test is to test EtoAmenityRoom in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void EtoAmenityRoom()
    {
      var arId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var rId = Guid.NewGuid();
      var mapper = new Mapper();

      var ar = new Entity.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      var lAr = mapper.MapEtoAmenityRoom(ar);

      Assert.Equal(arId, lAr.AmenityRoomId);
      Assert.Equal(amId, lAr.AmenityId);
      Assert.Equal(rId, lAr.RoomId);
    }

    /// <summary>
    /// This test is to test AmenityComplextoE in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void AmenityComplextoETest()
    {
      var acId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var cId = Guid.NewGuid();
      var mapper = new Mapper();

      var ac = new Logic.AmenityComplex
      {
        AmenityComplexId = acId,
        AmenityId = amId,
        ComplexId = cId
      };

      var eAc = mapper.MapAmenityComplextoE(ac);

      Assert.Equal(acId, eAc.AmenityComplexId);
      Assert.Equal(amId, eAc.AmenityId);
      Assert.Equal(cId, eAc.ComplexId);
    }

    /// <summary>
    /// This test is to test EtoAmenityComplex in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void EtoAmenityComplexTest()
    {
      var acId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var cId = Guid.NewGuid();
      var mapper = new Mapper();

      var ac = new Entity.AmenityComplex
      {
        AmenityComplexId = acId,
        AmenityId = amId,
        ComplexId = cId
      };

      var lAc = mapper.MapEtoAmenityComplex(ac);

      Assert.Equal(acId, lAc.AmenityComplexId);
      Assert.Equal(amId, lAc.AmenityId);
      Assert.Equal(cId, lAc.ComplexId);
    }

    /// <summary>
    /// This test is to test AmenitytoE in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void AmenitytoETest()
    {
      var amId = Guid.NewGuid();
      var mapper = new Mapper();
      var a = new Logic.Amenity
      {
        AmenityId = amId,
        AmenityType = "Fridge",
        Description = "To freeze items"
      };

      var ea = mapper.MapAmenitytoE(a);

      Assert.Equal(amId, ea.AmenityId);
      Assert.Equal("Fridge", ea.AmenityType);
      Assert.Equal("To freeze items", ea.Description);
    }

    /// <summary>
    /// This test is to test EtoAmenity in DataAccess.Mapper.cs
    /// </summary>
    [Fact]
    public void EtoAmenityTest()
    {
      var amId = Guid.NewGuid();
      var mapper = new Mapper();
      var a = new Entity.Amenity
      {
        AmenityId = amId,
        AmenityType = "Fridge",
        Description = "To freeze items"
      };

      var la = mapper.MapEtoAmenity(a);

      Assert.Equal(amId, la.AmenityId);
      Assert.Equal("Fridge", la.AmenityType);
      Assert.Equal("To freeze items", la.Description);
    }
  }
}
