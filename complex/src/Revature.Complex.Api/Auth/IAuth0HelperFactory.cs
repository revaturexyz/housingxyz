using Microsoft.AspNetCore.Http;

namespace Revature.Complex.Api
{
  public interface IAuth0HelperFactory
  {
    public Auth0Helper Create(HttpRequest request);
  }
}
