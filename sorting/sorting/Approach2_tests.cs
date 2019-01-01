using System;
using System.Linq;
using Xunit;

namespace sorting
{
    public class Approach2_tests
    {
        public class Approach1_tests
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
            [InlineData(new[]{3,4,2,6,7,5}, 4, 3, 3)]
            [InlineData(new[]{3,4,2,6,7,5}, 3, 2, 4)]
            [InlineData(new[]{3,4,2,6,7,5}, 5, 4, 2)]
            [InlineData(new[]{3,4}, 4, 2, 0)]
            [InlineData(new[]{4,3}, 4, 2, 0)]
            [InlineData(new[]{3,4,5}, 4, 2, 1)]
            public void Partiton_tests(int[] values, int pivot, int expectedLowerLen, int expectedUpperLen)
            {
                var result = Partition(values, pivot);
                
                Assert.Equal(expectedLowerLen, result.lower.Length);
                Assert.True(result.lower.All(v => v <= pivot));
                
                Assert.Equal(expectedUpperLen, result.upper.Length);
                Assert.True(result.upper.All(v => v > pivot));
            }

            
            
            public int[] Sort(int[] values)
            {
                Span<int> x = values;
                throw new NotImplementedException();
            }

            private int PickPivot(int[] values) {
                var iPivot = values.Length == 1 ? 0 : values.Length / 2;
                return values[iPivot];
            }

            private (int[] lower, int[] upper) Partition(int[] values, int pivot) {
                var result = new int[values.Length];
                var iLower = 0;
                var iUpper = result.Length - 1;
                
                foreach (var t in values)
                    if (t <= pivot)
                        result[iLower++] = t;
                    else
                        result[iUpper--] = t;

                return (result.Take(iLower).ToArray(),result.Skip(iLower).ToArray());
            }
        }
    }
}