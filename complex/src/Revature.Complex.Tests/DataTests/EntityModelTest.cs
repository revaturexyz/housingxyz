using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Entity = Revature.Complex.DataAccess.Entities;

namespace Revature.Complex.Tests.DataTests
{
  public class EntityModelTest
  {
    [Fact]
    public void AmenityTest()
    {
      Entity.Amenity amenity = new Entity.Amenity
      {
        AmenityId = 1,
        AmenityType = "fridge",
        Description = "to freeze items"
      };

      Assert.Equal(1, amenity.AmenityId);
      Assert.Equal("fridge", amenity.AmenityType);
      Assert.Equal("to freeze items", amenity.Description);
    }

    [Fact]
    public void AmenityComplexTest()
    {
      Guid guid1 = Guid.NewGuid();

      Entity.AmenityComplex ac = new Entity.AmenityComplex
      {
        AmenityComplexId = 1,
        AmenityId = 1,
        ComplexId = guid1
      };

      Assert.Equal(1, ac.AmenityComplexId);
      Assert.Equal(1, ac.AmenityId);
      Assert.Equal(guid1, ac.ComplexId);
    }

    [Fact]
    public void AmenityRoomTest()
    {
      Guid guid = Guid.NewGuid();

      Entity.AmenityRoom ar = new Entity.AmenityRoom
      {
        AmenityRoomId = 1,
        AmenityId = 2,
        RoomId = guid
      };

      Assert.Equal(1, ar.AmenityRoomId);
      Assert.Equal(2, ar.AmenityId);
      Assert.Equal(guid, ar.RoomId);
    }

    [Fact]
    public void ComplexTest()
    {
      Guid cId = Guid.NewGuid();
      Guid aId = Guid.NewGuid();
      Guid pId = Guid.NewGuid();

      Entity.Complex complex = new Entity.Complex
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
