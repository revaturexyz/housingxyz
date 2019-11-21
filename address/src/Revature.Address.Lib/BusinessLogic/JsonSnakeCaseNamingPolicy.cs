using System.Text.Json;
using Humanizer;

namespace Revature.Address.Lib.BusinessLogic
{
  public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
  {
    /// <summary>
    /// Configures the naming policy for Json Serializer
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override string ConvertName(string name) => name.Underscore();

  }
}
