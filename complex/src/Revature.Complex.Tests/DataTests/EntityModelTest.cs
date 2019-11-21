using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Entity = Revature.Complex.DataAccess.Entities;

namespace Revature.Complex.Tests.DataTests
{
  public class EntityModelTest
  {
    /// <summary>
    /// Verify Amenity constructor correctly stores the data inputted
    /// </summary>
    [Fact]
    public void AmenityTest()
    {
      Guid amId = Guid.NewGuid();
      Entity.Amenity amenity = new Entity.Amenity
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
    /// Verify AmenityComplex constructor correctly stores the data inputted
    /// </summary>
    [Fact]
    public void AmenityComplexTest()
    {
      Guid acId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid guid1 = Guid.NewGuid();

      Entity.AmenityComplex ac = new Entity.AmenityComplex
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
    /// Verify AmenityRoom constructor correctly stores the data inputted
    /// </summary>
    [Fact]
    public void AmenityRoomTest()
    {
      Guid arId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid guid = Guid.NewGuid();

      Entity.AmenityRoom ar = new Entity.AmenityRoom
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
    /// Verify Complex constructor correctly stores the data inputted
    /// </summary>
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
