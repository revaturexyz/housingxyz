namespace Xyz.Provider.Lib.Models.DistanceMatrix
{
  // used as a object model for the Google API distance matrix response
  public class ResponseElement
  {
    public string Status { get; set; }
    public ResponseDuration Duration { get; set; }
    public ResponseDistance Distance { get; set; }
  }
}
