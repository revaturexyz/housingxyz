using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Address.Lib.Models.DistanceMatrix
{

  /// <summary>
  /// This class contains a value field and
  /// a text field that displays the
  /// formatted duration value
  /// </summary>
  public class ResponseDuration
  {
    public double Value { get; set; }
    public string Text { get; set; }
  }
}
