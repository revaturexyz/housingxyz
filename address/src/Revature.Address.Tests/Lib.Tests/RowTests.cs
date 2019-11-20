using Revature.Address.Lib.Models.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
  public class RowTests
  {
    /// <summary>
    /// test that Row model is contructed and values are
    /// assigned to fields. Distance, Duration, and Element
    /// models make up Row model.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (set up variables for row constructor)
      var text = "Some Text";
      var value = 1734542d;
      var distance = new ResponseDistance
      {
        Text = text,
        Value = value
      };
      var duration = new ResponseDuration
      {
        Text = text + value,
        Value = value
      };
      var status = "Single";
      var element = new ResponseElement
      {
        Distance = distance,
        Duration = duration,
        Status = status
      };
      // Act (create row with variables)
      var row = new ResponseRow
      {
        Elements = new List<ResponseElement> { element }
      };
      // Assert (ensure model properties are set to variables)
      Assert.NotNull(row.Elements);
      Assert.Single(row.Elements);
      Assert.Equal(element, row.Elements[0]);
    }
  }
}
