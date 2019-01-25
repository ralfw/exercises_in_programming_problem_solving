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
                {0,0, 3,0},
                {3,0, 0,2},
                
                {0,1, 0,0},
                {4,0, 0,1}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
        
        [Fact]
        public void Puzzle3()
        {
            var puzzle = new[,] {
                {0,0,0, 2,6,0, 7,0,1},
                {6,8,0, 0,7,0, 0,9,0},
                {1,9,0, 0,0,4, 5,0,0},
                
                {8,2,0, 1,0,0, 0,4,0},
                {0,0,4, 6,0,2, 9,0,0},
                {0,5,0, 0,0,3, 0,2,8},
                
                {0,0,9, 3,0,0, 0,7,4},
                {0,4,0, 0,5,0, 0,3,6},
                {7,0,3, 0,1,8, 0,0,0}
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