using System.Linq;
using Xunit;

namespace sudoku.tests
{
    public class Constraining_tests
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
            
            Assert.Equal(Constraining.Status.Success, Constraining.Constrain(wb));
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
            
            Assert.Equal(Constraining.Status.DeadEnd, Constraining.Constrain(wb));
        }
        
        [Fact]
        public void Constrain_leads_to_non_unique_fixes() {
            var puzzle = new[,] {
                {6,3,5, 2,9,1, 4,8,7},
                {9,4,0, 3,0,5, 1,0,0},
                {2,0,0, 0,7,4, 6,0,5},
                
                {8,6,3, 1,2,7, 5,9,4},
                {1,9,4, 6,5,3, 2,7,8},
                {7,0,2, 9,4,8, 0,6,0},
                
                {3,1,7, 5,8,2, 9,4,6},
                {4,0,9, 7,1,6, 0,5,0},
                {5,0,0, 4,3,9, 7,0,0}
            };
            var wb = new Workbench(puzzle);
            
            Assert.Equal(Constraining.Status.Failure, Constraining.Constrain(wb));

            void Focus(int row, int col, int[] candidates) {
                var c = wb[row, col];
                var n = puzzle.GetLength(0);
                for(var i = 1; i<=n; i++)
                    if (!candidates.Contains(i))
                        c.RemoveCandidate(i);
            }
        }
    }
}