using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api
{
  /// <summary>
  /// Factory class for creating and Auth0Helper. Main purpose
  /// is to promote testability.
  /// </summary>
  public class Auth0HelperFactory : IAuth0HelperFactory
  {
    private readonly ILoggerFactory _loggerFactory;

    public Auth0HelperFactory(ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));
    }

    public Auth0Helper Create(HttpRequest request)
    {
      var logger = _loggerFactory.CreateLogger("Revature.Account.Api.Auth0Helper");
      var auth = new Auth0Helper(request, logger);
      auth.ConnectManagementClient();
      return auth;
    }
  }
}
