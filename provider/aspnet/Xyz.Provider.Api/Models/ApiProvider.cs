using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiProvider
  {
    public int ProviderId { get; set; }

    public ApiTrainingCenter ApiTrainingCenter { get; set; }

    public ApiAddress ApiAddress { get; set; }

    [StringLength(100)]
    public string CompanyName { get; set; }

    [StringLength(40), RegularExpression(@"[a-zA-Z]+$", ErrorMessage = "Non-ASCII-letters not allowed")]
    public string Username { get; set; }

    // Must be a minimum of 8 characters
    [StringLength(40), MinLength(8)]
    public string Password { get; set; }

    [StringLength(20), RegularExpression(@"[0-9]+$", ErrorMessage = "Non-digits not allowed")]
    public string ContactNumber { get; set; }
  }
}
