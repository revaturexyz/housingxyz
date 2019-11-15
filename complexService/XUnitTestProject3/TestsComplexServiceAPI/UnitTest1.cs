using System;
using ComplexServiceApi;
using Xunit;

namespace XUnitTestProject3
{
    public class UnitTest1
    {
        [Fact]
        public void TestTheTestSuitReturnsSum()
        {
            TestClass test = new TestClass();

            //arrange
            int x = 5;
            int y = 6;
            int expected = x + y;

            //act
            var actual = test.TestTheTestSuit(x, y);

            //assert
            Assert.Equal(expected:expected, actual: actual);
        }





    }
}
