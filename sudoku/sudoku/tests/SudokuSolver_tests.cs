using Xunit;

namespace sudoku.tests
{
    public class SudokuSolver_tests
    {
        [Fact]
        public void Constrain_leads_to_fixes() {
            var puzzle = new[,] {
                {1,2, 3,4},
                {3,4, 0,2},
                
                {2,1, 4,3},
                {0,3, 2,1}
            };
            var wb = new Workbench(puzzle);

            Assert.True(SudokuSolver.TryConstrain(wb, out var result));
            
            Assert.Equal(2, result);
        }
        
        
        [Fact]
        public void Constrain_leads_to_no_fixes() {
            var puzzle = new[,] {
                {1,2, 3,4},
                {3,4, 1,2},
                
                {2,1, 4,3},
                {4,3, 2,1}
            };
            var wb = new Workbench(puzzle);
            
            Assert.True(SudokuSolver.TryConstrain(wb, out var result));
            
            Assert.Equal(0, result);
        }
    }
}