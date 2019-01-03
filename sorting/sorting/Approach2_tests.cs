using System;
using System.Linq;
using Xunit;

namespace sorting
{
    public class Approach2_tests
    {
        [Fact(Skip="Infinite recursion...")]
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
        [InlineData(new[]{4,3}, 3, new[]{3,4}, 0)]
        [InlineData(new[]{4,3}, 4, new[]{4,3}, 1)]
        [InlineData(new[]{3,7,4,5,1,5,8}, 5, new[]{3,5,4,5,1,7,8}, 4)]
        public void In_place_partitioning(int[] values, int pivot, int[] expectedValues, int expectedEndOfLessThanPartition) {
            var iEndOfPartition = Partition(values, 0, values.Length-1, pivot);

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iEndOfPartition);
        }


        [Theory]
        [InlineData(new[]{4,3}, 3, new[]{3,4}, -1, 1)]
        [InlineData(new[]{4,3}, 4, new[]{3,4}, 0, 2)]
        [InlineData(new[]{4,3,4}, 4, new[]{3,4,4}, 0, 3)]
        [InlineData(new[]{3,7,4,5,1,5,8}, 5, new[]{3,1,4,5,5,7,8}, 2, 5)]
        public void Two_phase_partitioning(int[] testvalues, int testpivot, int[] expectedValues, int expectedEndOfLowerPartition, int expectedStartOfUpperPartition) {
            var result  = Partition(testvalues, 0, testvalues.Length-1, testpivot);

            Assert.Equal(expectedValues, testvalues);
            Assert.Equal(expectedEndOfLowerPartition, result.iEndOfLowerPartition);
            Assert.Equal(expectedStartOfUpperPartition, result.iStartOfUpperPartition);


            (int iEndOfLowerPartition, int iStartOfUpperPartition) Partition(int[] values, int iStartOfSlice, int iEndOfSlice, int pivot) {
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
        

        public int[] Sort(int[] values) {
            var result = (int[])values.Clone();
            Sort(result, 0, result.Length-1);
            return result;
        }

        private void Sort(int[] values, int iStartOfSlice, int iEndOfSlice) {
            var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
            if (lengthOfSlice < 2) return;

            var pivot = PickPivot(values, iStartOfSlice, iEndOfSlice);
            var iEndOfLowerPartition = Partition(values, iStartOfSlice, iEndOfSlice, pivot);

            Sort(values, iStartOfSlice, iEndOfLowerPartition);
            Sort(values, iEndOfLowerPartition+1, iEndOfSlice);
        }

        private int PickPivot(int[] values, int iStartOfSlice, int iEndOfSlice) {
            var lengthOfSlice = iEndOfSlice - iStartOfSlice + 1;
            var iPivot = lengthOfSlice == 1 ? 0 : lengthOfSlice / 2;
            return values[iPivot];
        }

        private int Partition(int[] values, int iStartOfSlice, int iEndOfSlice, int pivot) {
            var iLargerThan = iStartOfSlice;
            var iLessThan = iEndOfSlice;
            while(true) {
                while(iLargerThan <= iEndOfSlice && values[iLargerThan] <= pivot) iLargerThan++;
                while(iLessThan >= iStartOfSlice && values[iLessThan] > pivot) iLessThan--;

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