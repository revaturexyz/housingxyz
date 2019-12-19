using System;
using Xunit;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Tests.LibTests
{
  public class LibModelTest
  {
    /// <summary>
    /// This test is to test Amenity in Library.Model
    /// </summary>
    [Fact]
    public void AmenityTest()
    {
      var aId = Guid.NewGuid();
      var amenity = new Logic.Amenity
      {
        AmenityId = aId,
        AmenityType = "fridge",
        Description = "to freeze items"
      };

      Assert.Equal(aId, amenity.AmenityId);
      Assert.Equal("fridge", amenity.AmenityType);
      Assert.Equal("to freeze items", amenity.Description);
    }

    /// <summary>
    /// This test is to test AmenityComplex in Library.Model
    /// </summary>
    [Fact]
    public void AmenityComplexTest()
    {
      var acId1 = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var cId1 = Guid.NewGuid();

      var ac = new Logic.AmenityComplex
      {
        AmenityComplexId = acId1,
        AmenityId = amId,
        ComplexId = cId1
      };

      Assert.Equal(acId1, ac.AmenityComplexId);
      Assert.Equal(amId, ac.AmenityId);
      Assert.Equal(cId1, ac.ComplexId);
    }

    /// <summary>
    /// This test is to test AmenityRoom in Library.Model
    /// </summary>
    [Fact]
    public void AmenityRoomTest()
    {
      var arId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var rId = Guid.NewGuid();

      var ar = new Logic.AmenityRoom
      {
        AmenityRoomId = arId,
        AmenityId = amId,
        RoomId = rId
      };

      Assert.Equal(arId, ar.AmenityRoomId);
      Assert.Equal(amId, ar.AmenityId);
      Assert.Equal(rId, ar.RoomId);
    }

    /// <summary>
    /// This test is to test Complex in Library.Model
    /// </summary>
    [Fact]
    public void ComplexTest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();

      var complex = new Logic.Complex
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
