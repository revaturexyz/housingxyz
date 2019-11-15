using Xunit;
using Xyz.Provider.Lib.Models.DistanceMatrix;

namespace Xyz.Provider.Tests.LibTests
{
  public class ElementTests
  {
    /// <summary>
    /// tests Element constructor will assign values to feilds.
    /// Distance, and Duration model are apart of Element.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (create variables for model properties)
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
      // Act (create model with variables)
      var element = new ResponseElement
      {
        Distance = distance,
        Duration = duration,
        Status = status
      };
      // Assert (ensure the properties are set to the proper variables)
      Assert.Equal(distance, element.Distance);
      Assert.Equal(duration, element.Duration);
      Assert.Equal(status, element.Status);
    }
  }
}
