using Xunit;
using Xyz.Provider.Lib.Models.DistanceMatrix;

namespace Xyz.Provider.Tests.LibTests
{
  public class DurationTests
  {
    /// <summary>
    /// tests Duration constructor will assign values to feilds.
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
