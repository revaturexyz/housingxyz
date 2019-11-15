using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Xyz.Address.Lib.Validators
{
    ///<summary>Validatator to ensure that City and State values are not equal.
    ///For example, we want to specify "New York City, New York", not "New York, New York."
    ///</summary>
    public class CityStateValidatorAttribute : ValidationAttribute
    {
      private readonly string _City;

      public CityStateValidatorAttribute(string City)
      {
        _City = City;
      }

      ///<summary>Compares StateProvince value with respect to the City value
      ///Clarification for parameters in IsValid() method:
      ///value corresponds to StateProvince, validationContext corresponds to CityContext Information</summary>
      ///<param name="value"></param>
      ///<param name="validationContext"></param>
      ///<return>Success if validation succeeds. Otherwise, an error message will display</return>
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
        var CityInfo = validationContext.ObjectType.GetProperty(_City); //Obtain City Type and Properties. 
        var CityValue = CityInfo.GetValue(validationContext.ObjectInstance); //Obtain City Value. 

        if(value.ToString().Equals(CityValue.ToString())) 
        {
          return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
      }

      
    }
}