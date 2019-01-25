using System.Diagnostics;
using Xunit;

namespace sudoku
{
    public class Acceptance_tests
    {
        [Fact]
        public void Puzzle2()
        {
            var puzzle = new[,] {
                {0, 0, 3, 0},
                {3, 0, 0, 2},
                {0, 1, 0, 0},
                {4, 0, 0, 1}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
    }


    public class SudokuSolver
    {
        public static int[,] Solve(int[,] puzzle)
        {
            return puzzle;
        }
    }
}