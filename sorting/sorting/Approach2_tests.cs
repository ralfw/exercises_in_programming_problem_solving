using System;
using System.Linq;
using Xunit;

namespace sorting
{
    public class Approach2_tests
    {
        [Fact]
        public void Acceptance_test_case_1()
        {
            var values = new[] {3, 10, 7, -2, 5, 10, 2, 0, 9, 4};

            var result = Sort(values);

            var expected = new[] {-2, 0, 2, 3, 4, 5, 7, 9, 10, 10};
            Assert.Equal(expected, result);
            Assert.NotSame(result, values);
        }

        [Theory]
        [InlineData(new[]{1}, new[]{1})]
        [InlineData(new[]{2,1}, new[]{1,2})]
        [InlineData(new[]{3,2,1}, new[]{1,2,3})]
        [InlineData(new[]{3,2,1,0}, new[]{0,1,2,3})]
        public void Acceptance_tests(int[] values, int[] expected)
        {
            var result = Sort(values);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Acceptance_test_case_2()
        {
            var values = new int[0];
            var result = Sort(values);
            Assert.Empty(result);
        }


        [Theory]
        [InlineData(new[]{4}, 4)]
        [InlineData(new[]{4,3}, 3)]
        [InlineData(new[]{4,3,2}, 3)]
        [InlineData(new[]{4,3,2,1}, 2)]
        [InlineData(new[]{4,3,2,1,0}, 2)]
        public void Pick_pivot_tests(int[] values, int expected)
        {
            var result = PickPivot(values);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(new[]{4}, 4, 0, 1, 0)]
        [InlineData(new[]{4,4}, 4, 0, 2, 0)]
        [InlineData(new[]{5,4}, 4, 0, 1, 1)]
        public void Partiton_tests(int[] values, int pivot, int expectedLessThanLen, int expectedEqualToLen, int expectedLargerThanLen)
        {
            var result = Partition(values, pivot);
            
            Assert.Equal(expectedLessThanLen, result.lessThan.Length);
            Assert.True(result.lessThan.All(v => v < pivot));
            
            Assert.Equal(expectedEqualToLen, result.equalTo.Length);
            Assert.True(result.equalTo.All(v => v == pivot));
            
            Assert.Equal(expectedLargerThanLen, result.largerThan.Length);
            Assert.True(result.largerThan.All(v => v > pivot));
        }
        
        
        public int[] Sort(int[] values)
        {
            throw new NotImplementedException();
        }

        
        private int PickPivot(int[] values) {
            var iPivot = values.Length == 1 ? 0 : values.Length / 2;
            return values[iPivot];
        }

        private (int[] lessThan, int[] equalTo, int[] largerThan) Partition(int[] values, int pivot) {
            var result = new int[values.Length];
            var iLower = 0;
            var iUpper = result.Length - 1;
            
            foreach (var t in values)
                if (t < pivot)
                    result[iLower++] = t;
                else if (t > pivot)
                    result[iUpper--] = t;

            return (result.Take(iLower).ToArray(),
                    Enumerable.Range(1,iUpper-iLower+1).Select(_ => pivot).ToArray(),
                    result.Skip(iUpper+1).ToArray());
        }
    }
}