using System;
using Xunit;

namespace PerchedPeacockUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int x = 1;
            if (x > 1)
                throw new Exception();
        }
    }
}
