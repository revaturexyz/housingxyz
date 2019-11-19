using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Revature.Tenant.Tests.ApiTests;
using Revature.Tenant.Api.Models;

namespace Revature.Tenant.Tests.ApiTests.ModelTests
{
  public class ApiCarTest
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange 
      var carId = 0;
      var licensePlate = "LicensPlate";
      var make = "Make";
      var model = "Model";
      var color = "Color";
      var year = "Year";
      var state = "State";
      // Act (set properties to variables through constructor)
      var apiCar = new Api.Models.ApiCar
      {
        Id = carId,
        LicensePlate = licensePlate,
        Make = make,
        Model = model,
        Color = color,
        Year = year,
        State = state
      };

      // Assert (assure the values are set)
      Assert.Equal(carId, apiCar.Id);
      Assert.Equal(licensePlate, apiCar.LicensePlate);
      Assert.Equal(make, apiCar.Make);
      Assert.Equal(model, apiCar.Model);
      Assert.Equal(year, apiCar.Year);
      Assert.Equal(state, apiCar.State);

    }

  }
}
