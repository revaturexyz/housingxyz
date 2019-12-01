using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Revature.Account.Api
{
  /// <summary>
  /// Simple requirement which takes a role.
  /// </summary>
  public class RoleRequirement : IAuthorizationRequirement
  {
    public string Role { get; }

    public RoleRequirement(string role)
    {
      Role = role;
    }
  }
}
