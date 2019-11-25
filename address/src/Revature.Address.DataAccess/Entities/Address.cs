using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Revature.Address.DataAccess.Entities
{
  /// <summary>
  /// 
  /// </summary>
  public partial class Address
  {
    [Key]
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }
  }
}
