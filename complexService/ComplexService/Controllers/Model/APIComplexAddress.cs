﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexServiceApi.Controllers.Model
{
    public class APIComplexAddress
    {
    //AddressId = c.Address.AddressId,
    //                StreetAddress = c.Address.StreetAddress,
    //                City = c.Address.City,
    //                State = c.Address.State,
    //                ZipCode = c.Address.Zip
        public string ComplexGUID { get; set; }
        public string AddressGUID { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
  


    }
}
