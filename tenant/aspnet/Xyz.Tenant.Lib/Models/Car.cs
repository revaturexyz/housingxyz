using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.Tenant.Lib.Models
{
  public class Car
  {
    private int _id;
    private string _licensePlate;
    private string _make;
    private string _model;
    private string _color;
    private string _year;

    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Year { get; set; }

  }
}
