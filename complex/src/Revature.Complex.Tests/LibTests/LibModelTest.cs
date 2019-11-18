using System;
using System.Collections.Generic;
using System.Text;
using Logic = Revature.Complex.Lib.Models;
using Xunit;

namespace Revature.Complex.Tests.LibTests
{
  public class LibModelTest
  {
    [Fact]
    public void AmenityTest()
    {
      Logic.Amenity amenity = new Logic.Amenity
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

      Logic.AmenityComplex ac = new Logic.AmenityComplex
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

      Logic.AmenityRoom ar = new Logic.AmenityRoom
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

      Logic.Complex complex = new Logic.Complex
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
