using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Address.Lib.Models.DistanceMatrix
{
  public class ResponseElement
  {
    public string Status { get; set; }
    public ResponseDuration Duration { get; set; }
    public ResponseDistance Distance { get; set; }
  }
}
