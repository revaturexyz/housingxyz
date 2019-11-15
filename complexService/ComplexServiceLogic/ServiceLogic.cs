using System;
using Microsoft.Extensions.Logging;

namespace ComplexServiceLogic
{
    public class ServiceLogic
    {
        private readonly ILogger _logger;

        public ServiceLogic(ILogger<ServiceLogic> logger)
        {
            _logger = logger;
        }
    }
}
