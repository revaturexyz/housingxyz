
namespace Revature.Address.Lib.Models.DistanceMatrix
{

  /// <summary>
  /// This class contains a status, a duration response, and a distance response
  /// </summary>
  public class ResponseElement
  {
    public string Status { get; set; }
    public ResponseDuration Duration { get; set; }
    public ResponseDistance Distance { get; set; }
  }
}
