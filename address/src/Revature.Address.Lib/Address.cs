using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Revature.Address.Lib
{
    public class Address
    {

    private int _id { get; set; }
    private string _street { get; set; }
    private string _city { get; set; }
    private string _state { get; set; }
    private string _country { get; set; }
    private string _zipCode { get; set; }

    [Column("Key", Order = 1)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Street is required.")]
    [Column("Street", Order = 2, TypeName = "nvarchar(50)")]
    public string Street { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [Column("City", Order = 3, TypeName = "nvarchar(50)")]
    public string City { get; set; }

    [Required(ErrorMessage = "State Province is required.")]
    [Column("StateProvince", Order = 4, TypeName = "nvarchar(50)")]
    public string State { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    [Column("Country", Order = 5, TypeName = "nvarchar(2)")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Postal Code is required.")]
    [DataType(DataType.PostalCode)]
    [MaxLength(5)]
    [Column("PostalCode", Order = 6)]
    public string ZipCode { get; set; }

  }
}
