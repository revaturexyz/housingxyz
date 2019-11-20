using System;
using Xunit;
using DatMod = Revature.Tenant.DataAccess.Entities;

namespace Revature.Tenant.Tests.DataTests
{
  public class BatchTest
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      //Arrange
      var batchId = 1;
      var batchLanguage = "C#";
      DateTime startDate = new DateTime(2019, 1, 5);
      DateTime endDate = new DateTime(2019, 1, 5);

      //Act
      var apiBatch = new DatMod.Batch()
      {
        Id = batchId,
        BatchLanguage = batchLanguage,
        StartDate = startDate,
        EndDate = endDate
      };

      //Assert 
      Assert.Equal(batchId, apiBatch.Id);
      Assert.Equal(batchLanguage, apiBatch.BatchLanguage);
      Assert.Equal(startDate, apiBatch.StartDate);
      Assert.Equal(endDate, apiBatch.EndDate);
    }
  }
}
