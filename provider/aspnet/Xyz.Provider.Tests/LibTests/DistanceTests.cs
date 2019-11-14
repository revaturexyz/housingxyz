using Xunit;
using Xyz.Provider.Lib.Models.DistanceMatrix;

namespace Xyz.Provider.Tests.LibTests
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
