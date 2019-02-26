using Xunit;

namespace sudoku.tests
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
        
        [Fact]
        public void Puzzle3_2()
        {
            // Source: http://www.7sudoku.com/very-difficult
            var puzzle = new[,] {
                {0,1,0, 0,0,9, 0,7,4},
                {7,0,0, 0,3,0, 0,0,0},
                {0,4,0, 7,2,0, 3,0,1},
                
                {0,0,0, 0,0,0, 0,0,9},
                {0,6,7, 0,0,0, 2,1,0},
                {5,0,0, 0,0,0, 0,0,0},
                
                {4,0,2, 0,7,1, 0,6,0},
                {0,0,0, 0,4,0, 0,0,3},
                {3,5,0, 6,0,0, 0,2,0}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
    }
}