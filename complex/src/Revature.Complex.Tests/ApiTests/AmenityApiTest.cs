using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Revature.Complex.Lib.Models;
using Revature.Complex.Lib.Interface;
using Xunit;
using Revature.Complex.Api.Controllers;
using System.Threading.Tasks;

namespace Revature.Complex.Tests.ApiTests
{
  public class AmenityApiTest
  {
    public async void GetAllAmenitiesShouldReturnOk() //not aysnc T_T
    {
      //setup
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      List<Amenity> res = new List<Amenity>();
      _complexRepo.Setup(r => r.ReadAmenityListAsync())
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object);
      var model = Assert.IsType<List<Amenity>>(await controller.GetAmenitiestAsync());

      //assert
      Assert.IsType<List<Amenity>>(model);
    }
  }
}
