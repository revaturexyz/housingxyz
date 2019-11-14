using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiGender
  {
    public int GenderId { get; set; }

    [StringLength(20), RegularExpression(@"[a-zA-Z]", ErrorMessage = "Non-ASCII-letters not allowed")]
    public string GenderType { get; set; }
  }
}
