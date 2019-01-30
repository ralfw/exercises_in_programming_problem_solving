using System.Linq;
using Xunit;

namespace sudoku
{
    public class Workbench_tests
    {
        [Fact]
        public void Initially_all_cells_unfixed() {
            var sut = new Workbench(new[,] {
                {4,1},
                {2,3}
            });
            Assert.Equal(4, sut.Unfixed.Length);
            Assert.Empty(sut.Fixed);
        }
        
        [Fact]
        public void Matrix_generation() {
            var sut = new Workbench(new[,] {
                {4,1},
                {2,3}
            });

            var cells = sut.Unfixed;
            cells[0].RemoveCandidate(4);
            cells[0].RemoveCandidate(1);
            cells[0].RemoveCandidate(3); // leave 2
            Assert.Single(sut.Fixed);
            
            cells[1].RemoveCandidate(2);
            cells[1].RemoveCandidate(3);
            cells[1].RemoveCandidate(1); // leave 4
            Assert.Equal(2, sut.Fixed.Length);
            
            cells[2].RemoveCandidate(2);
            cells[2].RemoveCandidate(4);
            cells[2].RemoveCandidate(1); // leave 3
            Assert.Equal(3, sut.Fixed.Length);
            
            cells[3].RemoveCandidate(4);
            cells[3].RemoveCandidate(2);
            cells[3].RemoveCandidate(3); // leave 1
            Assert.Equal(4, sut.Fixed.Length);
            Assert.Empty(sut.Unfixed);
            
            Assert.Equal(new[,] {
                {2,4},
                {3,1}
            }, sut.Matrix);
        }


        [Fact]
        public void Workbench_initialization()
        {
            var puzzle = new[,] {
                {0,0, 2,0},
                {1,0, 0,3},
                
                {0,1, 3,4},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);
            
            Assert.Equal(7, sut.Fixed.Length);
            Assert.Equal(9, sut.Unfixed.Length);

            var fixedNumbers = sut.Fixed.Select(f => f.SolutionNumber);
            Assert.Equal(new[]{2,1,3,1,3,4,2}, fixedNumbers);
        }
        
        
        [Fact]
        public void Horizon_calculation()
        {
            var puzzle = new[,] {
                {0,0, 2,0},
                {1,0, 0,4},
                
                {0,1, 3,2},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var horizon = sut.Horizon(sut.Fixed[0]); // 2 in upper right box
            Assert.Equal(9, horizon.Length);
            
            Assert.Contains(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 4);
            Assert.Contains(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 3);
            Assert.DoesNotContain(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 2);
        }

        [Fact]
        public void Determine_coords()
        {
            var puzzle = new[,] {
                {0,0, 2,0},
                {1,0, 0,4},
                
                {0,1, 3,2},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var (row, col) = sut.DetermineCoordinates(sut.Fixed[0]);
            Assert.Equal(0, row);
            Assert.Equal(2, col);

            (row, col) = sut.DetermineCoordinates(sut.Fixed[1]);
            Assert.Equal(1, row);
            Assert.Equal(0, col);
            
            (row, col) = sut.DetermineCoordinates(sut.Fixed[5]);
            Assert.Equal(2, row);
            Assert.Equal(3, col);
        }
        
        
        [Fact]
        public void Cell_tests() {
            var sut = new Workbench.Cell(3);
            
            Assert.False(sut.IsFixed);
            
            sut.RemoveCandidate(3);
            Assert.False(sut.IsFixed);
            
            sut.RemoveCandidate(2);
            Assert.True(sut.IsFixed);
            Assert.Equal(1, sut.SolutionNumber);
            
            sut.RemoveCandidate(3);
            Assert.True(sut.IsFixed);
            
            sut.RemoveCandidate(1);
            Assert.False(sut.IsFixed);
        }
    }
}