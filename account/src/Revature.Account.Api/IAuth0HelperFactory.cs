using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Account.Api
{
  public interface IAuth0HelperFactory
  {
    public Auth0Helper Create(HttpRequest request);
  }
}
