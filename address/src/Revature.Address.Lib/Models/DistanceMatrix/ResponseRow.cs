using System.Collections.Generic;

namespace Revature.Address.Lib.Models.DistanceMatrix
{
  /// <summary>
  /// The Response is composed of a list of response elements
  /// from Distance Matrix API
  /// </summary>
  public class ResponseRow
  {
    public List<ResponseElement> Elements { get; set; }
  }
}
