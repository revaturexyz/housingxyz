using Revature.Address.Lib.Models.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
  public class DistanceTests
  {
    /// <summary>
    /// tests Distance constructor will assign values to feilds.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (create variables for constructor)
      var text = "Some Text";
      var value = 1734542d;
      // Act (initialize properties with variables in constructor)
      var distance = new ResponseDistance
      {
        Text = text,
        Value = value
      };
      // Assert (ensure the properties are set to the proper variables)
      Assert.Equal(text, distance.Text);
      Assert.Equal(value, distance.Value);
    }
  }
}
