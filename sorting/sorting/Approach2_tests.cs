using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace sorting
{
    public class Approach2_tests
    {
        [Theory]
        [InlineData(new int[0], new int[0])]
        [InlineData(new int[]{3,2,1}, new int[]{1,2,3})]
        [InlineData(new int[]{2,3,1}, new int[]{1,2,3})]
        [InlineData(new int[]{2,3,4,1}, new int[]{1,2,3,4})]
        [InlineData(new int[]{2,3,4,1,4}, new int[]{1,2,3,4,4})]
        [InlineData(new[]{3, 10, 7, -2, 5, 10, 2, 0, 9, 4}, new[]{-2, 0, 2, 3, 4, 5, 7, 9, 10, 10}, Skip="")]
        public void Acceptance_test_case_1(int[] values, int[] expected) {
            Debug.WriteLine($"-----{string.Join(",", values.Select(v=>v.ToString()))}-----");
            var result = Sort(values);

            Assert.Equal(expected, result);
            Assert.NotSame(result, values);
        }

        [Theory]
        [InlineData(new[]{4,3}, 3, new[]{3,4}, -1, 1)]
        [InlineData(new[]{4,3}, 4, new[]{3,4}, 0, 2)]
        [InlineData(new[]{4,3,2}, 3, new[]{2,3,4}, 0, 2)]
        [InlineData(new[]{4,3,4}, 4, new[]{3,4,4}, 0, 3)]
        [InlineData(new[]{3,7,4,5,1,5,8}, 5, new[]{3,1,4,5,5,7,8}, 2, 5)]
        [InlineData(new[] {3,10,7,-2,5,10,2,0,9,4}, 10, new[]{3,4,7,-2,5,9,2,0,10,10}, 7, 10)]
        public void Two_phase_partitioning(int[] testvalues, int testpivot, int[] expectedValues, int expectedEndOfLowerPartition, int expectedStartOfUpperPartition) {
            var result  = Partition(testvalues, 0, testvalues.Length-1, testpivot);

            Assert.Equal(expectedValues, testvalues);
            Assert.Equal(expectedEndOfLowerPartition, result.iEndOfLowerPartition);
            Assert.Equal(expectedStartOfUpperPartition, result.iStartOfUpperPartition);
        }

        [Fact]
        public void Two_phase_part_error() {
            var values = new[]{ -2,0,4,3,2 };

            var pivot = PickPivot(values, 2, 4);
            Assert.Equal(3, pivot);
            var result = Partition(values, 2, 4, 3);
            Assert.Equal(2, result.iEndOfLowerPartition);
            Assert.Equal(4, result.iStartOfUpperPartition);
        }
        

        public int[] Sort(int[] values) {
            var result = (int[])values.Clone();
            Sort(result, 0, result.Length-1);
            return result;
        }

        private void Sort(int[] values, int iStartOfSlice, int iEndOfSlice) {
            Debug.WriteLine($"{iStartOfSlice}/{iEndOfSlice} // {string.Join(",", values.Where((_,i)=>iStartOfSlice<=i && i<=iEndOfSlice).Select(v=>v.ToString()))}");
            var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
            if (iStartOfSlice < 0 || iEndOfSlice >= values.Length || lengthOfSlice < 2) return;

            var pivot = PickPivot(values, iStartOfSlice, iEndOfSlice);
            var partitions = Partition(values, iStartOfSlice, iEndOfSlice, pivot);

            Sort(values, iStartOfSlice, partitions.iEndOfLowerPartition);
            Sort(values, partitions.iStartOfUpperPartition, iEndOfSlice);
        }

        private int PickPivot(int[] values, int iStartOfSlice, int iEndOfSlice) {
            var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
            var iPivot = iStartOfSlice + (lengthOfSlice == 1 ? 0 : lengthOfSlice / 2);
            return values[iPivot];
        }

        private (int iEndOfLowerPartition, int iStartOfUpperPartition) Partition(int[] values, int iStartOfSlice, int iEndOfSlice, int pivot) {
            var iEndOfLowerPartition = SeggregateValues(iStartOfSlice, iEndOfSlice, true);
            var iEndOfLowerPartitionWithoutPivot = SeggregateValues(iStartOfSlice, iEndOfLowerPartition, false);

            return (iEndOfLowerPartitionWithoutPivot, iEndOfLowerPartition+1);


            int SeggregateValues(int iStart, int iEnd, bool includePivotInLower) {
                var iLargerThan = iStart;
                var iLessThan = iEnd;

                while(true) {
                    while(iLargerThan <= iEnd && 
                          (includePivotInLower && values[iLargerThan] <= pivot 
                           || !includePivotInLower && values[iLargerThan] < pivot)) 
                        iLargerThan++;
                    while(iLessThan >= iStart && 
                          (includePivotInLower && values[iLessThan] > pivot 
                           || !includePivotInLower && values[iLessThan] >= pivot)) 
                        iLessThan--;

                    if (iLargerThan>=iLessThan) break;

                    Swap(iLargerThan, iLessThan);
                }
                return iLargerThan-1;


                void Swap(int i, int j) {
                    var t = values[i];
                    values[i] = values[j];
                    values[j] = t;
                }
            }
        }
    }
}