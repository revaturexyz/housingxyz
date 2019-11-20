using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Address.Lib.Models.DistanceMatrix
{
  public class Response
  {
    public string Status { get; set; }
    public List<string> OriginAddresses { get; set; }
    public List<string> DestinationAddresses { get; set; }
    public List<ResponseRow> Rows { get; set; }
  }
}
