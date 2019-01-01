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
        
        
        [Fact]
        public void Swap_two_unsorted_values() {
            var values = new[] {3,2};
            var result = Sort(values);
            Assert.Equal(new[]{2,3}, result);
        }
        
        
        [Fact]
        public void Dont_swap_two_sorted_values() {
            var values = new[] {2,3};
            var result = Sort(values);
            Assert.Equal(new[]{2,3}, result);
        }
       
        
        [Fact]
        public void Repeat_swapping_with_single_pass() {
            var values = new[] {3,1,2};
            var result = Sort(values);
            Assert.Equal(new[]{1,2,3}, result);
        }
        
        
        [Fact]
        public void Repeat_swapping_with_multiple_passes() {
            var values = new[] {3,2,1};
            var result = Sort(values);
            Assert.Equal(new[]{1,2,3}, result);
        }
        
        
        
        private int[] Sort(int[] values) {
            var result = (int[])values.Clone();

            while (BubbleUpPass().bubblingHappened){}
            
            return result;


            (bool bubblingHappened, int numberOfBubbles) BubbleUpPass() {
                var numberOfSwaps = 0;
                for (var i = 0; i < result.Length-1; i++)
                    numberOfSwaps += SwapIfNecessary(i, i + 1) ? 1 : 0;
                return (numberOfSwaps > 0, numberOfSwaps);
            }

            bool SwapIfNecessary(int a, int b) {
                if (result[a] <= result[b]) return false;
                Swap(a,b);
                return true;
            }
            
            void Swap(int a, int b) {
                var t = result[a];
                result[a] = result[b];
                result[b] = t;
            }
        }
    }
}