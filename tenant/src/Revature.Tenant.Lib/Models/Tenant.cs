using System;
namespace Revature.Tenant.Lib.Models
{
  public class Tenant
  {
    private int _id;
    private string _email;
    private Guid _gender;
    private string _firstName;
    private string _lastName;
    private Guid _addressId;
    private int _roomId;
    private int _carId;

    public int Id { get; set; }
    public string Email { get; set; }
    public Guid Gender { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AddressId { get; set; }
    public int RoomId { get; set; }
    public int CarId { get; set; }
  }
}
