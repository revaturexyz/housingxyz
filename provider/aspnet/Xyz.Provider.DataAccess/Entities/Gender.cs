using System.Collections.Generic;

namespace Xyz.Provider.DataAccess.Entities
{
  public partial class Gender
  {
    /// <summary>
    /// Constructs Gender and initializes navigation properties to default values
    /// </summary>
    public Gender()
    {
      Room = new HashSet<Room>();
    }

    /// <summary>
    /// Primary key of Gender row
    /// </summary>
    public int GenderId { get; set; }

    /// <summary>
    /// GenderType cell of Gender row
    /// </summary>
    public string GenderType { get; set; }

    public virtual ICollection<Room> Room { get; set; }
  }
}
