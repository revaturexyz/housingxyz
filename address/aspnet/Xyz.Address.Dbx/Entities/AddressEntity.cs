using System;
using System.ComponentModel.DataAnnotations;
using Xyz.Address.Lib.Models;


namespace Xyz.Address.Dbx.Models
{
  ///<summary>Create concrete address entity for Database Repository Pattern. Inherits from AddressModel</summary>
  public class AddressEntity : AddressModel
  {
    #region Constructors

    public AddressEntity()
    {
      DateCreated = DateTime.Now;
      DateModified = DateTime.Now;
    }

    #endregion

    #region Properties
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateCreated { get;}
    [Required]
    public DateTime DateModified { get; set; }
    [Key]
    public int Id { get; set; }

    #endregion
  }
}
