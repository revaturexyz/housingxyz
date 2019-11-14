using System;
using System.Collections.Generic;
using System.Text;
using r = Revature.Room.Api.Models;

namespace Revature.Room.Processor
{
  public interface IProcessData
  {
    public void Process(r.Room busRoom);
  }
}
