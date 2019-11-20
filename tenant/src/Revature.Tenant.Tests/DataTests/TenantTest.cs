using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DatMod = Revature.Tenant.DataAccess.Entities;

namespace Revature.Tenant.Tests.DataTests
{
  public class TenantTest
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange 
      var tenantId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");
      var email = "firstname@email.com";
      var gender = "Male";
      var firstName = "Colt";
      var lastName = "Clary";
      var addressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67c");
      var roomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67b");
      var carId = 3;
      var batchId = 3;
      var trainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67b");
      // Act (set properties to variables through constructor)
      var apiTenant = new DatMod.Tenant
      {
        Id = tenantId,
        Email = email,
        Gender = gender,
        FirstName = firstName,
        LastName = lastName,
        AddressId = addressId,
        RoomId = roomId,
        CarId = carId,
        BatchId = batchId,
        TrainingCenter = trainingCenter
      };
      // Assert (assure the values are set)

      Assert.Equal(tenantId, apiTenant.Id);
      Assert.Equal(email, apiTenant.Email);
      Assert.Equal(gender, apiTenant.Gender);
      Assert.Equal(firstName, apiTenant.FirstName);
      Assert.Equal(lastName, apiTenant.LastName);
      Assert.Equal(addressId, apiTenant.AddressId);
      Assert.Equal(roomId, apiTenant.RoomId);
      Assert.Equal(carId, apiTenant.CarId);
      Assert.Equal(batchId, apiTenant.BatchId);
      Assert.Equal(trainingCenter, apiTenant.TrainingCenter);
    }
  }
}
