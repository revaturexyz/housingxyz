using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xyz.Address.Lib.Validators;
using System.Reflection;

namespace Xyz.Address.Lib.Models
{
  ///<summary>AddressModel with properties and validation methods. </summary>
  public abstract class AddressModel//: IValidatableObject
  {
    #region Constructors
    public AddressModel()
    {
      Key = new Guid();
    }

    #endregion

    #region Methods

    ///<summary> Perform validation checks on the Address Properties.  </summary>
    ///<param name="validationContext"> List of Address Properties to be validated on. </param>
    ///<return> If property is null, the Validate method returns an error message, indicating property cannot be null. </return>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      foreach (var Property in this.GetType().GetProperties()) 
      { 
        if (Property.GetValue(validationContext.ObjectInstance) is null) 
        {
          yield return new ValidationResult($"{Property.Name} cannot be empty", new [] { Property.Name });
        }
      }
    }

    #endregion
  
    #region Properties

    [Column("Key", Order = 1)]
    public Guid Key { get; set; }

    [Required(ErrorMessage = "Street is required.")]
    [StreetValidator]
    [Column("Street", Order = 2, TypeName = "nvarchar(50)")]
    public string Street { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [Column("City", Order = 3, TypeName = "nvarchar(50)")]
    public string City { get; set; }

    [Required(ErrorMessage = "State Province is required.")]
    [CityStateValidator("City", ErrorMessage = "City and State must not match. Ensure the City input field contains 'City.'")]
    [Column("StateProvince", Order = 4, TypeName = "nvarchar(50)")]
    public string StateProvince { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    [Column("Country", Order = 5, TypeName = "nvarchar(2)")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Postal Code is required.")]
    [DataType(DataType.PostalCode)]
    [MaxLength(5)]
    [Column("PostalCode", Order = 6)]
    public string PostalCode { get; set; }

    #endregion
  }
}
