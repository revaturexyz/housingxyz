using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Xyz.Address.Lib.Validators
{
  /// <summary>
  ///  Validator that ensure that Street in address follows the US Street Address format. 
  /// </summary>
  public class StreetValidatorAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      Regex rgx = new Regex(@"[0-9]+[ ][A-Za-z|\s]*");
      return rgx.IsMatch(value.ToString()) ? true : false;
    }
  }
}
