using System;
using Revature.Complex.Api.Models;
using Xunit;

namespace Revature.Complex.Tests.ApiTests
{
  public class ApiModelTest
  {
    /// <summary>
    /// This test is to test Amenity model in Api Model 
    /// </summary>
    [Fact]
    public void ApiAmenityTest()
    {
      var aId = Guid.NewGuid();
      var amenity = new ApiAmenity
      {
        AmenityId = aId,
        AmenityType = "Test",
        Description = "describe"
      };

      Assert.Equal(aId, amenity.AmenityId);
      Assert.Equal("Test", amenity.AmenityType);
      Assert.Equal("describe", amenity.Description);
    }

    /// <summary>
    /// This test is to test Complex Address model in Api Model 
    /// </summary>
    [Fact]
    public void ApiComplexAddressTest()
    {
      var aId = Guid.NewGuid();
      var address = new ApiComplexAddress
      {
        AddressId = aId,
        StreetAddress = "123 test ave",
        City = "Dallas",
        State = "TX",
        ZipCode = "76010"
      };

      Assert.Equal(aId, address.AddressId);
      Assert.Equal("123 test ave", address.StreetAddress);
      Assert.Equal("Dallas", address.City);
      Assert.Equal("TX", address.State);
      Assert.Equal("76010", address.ZipCode);
    }

    /// <summary>
    /// This test is to test Complex model in Api Model 
    /// </summary>
    [Fact]
    public void ApiComplexTest()
    {
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();

      var complex = new ApiComplex
      {
        ComplexId = aId,
        ComplexName = "Test",
        ContactNumber = "1234567890",
        ProviderId = pId
      };

      Assert.Equal(aId, complex.ComplexId);
      Assert.Equal(pId, complex.ProviderId);
      Assert.Equal("Test", complex.ComplexName);
      Assert.Equal("1234567890", complex.ContactNumber);
    }

    /// <summary>
    /// This test is to test Room model in Api Model 
    /// </summary>
    [Fact]
    public void ApiRoomTest()
    {
      var cId = Guid.NewGuid();
      var start = DateTime.Parse("2019/1/1");
      var end = DateTime.Parse("2020/1/1");
      var room = new ApiRoom
      {
        RoomNumber = "1234",
        ComplexId = cId,
        NumberOfBeds = 3,
        ApiRoomType = "apartment",
        LeaseStart = start,
        LeaseEnd = end
      };

      Assert.Equal("1234", room.RoomNumber);
      Assert.Equal(cId, room.ComplexId);
      Assert.Equal(3, room.NumberOfBeds);
      Assert.Equal("apartment", room.ApiRoomType);
      Assert.Equal(DateTime.Parse("2019/1/1"), room.LeaseStart);
      Assert.Equal(DateTime.Parse("2020/1/1"), room.LeaseEnd);
    }

    /// <summary>
    /// This test is to test RoomtoSend model in Api Model 
    /// </summary>
    [Fact]
    public void ApiRoomtoSendTest()
    {
      var rId = Guid.NewGuid();
      var cId = Guid.NewGuid();
      var start = DateTime.Parse("2019/1/1");
      var end = DateTime.Parse("2020/1/1");
      var send = new ApiRoomtoSend
      {
        RoomId = rId,
        RoomNumber = "1234",
        ComplexId = cId,
        NumberOfBeds = 4,
        RoomType = "dormitory",
        LeaseStart = start,
        LeaseEnd = end
      };

      Assert.Equal(rId, send.RoomId);
      Assert.Equal("1234", send.RoomNumber);
      Assert.Equal(cId, send.ComplexId);
      Assert.Equal(4, send.NumberOfBeds);
      Assert.Equal("dormitory", send.RoomType);
      Assert.Equal(DateTime.Parse("2019/1/1"), send.LeaseStart);
      Assert.Equal(DateTime.Parse("2020/1/1"), send.LeaseEnd);
    }
  }
}
