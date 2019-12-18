using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api
{
  /// <summary>
  /// Checks that the user has the given role.
  /// </summary>
  public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
  {
    private readonly ILoggerFactory _loggerFactory;

    public RoleRequirementHandler(ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   RoleRequirement requirement)
    {
      if (context.Resource is AuthorizationFilterContext mvcContext)
      {
        var logger = _loggerFactory.CreateLogger("Revature.Account.Api.Auth0Helper");
        // We just want to read the token, no management client, so we don't use the factory
        var auth = new Auth0Helper(mvcContext.HttpContext.Request, logger);

        foreach (var role in auth.Roles)
          if (role == requirement.Role)
            context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
