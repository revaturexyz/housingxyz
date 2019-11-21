using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Models
{
  public class ApiComplexAddress
  {
    /// <summary>
    /// Address Id of Api Address model
    /// </summary>
    public Guid AddressId { get; set; }
    /// <summary>
    /// Complex Id of Api Address model
    /// </summary>
    public Guid ComplexId { get; set; }

    /// <summary>
    /// Street address of Api Address model
    /// </summary>
    public string StreetAddress { get; set; }

    /// <summary>
    /// City name of Api Address model
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// State Name of Api Addess model
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Country name of Api Address model
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Zip code of Api Address model
    /// </summary>
    public string ZipCode { get; set; }
  }
}
