using Microsoft.AspNetCore.Http;

namespace Revature.Account.Api
{
  /// <summary>
  /// Factory class for creating and Auth0Helper. Main purpose
  /// is to promote testability.
  /// </summary>
  public class Auth0HelperFactory : IAuth0HelperFactory
  {
    public Auth0Helper Create(HttpRequest request)
    {
      var auth = new Auth0Helper(request);
      auth.ConnectManagementClient();
      return auth;
    }
  }
}
