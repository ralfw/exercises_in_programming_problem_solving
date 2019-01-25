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
        public void CheckSolution_incorrect_due_to_number_doubling_in_rows()
        {
            var sln = new[,] {
                {1, 2, 3, 4},
                {3, 4, 1, 2},
                {2, 1, 1, 3},
                {4, 3, 2, 4}
            };
            Assert.False(SolutionChecker.Check(sln));
        }
    }
}