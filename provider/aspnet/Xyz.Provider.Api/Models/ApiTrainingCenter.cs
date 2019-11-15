using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiTrainingCenter
  {
    public int CenterId { get; set; }

    public ApiAddress ApiAddress { get; set; }

    [StringLength(60)]
    public string CenterName { get; set; }

    [StringLength(20)]
    public string ContactNumber { get; set; }

    public ICollection<ApiProvider> ApiProvider { get; set; }
  }
}
