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
      Guid aId = Guid.NewGuid();
      Logic.Amenity amenity = new Logic.Amenity
      {
        AmenityId = aId,
        AmenityType = "fridge",
        Description = "to freeze items"
      };

      Assert.Equal(aId, amenity.AmenityId);
      Assert.Equal("fridge", amenity.AmenityType);
      Assert.Equal("to freeze items", amenity.Description);
    }

    [Fact]
    public void AmenityComplexTest()
    {
      Guid acId1 = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid cId1 = Guid.NewGuid();

      Logic.AmenityComplex ac = new Logic.AmenityComplex
      {
        AmenityComplexId = acId1,
        AmenityId = amId,
        ComplexId = cId1
      };

      Assert.Equal(acId1, ac.AmenityComplexId);
      Assert.Equal(amId, ac.AmenityId);
      Assert.Equal(cId1, ac.ComplexId);
    }

    [Fact]
    public void AmenityRoomTest()
    {
      Guid arId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      Guid rId = Guid.NewGuid();

      Logic.AmenityRoom ar = new Logic.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      Assert.Equal(arId, ar.AmenityRoomId);
      Assert.Equal(amId, ar.AmenityId);
      Assert.Equal(rId, ar.RoomId);
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
