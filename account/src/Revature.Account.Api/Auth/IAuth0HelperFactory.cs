using Microsoft.AspNetCore.Http;

namespace Revature.Account.Api
{
  public interface IAuth0HelperFactory
  {
    public Auth0Helper Create(HttpRequest request);
  }
}
