using System.Collections.Generic;

namespace Xyz.Provider.Lib.Models.DistanceMatrix
{
  // used as a object model for the Google API distance matrix response
  // https://developers.google.com/maps/documentation/distance-matrix/intro#DistanceMatrixResponses
  public class Response
  {
    public string Status { get; set; }
    public List<string> OriginAddresses { get; set; }
    public List<string> DestinationAddresses { get; set; }
    public List<ResponseRow> Rows { get; set; }
  }
}
