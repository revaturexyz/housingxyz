using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.Lib.CustomExceptions
{
  public class NegDateException : Exception
  {
    public NegDateException()
    {
    }

    public NegDateException(string message)
        : base(message)
    {
    }

    public NegDateException(string message, Exception inner) : base(message, inner)
    {
    }
  }
}
