using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexServiceApi.Controllers.Model
{
    public class AddressToSent
    {
        public APIComplexAddress ComplexAddress { get; set; }

        public Guid AddressGuid { get; set; }
    }
}
