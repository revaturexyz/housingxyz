using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Revature.Tenant.Tests.ApiTests;
using Revature.Tenant.Api.Models;

namespace Revature.Tenant.Tests.ApiTests.ModelTests
{
  public class ApiBatchTest
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      //Arrange
      var batchId = 1;
      var batchLanguage = "C#";
      DateTime startDate = new DateTime(2019,1,5);
      DateTime endDate = new DateTime(2019,1,5);
      var trainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");

      //Act
      var apiBatch = new ApiBatch()
      {
        Id = batchId,
        BatchLanguage = batchLanguage,
        StartDate = startDate,
        EndDate = endDate,
        TrainingCenter = trainingCenter
      };

      //Assert 
      Assert.Equal(batchId, apiBatch.Id);
      Assert.Equal(batchLanguage, apiBatch.BatchLanguage);
      Assert.Equal(startDate, apiBatch.StartDate);
      Assert.Equal(endDate, apiBatch.EndDate);
      Assert.Equal(trainingCenter, apiBatch.TrainingCenter);
    }
  }
}
