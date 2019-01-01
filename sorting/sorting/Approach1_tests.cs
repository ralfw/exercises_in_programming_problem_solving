using System;
using Xunit;

namespace sorting
{
    public class Approach1_tests
    {
                
        [Fact]
        public void Acceptance_test() {
            var values = new[] {3,10,7,-2,5,10,2,0,9,4};
            
            var result = Sort(values);

            var expected = new[] {-2, 0, 2, 3, 4, 5, 7, 9, 10, 10};
            Assert.Equal(expected, result);
        }
        
        
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