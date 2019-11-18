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
  public class ComplexApiTest
  {
    public async void GetAllAmenitiesShouldReturnOk() //not aysnc T_T
    {
      //setup
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      List<Amenity> res = new List<Amenity>();
      _complexRepo.Setup(r => r.ReadAmenityListAsync())
          .Returns(Task.FromResult(res));

      //act
      var controller = new ComplexController(_complexRepo.Object);
      var model = Assert.IsType<List<Amenity>>(controller.GetAllAmenities());

      //assert
      Assert.IsType<List<Amenity>>(model);
    }
  }
}
