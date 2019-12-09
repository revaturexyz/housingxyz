using System;
using Xunit;
using Entity = Revature.Complex.DataAccess.Entities;

namespace Revature.Complex.Tests.DataTests
{
  public class EntityModelTest
  {
    /// <summary>
    /// This test is to test Amenity in Entity Model
    /// </summary>
    [Fact]
    public void AmenityTest()
    {
      var amId = Guid.NewGuid();
      var amenity = new Entity.Amenity
      {
        AmenityId = amId,
        AmenityType = "fridge",
        Description = "to freeze items"
      };

      Assert.Equal(amId, amenity.AmenityId);
      Assert.Equal("fridge", amenity.AmenityType);
      Assert.Equal("to freeze items", amenity.Description);
    }

    /// <summary>
    /// This test is to test AmenityComplex in Entity Model
    /// </summary>
    [Fact]
    public void AmenityComplexTest()
    {
      var acId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var guid1 = Guid.NewGuid();

      var ac = new Entity.AmenityComplex
      {
        AmenityComplexId = acId,
        AmenityId = amId,
        ComplexId = guid1
      };

      Assert.Equal(acId, ac.AmenityComplexId);
      Assert.Equal(amId, ac.AmenityId);
      Assert.Equal(guid1, ac.ComplexId);
    }

    /// <summary>
    /// This test is to test AmenityRoom in Entity Model
    /// </summary>
    [Fact]
    public void AmenityRoomTest()
    {
      var arId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var guid = Guid.NewGuid();

      var ar = new Entity.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = guid
      };

      Assert.Equal(arId, ar.AmenityRoomId);
      Assert.Equal(amId, ar.AmenityId);
      Assert.Equal(guid, ar.RoomId);
    }

    /// <summary>
    /// This test is to test Complex in Entity Model
    /// </summary>
    [Fact]
    public void ComplexTest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();

      var complex = new Entity.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };

      Assert.Equal(cId, complex.ComplexId);
      Assert.Equal(aId, complex.AddressId);
      Assert.Equal(pId, complex.ProviderId);
      Assert.Equal("Liv+", complex.ComplexName);
      Assert.Equal("(123)456-7890", complex.ContactNumber);
    }
  }
}
