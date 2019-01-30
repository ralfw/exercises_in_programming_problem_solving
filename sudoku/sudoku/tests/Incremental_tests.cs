using Xunit;

namespace sudoku.tests
{
    public class Incremental_tests
    {
        [Fact]
        public void Level1()
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
        
        
        [Fact(Skip = "too difficult")]
        public void Level2()
        {
            var puzzle = new[,] {
                {0,2, 0,0},
                {3,0, 0,2},
                
                {0,1, 0,3},
                {4,0, 0,0}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
        
        
        [Fact(Skip = "too difficult")]
        public void Level3()
        {
            var puzzle = new[,] {
                {6,0,0, 0,0,1, 4,8,1},
                {0,0,0, 0,0,0, 0,0,0},
                {2,0,0, 0,0,0, 6,0,5},
                
                {0,0,3, 0,0,7, 0,0,0},
                {0,9,0, 6,5,0, 2,7,8},
                {0,0,0, 9,0,0, 0,6,0},
                
                {3,0,7, 0,8,2, 9,4,0},
                {4,0,9, 0,1,0, 0,5,0},
                {5,0,0, 0,0,9, 7,0,0}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
    }
}