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
            /*
                The pivot value is the value right in the middle of a slice.
                For a discussion on picking a pivot see: https://en.wikipedia.org/wiki/Quicksort#Choice_of_pivot
            */
            var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
            var iPivot = iStartOfSlice + (lengthOfSlice == 1 ? 0 : lengthOfSlice / 2);
            return values[iPivot];
        }

        private (int iEndOfLowerPartition, int iStartOfUpperPartition) Partition(int[] values, int iStartOfSlice, int iEndOfSlice, int pivot) {
            /*
                A slice from the values array is split into three partitions:
                -all values less than the pivot
                -all values larger than the pivot
                -the pivot values
                This creates a very rough sorting of values: less < pivot < larger

                This achieved by segregating the values twice.
                1. all values less or equal (!) than the pivot vs. all values larger than the pivot
                2. only the values less than the pivot vs the pivot

                However, to the outside only the less than and larger than partitions are relevant
                because they need further sorting. That's why the literature only mentions two partitions
                and says about the pivot: "After this partitioning, the pivot is in its final position."
                (https://en.wikipedia.org/wiki/Quicksort#Algorithm)
             */
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