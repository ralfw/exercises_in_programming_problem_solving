using Xunit;

namespace sudoku
{
    public class SolutionChecker_tests
    {
        [Fact]
        public void CheckSolution_correct()
        {
            var sln = new[,] {
                {1, 2, 3, 4},
                {3, 4, 1, 2},
                {2, 1, 4, 3},
                {4, 3, 2, 1}
            };
            Assert.True(SolutionChecker.Check(sln));
        }
        
        [Fact]
        public void CheckSolution_incorrect_due_to_missing_number()
        {
            var sln = new[,] {
                {1, 2, 3, 4},
                {3, 4, 0, 2},
                {2, 1, 4, 3},
                {4, 3, 2, 1}
            };
            Assert.False(SolutionChecker.Check(sln));
        }
        
        [Fact]
        public void CheckSolution_incorrect_due_to_number_doubling()
        {
            var sln = new[,] {
                {1, 2, 3, 4},
                {3, 4, 1, 2},
                {2, 1, 1, 3},
                {4, 3, 2, 4}
            };
            Assert.False(SolutionChecker.Check(sln));
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        public void CheckRow_test(int row, bool expected) {
            var sln = new[,] {
                {1, 2, 3, 4},
                {0, 4, 1, 2},
                {4, 1, 4, 3},
                {4, 3, 2, 1}
            };
            Assert.Equal(expected, SolutionChecker.CheckRow(sln, 4, row));
        }
        
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        public void CheckCol_test(int col, bool expected) {
            var sln = new[,] {
                {1, 0, 4, 4},
                {3, 4, 1, 2},
                {2, 1, 4, 3},
                {4, 3, 2, 1}
            };
            Assert.Equal(expected, SolutionChecker.CheckCol(sln, 4, col));
        }
        
        [Theory]
        [InlineData(0,0, true)]
        [InlineData(0,2, false)]
        [InlineData(2,0, false)]
        [InlineData(2,2, true)]
        public void CheckBox_test(int row, int col, bool expected) {
            var sln = new[,] {
                {1, 2, 3, 4},
                {3, 4, 0, 2},
                {2, 4, 4, 3},
                {4, 3, 2, 1}
            };
            Assert.Equal(expected, SolutionChecker.CheckBox(sln, 2, row, col));
        }
    }
}