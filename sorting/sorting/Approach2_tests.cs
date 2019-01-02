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
        

        [Fact]
        public void tddaiymi_partition_two_values_wrong_order() {
            // no partitioning for array with less than 2 elements!
            var values = new[]{4,3};
            var pivot = 3;

            var expectedValues = new[]{3,4};
            var expectedEndOfLessThanPartition = 0;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            // both loops will stop at the pivot at latest
            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            var t = values[iLargerThan];
            values[iLargerThan] = values[iLessThan];
            values[iLessThan] = t;

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan);
        }

        [Fact]
        public void tddaiymi_partition_two_values_right_order() {
            // no partitioning for array with less than 2 elements!
            var values = new[]{3,4};
            var pivot = 3;

            var expectedValues = new[]{3,4};
            var expectedEndOfLessThanPartition = 0;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            // both loops will stop at the pivot at latest
            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }

        [Fact]
        public void tddaiymi_partition_three_values_wrong_order() {
            var values = new[]{4,3,2};
            var pivot = 3;

            var expectedValues = new[]{2,3,4};
            var expectedEndOfLessThanPartition = 1;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }

        [Fact]
        public void tddaiymi_partition_three_values_right_order() {
            var values = new[]{2,3,4};
            var pivot = 3;

            var expectedValues = new[]{2,3,4};
            var expectedEndOfLessThanPartition = 1;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            while(values[iLargerThan] <= pivot) iLargerThan++;
            while(values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }

        [Fact]
        public void tddaiymi_partition_three_values_with_pivot_being_highest() {
            var values = new[]{2,3,1};
            var pivot = 3;

            var expectedValues = new[]{2,3,1};
            var expectedEndOfLessThanPartition = 2;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            while(iLargerThan < values.Length && values[iLargerThan] <= pivot) iLargerThan++;
            while(iLessThan >= 0 && values[iLessThan] > pivot) iLessThan--;

            Assert.False(iLargerThan<iLessThan);
            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }

        [Fact]
        public void tddaiymi_partition_three_values_with_pivot_being_lowest() {
            var values = new[]{2,3,1};
            var pivot = 1;

            var expectedValues = new[]{1,3,2};
            var expectedEndOfLessThanPartition = 0;

            var iLargerThan = 0;
            var iLessThan = values.Length-1;

            while(iLargerThan < values.Length && values[iLargerThan] <= pivot) iLargerThan++;
            while(iLessThan >= 0 && values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            while(iLargerThan < values.Length && values[iLargerThan] <= pivot) iLargerThan++;
            while(iLessThan >= 0 && values[iLessThan] > pivot) iLessThan--;

            if (iLargerThan<iLessThan) {
                var t = values[iLargerThan];
                values[iLargerThan] = values[iLessThan];
                values[iLessThan] = t;
            }

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }
        

        [Fact]
        public void tddaiymi_partition_acceptance() {
            var values = new[]{3,7,4,5,1,8};
            var pivot = 5;

            var expectedValues = new[]{3,1,4,5,7,8};
            var expectedEndOfLessThanPartition = 3;

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

            Assert.Equal(expectedValues, values);
            Assert.Equal(expectedEndOfLessThanPartition, iLargerThan-1);
        }

        [Fact]
        public void tddaiymi_partition_acceptance_with_multiple_pivot_values() {
            var values = new[]{3,7,4,5,1,5,8};
            var pivot = 5;

            var expectedValues = new[]{3,5,4,5,1,7,8};
            var expectedEndOfLessThanPartition = 4;

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