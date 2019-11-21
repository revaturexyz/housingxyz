using Revature.Address.Lib.Models.DistanceMatrix;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
  public class DurationTests
  {
    /// <summary>
    /// tests Duration constructor will assign values to fields.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (create variables for properties)
      var text = "Some Text";
      var value = 340110d;
      // Act (initialize properties in constructor)
      var duration = new ResponseDuration
      {
        Text = text,
        Value = value
      };
      // Assert (ensure properties are set to the proper variables)
      Assert.Equal(text, duration.Text);
      Assert.Equal(value, duration.Value);
    }
  }
}
