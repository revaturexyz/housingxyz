using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.Tenant.Lib.Models
{
  public class Tenant
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _email;
    private int _addressId;
    private int _roomId;
    private int _carId;

    public int Id { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
