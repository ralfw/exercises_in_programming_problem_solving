using System;
using Xunit;

namespace sorting
{
    public class Approach1_tests
    {
        [Fact]
        public void Empty_array()
        {
            var values = new int[0];
            var result = Sort(values);
            Assert.Empty(result);
        }


        private int[] Sort(int[] values)
        {
            return values;
        }
    }
}