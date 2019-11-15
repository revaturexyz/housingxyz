using System.Collections.Generic;
using Xunit;
using Xyz.Provider.Lib.Models.DistanceMatrix;

namespace Xyz.Provider.Tests.LibTests
{
  public class DistanceMatrixResponseTests
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (set up variables for the Distance Matrix)
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
      var row1 = new ResponseRow
      {
        Elements = new List<ResponseElement> { element, element }
      };
      var row2 = new ResponseRow
      {
        Elements = new List<ResponseElement> { element, element }
      };
      // Act (create a DistanceMatrixResponse object by passing in parameters)
      var response = new Response
      {
        DestinationAddresses = new List<string> { "2124 Parker St.", "2399 Prospect St." },
        OriginAddresses = new List<string> { "2132 Oxford St.", "2216 Blake St." },
        Rows = new List<ResponseRow> { row1, row2 },
        Status = status
      };
      // Assert (ensure the properties are set to the values)
      Assert.Equal(2, response.DestinationAddresses.Count);
      Assert.Equal(2, response.OriginAddresses.Count);
      Assert.Equal(2, response.Rows.Count);
      Assert.Equal(status, response.Status);
    }
  }
}
