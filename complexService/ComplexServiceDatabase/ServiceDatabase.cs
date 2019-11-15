using System;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComplexServiceDatabase
{
    public class ServiceDatabase
    {
        private readonly ILogger _logger;

        public ServiceDatabase(ILogger<ServiceDatabase> logger)
        {
            _logger = logger;
        }


    }
}
