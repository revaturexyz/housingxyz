using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Logic = Revature.Complex.Lib.Models;
using Revature.Complex.Lib.Interface;
using Xunit;
using Revature.Complex.Api.Controllers;
using Revature.Complex.Api.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

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
      //setup
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      List<Logic.Complex> res = new List<Logic.Complex>();
      _complexRepo.Setup(r => r.ReadComplexListAsync())
          .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      Guid complexId = Guid.NewGuid();
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      Logic.Complex res = new Logic.Complex();
      _complexRepo.Setup(r => r.ReadComplexByIdAsync(complexId))
          .Returns(Task.FromResult(res));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      string name = "test1";
      string number = "1234567890";
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      Logic.Complex res = new Logic.Complex();
      _complexRepo.Setup(r => r.ReadComplexByNameAndNumberAsync(name, number))
          .Returns(Task.FromResult(res));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      Guid pId = Guid.NewGuid();
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      List<Logic.Complex> res = new List<Logic.Complex>();
      _complexRepo.Setup(r => r.ReadComplexByProviderIdAsync(pId))
          .Returns(Task.FromResult(res));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(await controller.GetComplexListByProviderId(pId));

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<ApiComplex>>>(model);
    }

    /// <summary>
    /// This test is to test PostComplexAsync in Complex Api
    /// </summary>
    [Fact]
    public async void PostComplexAsyncTest()
    {
      Guid cId = Guid.NewGuid();
      Guid aId = Guid.NewGuid();
      Guid pId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      ApiComplexAddress address = new ApiComplexAddress
      {
        AddressId = aId,
        StreetAddress = "test ave",
        City = "dallas",
        State = "TX",
        Country = "USA",
        ZipCode = "76010"
      };
      Logic.Amenity amenity = new Logic.Amenity
      {
        AmenityId = amId,
        AmenityType = "name",
        Description = "description"
      };
      List<Logic.Amenity> amenities = new List<Logic.Amenity>
      {
        amenity
      };
      ApiComplex apiComplex = new ApiComplex
      {
        ComplexId = cId,
        Address = address,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890",
        ComplexAmentiy = amenities
      };
      Logic.Complex complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890"
      };
      Logic.AmenityComplex ac = new Logic.AmenityComplex
      {
        AmenityComplexId = Guid.NewGuid(),
        AmenityId = amId,
        ComplexId = cId
      };
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      string res = "result";
      _complexRepo.Setup(r => r.CreateComplexAsync(complex))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(p => p.CreateAmenityComplexAsync(ac))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(c => c.ReadAmenityListAsync())
          .Returns(Task.FromResult(amenities));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult<ApiComplex>>(await controller.PostComplexAsync(apiComplex));

      //assert
      Assert.IsAssignableFrom<ActionResult<ApiComplex>>(model);
    }

    /// <summary>
    /// This test is to test PostRoomAsync in Complex Api
    /// </summary>
    [Fact]
    public async void PostRoomsAsyncTest()
    {
      Guid amId = Guid.NewGuid();
      Guid cId = Guid.NewGuid();
      Guid rId = Guid.NewGuid();
      ApiAmenity amenity = new ApiAmenity
      {
        AmenityId = amId,
        AmenityType = "Pool",
        Description = "swimmming"
      };
      List<ApiAmenity> amenities = new List<ApiAmenity>
      {
        amenity
      };
      Logic.AmenityRoom ar = new Logic.AmenityRoom
      {
        AmenityRoomId = Guid.NewGuid(),
        AmenityId = amId,
        RoomId = rId
      };
      ApiRoom room = new ApiRoom
      {
        RoomId = rId,
        RoomNumber = "1234",
        ComplexId = cId,
        ApiRoomType = "dorm",
        NumberOfBeds = 3,
        Gender = "default",
        LeaseStart = Convert.ToDateTime("2010/1/1"),
        LeaseEnd = Convert.ToDateTime("2020/1/1"),
        Amenities = amenities
      };
      List<ApiRoom> rooms = new List<ApiRoom>
      {
        room
      };
      IEnumerable<ApiRoom> apiRooms = rooms;
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      string res = "result";
      _complexRepo.Setup(r => r.CreateAmenityRoomAsync(ar))
          .Returns(Task.FromResult(res));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      Guid cId = Guid.NewGuid();
      Guid aId = Guid.NewGuid();
      Guid pId = Guid.NewGuid();
      Guid amId = Guid.NewGuid();
      ApiComplexAddress address = new ApiComplexAddress
      {
        AddressId = aId,
        StreetAddress = "test ave",
        City = "dallas",
        State = "TX",
        Country = "USA",
        ZipCode = "76010"
      };
      Logic.Amenity amenity = new Logic.Amenity
      {
        AmenityId = amId,
        AmenityType = "name",
        Description = "description"
      };
      List<Logic.Amenity> amenities = new List<Logic.Amenity>
      {
        amenity
      };
      ApiComplex apiComplex = new ApiComplex
      {
        ComplexId = cId,
        Address = address,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890",
        ComplexAmentiy = amenities
      };
      Logic.Complex complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "1234567890"
      };
      Logic.AmenityComplex ac = new Logic.AmenityComplex
      {
        AmenityComplexId = Guid.NewGuid(),
        AmenityId = amId,
        ComplexId = cId
      };
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      string res = "result";
      _complexRepo.Setup(r => r.DeleteAmenityComplexAsync(cId))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(r => r.UpdateComplexAsync(complex))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(c => c.ReadAmenityListAsync())
          .Returns(Task.FromResult(amenities));
      _complexRepo.Setup(p => p.CreateAmenityComplexAsync(ac))
          .Returns(Task.FromResult(res));



      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      Guid amId = Guid.NewGuid();
      Guid cId = Guid.NewGuid();
      Guid rId = Guid.NewGuid();
      ApiAmenity amenity = new ApiAmenity
      {
        AmenityId = amId,
        AmenityType = "Pool",
        Description = "swimmming"
      };
      List<ApiAmenity> amenities = new List<ApiAmenity>
      {
        amenity
      };
      Logic.AmenityRoom ar = new Logic.AmenityRoom
      {
        AmenityRoomId = Guid.NewGuid(),
        AmenityId = amId,
        RoomId = rId
      };
      ApiRoom room = new ApiRoom
      {
        RoomId = rId,
        RoomNumber = "1234",
        ComplexId = cId,
        ApiRoomType = "dorm",
        NumberOfBeds = 3,
        Gender = "default",
        LeaseStart = Convert.ToDateTime("2010/1/1"),
        LeaseEnd = Convert.ToDateTime("2020/1/1"),
        Amenities = amenities
      };
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      string res = "result";
      _complexRepo.Setup(r => r.DeleteAmenityRoomAsync(rId))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(r => r.CreateAmenityRoomAsync(ar))
          .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
      Guid cId = Guid.NewGuid();
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<ComplexController>> _logger = new Mock<ILogger<ComplexController>>();
      string res = "result";
      _complexRepo.Setup(r => r.DeleteAmenityComplexAsync(cId))
          .Returns(Task.FromResult(res));
      _complexRepo.Setup(r => r.DeleteComplexAsync(cId))
          .Returns(Task.FromResult(res));


      //act
      var controller = new ComplexController(_complexRepo.Object, _logger.Object);
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
    }
  }
}
