using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Address.Lib.Models.DistanceMatrix
{
  /// <summary>
  /// The Response is composedd of a list of response elements
  /// from Distance Matrix API
  /// </summary>
  public class ResponseRow
  {
    public List<ResponseElement> Elements { get; set; }
  }
}
