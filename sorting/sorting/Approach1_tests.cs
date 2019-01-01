using System;
using Xunit;

namespace sorting
{
    public class Approach1_tests
    {
        [Fact]
        public void Empty_array() {
            var values = new int[0];
            var result = Sort(values);
            Assert.Empty(result);
        }
        
        [Fact]
        public void Treat_input_as_immutable() {
            var values = new[] {2};
            var result = Sort(values);
            Assert.NotSame(result, values);
            Assert.Equal(new[]{2}, result);
        }


        private int[] Sort(int[] values)
        {
            var result = (int[])values.Clone();
            return result;
        }
    }
}