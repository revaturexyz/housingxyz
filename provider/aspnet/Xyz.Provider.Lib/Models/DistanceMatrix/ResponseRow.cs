using System.Collections.Generic;

namespace Xyz.Provider.Lib.Models.DistanceMatrix
{
  // used as a object model for the Google API distance matrix response
  public class ResponseRow
  {
    public List<ResponseElement> Elements { get; set; }
  }
}
