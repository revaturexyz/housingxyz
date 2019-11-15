using System.Text.Json;
using Humanizer;

namespace Xyz.Provider.Lib.BusinessLogic
{
  public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
  {
    public override string ConvertName(string name) => name.Underscore();
  }
}
