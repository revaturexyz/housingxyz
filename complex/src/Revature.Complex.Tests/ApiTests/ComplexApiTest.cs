using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Complex.Api.Controllers;
using Revature.Complex.Api.Models;
using Revature.Complex.Api.Services;
using Revature.Complex.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Tests.ApiTests
{
  public class ComplexApiTest
  {
    /// <summary>
    /// This test is to test GetAllComplexAsync in Complex Api
    /// </summary>
    [Fact]
    public async void GetAllComplexAsyncTest()
    {
      var complex = new Logic.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "test",
        ContactNumber = "1234567892"
      };
      //setup
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = new List<Logic.Complex>
      {
        complex
      };
      complexRepo.Setup(r => r.ReadComplexListAsync())
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(await controller.GetAllComplexAsync());

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(model);
    }

    /// <summary>
    /// This test is to test GetComplexByIdAsync in Complex Api
    /// </summary>
    [Fact]
    public async void GetComplexByIdAsyncTest()
    {
      //setup
      var complexId = Guid.NewGuid();
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = new Logic.Complex();

      complexRepo.Setup(r => r.ReadComplexByIdAsync(complexId))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<ActionResult<ApiComplex>>(await controller.GetComplexByIdAsync(complexId));

      //assert
      Assert.IsAssignableFrom<ActionResult<ApiComplex>>(model);
    }

    /// <summary>
    /// This test is to test GetComplexByNameAndNumberAsync in Complex Api
    /// </summary>
    [Fact]
    public async void GetComplexByNameAndNumberAsyncTest()
    {
      //setup
      var name = "test1";
      var number = "1234567890";
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = new Logic.Complex();

      complexRepo.Setup(r => r.ReadComplexByNameAndNumberAsync(name, number))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<ActionResult<ApiComplex>>(await controller.GetComplexByNameAndNumberAsync(name, number));

      //assert
      Assert.IsAssignableFrom<ActionResult<ApiComplex>>(model);
    }

    /// <summary>
    /// This test is to test GetComplexListByProviderIdAsync in Complex Api
    /// </summary>
    [Fact]
    public async void GetComplexListByProviderIdAsyncTest()
    {
      //setup
      var pId = Guid.NewGuid();
      var complex = new Logic.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = pId,
        ComplexName = "test",
        ContactNumber = "1234567892"
      };
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = new List<Logic.Complex>
      {
        complex
      };
      complexRepo.Setup(r => r.ReadComplexByProviderIdAsync(pId))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(await controller.GetComplexListByProviderId(pId));

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(model);
    }

    //  //act
    //  var controller = new ComplexController(_complexRepo.Object, _logger.Object, rss.Object, ar.Object, rr.Object);
    //  var model = Assert.IsAssignableFrom<ActionResult<ApiComplex>>(await controller.PostComplexAsync(apiComplex));

    //  //assert
    //  Assert.IsAssignableFrom<ActionResult<ApiComplex>>(model);
    //}

    /// <summary>
    /// This test is to test PostRoomAsync in Complex Api
    /// </summary>
    [Fact]
    public async void PostRoomsAsyncTest()
    {
      var amId = Guid.NewGuid();
      var cId = Guid.NewGuid();
      var rId = Guid.NewGuid();
      var amenity = new ApiAmenity
      {
        AmenityId = amId,
        AmenityType = "Pool",
        Description = "swimmming"
      };
      var amenities = new List<ApiAmenity>
      {
        amenity
      };
      var ar = new Logic.AmenityRoom
      {
        AmenityRoomId = Guid.NewGuid(),
        AmenityId = amId,
        RoomId = rId
      };
      var room = new ApiRoom
      {
        RoomId = rId,
        RoomNumber = "1234",
        ComplexId = cId,
        ApiRoomType = "dorm",
        NumberOfBeds = 3,
        LeaseStart = Convert.ToDateTime("2010/1/1"),
        LeaseEnd = Convert.ToDateTime("2020/1/1"),
        Amenities = amenities
      };
      var rooms = new List<ApiRoom>
      {
        room
      };
      IEnumerable<ApiRoom> apiRooms = rooms;
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ara = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = true;

      complexRepo.Setup(r => r.CreateAmenityRoomAsync(ar))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ara.Object, rr.Object);
      var model = Assert.IsAssignableFrom<StatusCodeResult>(await controller.PostRoomsAsync(apiRooms));

      //assert
      Assert.IsAssignableFrom<StatusCodeResult>(model);
    }

    /// <summary>
    /// This test is to test PutComplexAsync in Complex Api
    /// </summary>
    [Fact]
    public async void PutComplexAsyncTest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var pId = Guid.NewGuid();
      var amId = Guid.NewGuid();
      var address = new ApiComplexAddress
      {
        AddressId = aId,
        StreetAddress = "test ave",
        City = "dallas",
        State = "TX",
        Country = "USA",
        ZipCode = "76010"
      };
      var amenity = new Logic.Amenity
      {
        AmenityId = amId,
        AmenityType = "name",
        Description = "description"
      };
      var amenities = new List<Logic.Amenity>
      {
        amenity
      };
      var apiComplex = new ApiComplex
      {
        ComplexId = cId,
        Address = address,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890",
        ComplexAmenity = amenities
      };
      var complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890"
      };
      var ac = new Logic.AmenityComplex
      {
        AmenityComplexId = Guid.NewGuid(),
        AmenityId = amId,
        ComplexId = cId
      };
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = true;

      complexRepo.Setup(r => r.DeleteAmenityComplexAsync(cId))
        .Returns(Task.FromResult(res));
      complexRepo.Setup(r => r.UpdateComplexAsync(complex))
        .Returns(Task.FromResult(res));
      complexRepo.Setup(c => c.ReadAmenityListAsync())
        .Returns(Task.FromResult(amenities));
      complexRepo.Setup(p => p.CreateAmenityComplexAsync(ac))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<StatusCodeResult>(await controller.PutComplexAsync(apiComplex));

      //assert
      Assert.IsAssignableFrom<StatusCodeResult>(model);
    }

    /// <summary>
    /// This test is to test PutRoomAsync in Complex Api
    /// </summary>
    [Fact]
    public async void PutRoomAsyncTest()
    {
      var amId = Guid.NewGuid();
      var cId = Guid.NewGuid();
      var rId = Guid.NewGuid();
      var amenity = new ApiAmenity
      {
        AmenityId = amId,
        AmenityType = "Pool",
        Description = "swimmming"
      };
      var amenities = new List<ApiAmenity>
      {
        amenity
      };
      var ar = new Logic.AmenityRoom
      {
        AmenityRoomId = Guid.NewGuid(),
        AmenityId = amId,
        RoomId = rId
      };
      var room = new ApiRoom
      {
        RoomId = rId,
        RoomNumber = "1234",
        ComplexId = cId,
        ApiRoomType = "dorm",
        NumberOfBeds = 3,
        LeaseStart = Convert.ToDateTime("2010/1/1"),
        LeaseEnd = Convert.ToDateTime("2020/1/1"),
        Amenities = amenities
      };
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ara = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = true;

      complexRepo.Setup(r => r.DeleteAmenityRoomAsync(rId))
        .Returns(Task.FromResult(res));
      complexRepo.Setup(r => r.CreateAmenityRoomAsync(ar))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ara.Object, rr.Object);
      var model = Assert.IsAssignableFrom<StatusCodeResult>(await controller.PutRoomAsync(room));

      //assert
      Assert.IsAssignableFrom<StatusCodeResult>(model);
    }

    /// <summary>
    /// This test is to test DeleteComplexAsync in Complex Api
    /// </summary>
    [Fact]
    public async void DeleteComplexAsyncTest()
    {
      var cId = Guid.NewGuid();
      var aId = Guid.NewGuid();
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = true;

      complexRepo.Setup(r => r.DeleteAmenityComplexAsync(cId))
        .Returns(Task.FromResult(res));
      complexRepo.Setup(r => r.DeleteComplexAsync(cId))
        .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<StatusCodeResult>(await controller.DeleteComplexAsync(cId));

      //assert
      Assert.IsAssignableFrom<StatusCodeResult>(model);
    }

    /// <summary>
    /// This test is to test DeleteRoomAsync in Complex Api
    /// </summary>
    [Fact]
    public async void DeleteRoomAsyncTest()
    {
      var rId = Guid.NewGuid();
      var room = new ApiRoom
      {
        RoomId = rId
      };
      var roomtoSend = new ApiRoomtoSend
      {
        RoomId = rId
      };
      var complexRepo = new Mock<IRepository>();
      var logger = new Mock<ILogger<ComplexController>>();
      var rss = new Mock<IRoomServiceSender>();
      var ar = new Mock<IAddressRequest>();
      var rr = new Mock<IRoomRequest>();
      var res = true;

      complexRepo.Setup(r => r.DeleteAmenityRoomAsync(rId))
        .Returns(Task.FromResult(res));
      rss.Setup(r => r.SendRoomsMessages(roomtoSend));

      //act
      var controller = new ComplexController(complexRepo.Object, logger.Object, rss.Object, ar.Object, rr.Object);
      var model = Assert.IsAssignableFrom<StatusCodeResult>(await controller.DeleteRoomAsync(room));

      //assert
      Assert.IsAssignableFrom<StatusCodeResult>(model);
    }
  }
}
