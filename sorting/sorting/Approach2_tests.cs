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
            var iEndOfPartition = Partition(values, pivot);

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iEndOfPartition);
        }
        

        private int Partition(int[] values, int pivot) {
            var iLargerThan = 0;
            var iLessThan = values.Length-1;
            while(true) {
                while(iLargerThan < values.Length && values[iLargerThan] <= pivot) iLargerThan++;
                while(iLessThan >= 0 && values[iLessThan] > pivot) iLessThan--;
                if (iLargerThan>=iLessThan) break;

                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }
            return iLargerThan-1;
        }

        public int[] Sort(int[] values) {
            if (values.Length < 2) return values;

            var partitions = Partition();
            
            var sortedLessThan = Sort(partitions.lessThan);
            var sortedLargerThan = Sort(partitions.largerThan);

            return          sortedLessThan
                    .Concat(partitions.equalTo)
                    .Concat(sortedLargerThan)
                    .ToArray();


            (int[] lessThan, int[] equalTo, int[] largerThan) Partition() {
                var pivot = PickPivot(values);
                return PartitionWithPivot(values, pivot);
            }
        }

        private int PickPivot(int[] values) {
            var iPivot = values.Length == 1 ? 0 : values.Length / 2;
            return values[iPivot];
        }

        private (int[] lessThan, int[] equalTo, int[] largerThan) PartitionWithPivot(int[] values, int pivot) {
            var result = new int[values.Length];
            var iLower = 0;
            var iUpper = result.Length - 1;
            
            foreach (var t in values)
                if (t < pivot)
                    result[iLower++] = t;
                else if (t > pivot)
                    result[iUpper--] = t;

            var lessThanPartition = result.Take(iLower).ToArray();
            var pivotPartition = Enumerable.Range(1, iUpper - iLower + 1).Select(_ => pivot).ToArray();
            var largerThanPartition = result.Skip(iUpper + 1).ToArray();
            
            return (lessThanPartition,pivotPartition,largerThanPartition);
        }
    }
}