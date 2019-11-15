using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ComplexServiceApi
{
    public class TestClass
    {
        private readonly ILogger _logger;

        public TestClass(ILogger<TestClass> logger)
        {
            _logger = logger;
        }


        public int TestTheTestSuit(int x, int y)
        {
            return x + y;
        }
    }
}
