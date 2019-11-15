using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Xyz.Address.Api
{
  ///<summary> Class for handling exceptions and prints user-friendly error messages instead of full stack trace. 
  ///Because this is a global exception handler, this has already been registered within Startup.cs, under ConfigureServices() method.</summary>
  class GlobalExceptionHandlerAttribute : ExceptionFilterAttribute
  {
      public override void OnException(ExceptionContext context)
      {
        var exception = context.Exception;
        var result = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent("Internal Server Error Occured"), ReasonPhrase = "Exception"
        };

        context.ExceptionHandled = true;
      }
  }
}