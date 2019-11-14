using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class Provider
  {
    public Provider()
    {
      Complex = new HashSet<Complex>();
      Notification = new HashSet<Notification>();
    }

    public int ProviderId { get; set; }
    public int CenterId { get; set; }
    public int AddressId { get; set; }
    public string CompanyName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ContactNumber { get; set; }

    public virtual Address Address { get; set; }
    public virtual TrainingCenter Center { get; set; }
    public virtual ICollection<Complex> Complex { get; set; }
    public virtual ICollection<Notification> Notification { get; set; }
  }
}
