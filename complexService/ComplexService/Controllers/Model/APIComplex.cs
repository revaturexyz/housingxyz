using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexServiceApi.Controllers.Model
{
    public class APIComplex
    {

        public int ComplexId { get; set; }

        public string ApiAddressGUID { get; set; }

        public string  ApiProviderGUID { get; set; }

        [StringLength(100)]
        public string ComplexName { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        public ICollection<string> ApiRoomsGUID { get; set; }
    }
}
