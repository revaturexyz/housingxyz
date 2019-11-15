using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revature.Room.DataAccess.Entities
{
  public class Gender
  {
    [Key]
    public string Type { get; set; }
  }
}
