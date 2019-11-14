using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xyz.Provider.Api.Models
{
  public class ApiComplex
  {
    public int ComplexId { get; set; }

    public ApiAddress ApiAddress { get; set; }

    public ApiTrainingCenter ApiTrainingCenter { get; set; }

    public ApiProvider ApiProvider { get; set; }

    [StringLength(100)]
    public string ComplexName { get; set; }

    [StringLength(20)]
    public string ContactNumber { get; set; }

    public ICollection<ApiRoom> ApiRooms { get; set; }
  }
}
