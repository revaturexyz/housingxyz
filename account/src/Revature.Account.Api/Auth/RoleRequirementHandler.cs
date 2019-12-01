using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Revature.Account.Api
{
  /// <summary>
  /// Checks that the user has the given role.
  /// </summary>
  public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   RoleRequirement requirement)
    {
      if (context.Resource is AuthorizationFilterContext mvcContext)
      {
        // We just want to read the token, no management client, so we don't use the factory
        Auth0Helper auth = new Auth0Helper(mvcContext.HttpContext.Request);

        foreach (string role in auth.Roles)
          if (role == requirement.Role)
            context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
