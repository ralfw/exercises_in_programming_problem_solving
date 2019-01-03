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
        [InlineData(new[]{3, 10, 7, -2, 5, 10, 2, 0, 9, 4}, new[]{-2, 0, 2, 3, 4, 5, 7, 9, 10, 10}, Skip="")]
        public void Acceptance_test_case_1(int[] values, int[] expected) {
            Debug.WriteLine($"-----{string.Join(",", values.Select(v=>v.ToString()))}-----");
            var result = Sort(values);

            Assert.Equal(expected, result);
            Assert.NotSame(result, values);
        }


        public int[] Sort(int[] values) {
            var result = (int[])values.Clone();
            Sort(result, 0, result.Length-1);
            return result;
        }

        private void Sort(int[] values, int iStartOfSlice, int iEndOfSlice) {
            Descend(() => {
                var pivot = PickPivot(values, iStartOfSlice, iEndOfSlice);
                var partitions = Partition(values, iStartOfSlice, iEndOfSlice, pivot);

                Sort(values, iStartOfSlice, partitions.iEndOfLowerPartition);
                Sort(values, partitions.iStartOfUpperPartition, iEndOfSlice);
            });


            void Descend(Action run) {
                var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
                if (iStartOfSlice < 0 || iEndOfSlice >= values.Length || lengthOfSlice < 2) return;
                run();
            }
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