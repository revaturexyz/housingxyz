using System.Collections.Generic;

namespace Revature.Address.Lib.Models.DistanceMatrix
{
  /// <summary>
  /// Class for receiving information from the Distance
  /// Matrix API request. It contains a status, list of origin
  /// and destination addresses, and a list of response rows
  /// containing distance matrix results
  /// </summary>
  public class Response
  {
    public string Status { get; set; }
    public List<string> OriginAddresses { get; set; }
    public List<string> DestinationAddresses { get; set; }
    public List<ResponseRow> Rows { get; set; }
  }
}
