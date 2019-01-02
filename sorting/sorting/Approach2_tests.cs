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
        

        public int[] Sort(int[] values) {
            var result = (int[])values.Clone();
            Sort(result, 0, result.Length-1);
            return result;
        }

        private void Sort(int[] values, int iStartOfPartition, int iEndOfPartition) {
            var lengthOfPartition = iEndOfPartition - iStartOfPartition + 1;
            if (lengthOfPartition < 2) return;

            var pivot = PickPivot(values, iStartOfPartition, iEndOfPartition);
            var iEndOfLowerPartition = Partition(values, iStartOfPartition, iEndOfPartition, pivot);
            Console.WriteLine($"{iStartOfPartition}-{iEndOfPartition} / {pivot} / {iEndOfLowerPartition}");

            Sort(values, iStartOfPartition, iEndOfLowerPartition);
            Sort(values, iEndOfLowerPartition+1, iEndOfPartition);
        }

        private int PickPivot(int[] values, int iStartOfPartition, int iEndOfPartition) {
            var lengthOfPartition = iEndOfPartition - iStartOfPartition + 1;
            var iPivot = lengthOfPartition == 1 ? 0 : lengthOfPartition / 2;
            return values[iPivot];
        }

        private int Partition(int[] values, int iStartOfPartition, int iEndOfPartition, int pivot) {
            var iLargerThan = iStartOfPartition;
            var iLessThan = iEndOfPartition;
            while(true) {
                while(iLargerThan <= iEndOfPartition && values[iLargerThan] <= pivot) iLargerThan++;
                while(iLessThan >= iStartOfPartition && values[iLessThan] > pivot) iLessThan--;
                if (iLargerThan>=iLessThan) break;

                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }
            return iLargerThan-1;
        }
    }
}