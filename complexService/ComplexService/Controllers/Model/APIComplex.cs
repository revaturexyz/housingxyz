using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexServiceApi.Controllers.Model
{
    public class APIComplex
    {
        
        public APIComplexAddress Address { get; set; }
        public List<string> ComplexAmentiy { get; set; }

        [StringLength(100)]
        public string ComplexName { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        public Guid ProviderID { get; set; }

        


    }
}
