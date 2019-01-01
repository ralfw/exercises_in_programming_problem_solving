using System;
using Xunit;

namespace sorting
{
    public class Approach1_tests
    {
                
        [Fact]
        public void Acceptance_test_case_1() {
            var values = new[] {3,10,7,-2,5,10,2,0,9,4};
            
            var result = Sort(values);

            var expected = new[] {-2, 0, 2, 3, 4, 5, 7, 9, 10, 10};
            Assert.Equal(expected, result);
            Assert.NotSame(result, values);
        }
        
        
        [Fact]
        public void Acceptance_test_case_2() {
            var values = new int[0];
            var result = Sort(values);
            Assert.Empty(result);
        }
        
        

        
        private int[] Sort(int[] values) {
            var result = (int[])values.Clone();

            while (LetLargerValuesBubbleUp().bubblingHappened){}
            
            return result;


            (bool bubblingHappened, int numberOfBubbles) LetLargerValuesBubbleUp() {
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