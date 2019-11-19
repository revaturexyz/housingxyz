using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Room.Lib.Models
{
  public class ComplexMessage
  {
    //The room you are going to send
    public Room room { get; set; }

    //The operation type is what CRUD operation you are sending to the Room service
    //We are only receiving Create, Update, and Delete.
    //For the operationType it goes as follows
    //Create: 0, Update: 1, Delete: 2
    public int operationType { get; set; }
  }
}
