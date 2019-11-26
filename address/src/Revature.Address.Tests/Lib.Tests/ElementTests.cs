using Revature.Address.Lib.Models.DistanceMatrix;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
  /// <summary>
  /// Contains tests for ResponseElement class
  /// </summary>
  public class ElementTests
  {
    /// <summary>
    /// tests Element constructor will assign values to fields.
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
