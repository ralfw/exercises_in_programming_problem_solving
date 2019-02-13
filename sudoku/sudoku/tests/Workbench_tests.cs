using System.Linq;
using Xunit;

namespace sudoku.tests
{
    public class Workbench_tests
    {
        [Fact]
        public void Initially_all_cells_unfixed() {
            var puzzle = new[,] {
                {0,0, 0,0},
                {0,0, 0,0},
                
                {0,0, 0,0},
                {0,0, 0,0}
            };
            var sut = new Workbench(puzzle);
            Assert.Equal(16, sut.Unfixed.Length);
            Assert.Empty(sut.Fixed);
        }
        
        [Fact]
        public void Matrix_generation() {
            var puzzle = new[,] {
                {0,0, 0,0},
                {0,0, 0,0},
                
                {0,0, 0,0},
                {0,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var cells = sut.Unfixed;
            cells[0].RemoveCandidate(2);
            cells[0].RemoveCandidate(3);
            cells[0].RemoveCandidate(4);
            Assert.Single(sut.Fixed); // 1 left
            
            cells[1].RemoveCandidate(1);
            cells[1].RemoveCandidate(3);
            cells[1].RemoveCandidate(4);
            Assert.Equal(2, sut.Fixed.Length); // 2 left

            foreach (var cell in cells.Skip(2)) {
                cell.RemoveCandidate(1);
                cell.RemoveCandidate(2);
                cell.RemoveCandidate(3);
            } // 4 left

            Assert.Equal(16, sut.Fixed.Length);
            Assert.Empty(sut.Unfixed);
            
            Assert.Equal(new[,] {
                {1,2, 4,4},
                {4,4, 4,4},
                
                {4,4, 4,4},
                {4,4, 4,4}
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
        public void Cells_initialized_with_correct_number_range()
        {
            var puzzle = new[,] {
                {0,0, 2,0},
                {1,0, 0,3},
                
                {0,1, 3,4},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var cell = sut.Unfixed.First();
            cell.RemoveCandidate(2);
            cell.RemoveCandidate(1);
            cell.RemoveCandidate(4);
            Assert.True(cell.IsFixed);
            Assert.Equal(3, cell.SolutionNumber);
        }
        
        
        [Fact]
        public void Horizon_calculation()
        {
            var puzzle = new[,] {
                {0,0, 0,3},
                {1,0, 2,4},
                
                {0,1, 3,2},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var horizon = sut.Horizon(sut.Fixed[2]); // 2 in upper right box
            Assert.Equal(9, horizon.Length);
            
            Assert.Contains(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 4);
            Assert.Contains(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 3);
            Assert.Contains(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 1);
            Assert.DoesNotContain(horizon.Where(c => c.IsFixed), c => c.SolutionNumber == 2);
        }

        [Fact]
        public void __Determine_coords()
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
        public void __Horizon_coords_calculation()
        {
            var puzzle = new[,] {
                {0,0, 0,3},
                {1,0, 2,4},
                
                {0,1, 3,2},
                {2,0, 0,0}
            };
            var sut = new Workbench(puzzle);

            var result = sut.HorizonCoordinates(2, 3).ToArray();
            Assert.Equal(9, result.Length);
            
            Assert.Equal(new[] {
                (2,2),(3,2),(3,3), // box
                (2,0),(2,1),(2,2), // row
                (0,3),(1,3),(3,3)  // col
            }, result);
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

        [Fact]
        public void Cell_removeCandidatesExcept() {
            var sut = new Workbench.Cell(3);
            sut.SolutionNumber = 2;
            Assert.True(sut.IsFixed);
            Assert.Equal(2, sut.SolutionNumber);
        }


        [Fact]
        public void Cell_clone() {
            var sut = new Workbench.Cell(4);
            sut.RemoveCandidate(1);
            sut.RemoveCandidate(4);

            var result = sut.Clone();
            
            Assert.NotSame(sut, result);
            Assert.Equal(new[]{2,3}, result.CandidateNumbers);

            result.RemoveCandidate(2);
            Assert.True(result.IsFixed);
            Assert.False(sut.IsFixed);
        }

        [Fact]
        public void Workbench_clone()
        {
            var puzzle = new[,] {
                {0,2, 3,4},
                {3,4, 1,2},
                
                {2,1, 4,3},
                {4,3, 2,1}
            };
            var sut = new Workbench(puzzle);

            var result = sut.Clone();
            Assert.NotSame(sut, result);
            Assert.Equal(sut.Fixed.Length, result.Fixed.Length);
            
            result.Unfixed.First().RemoveCandidate(2);
            result.Unfixed.First().RemoveCandidate(3);
            result.Unfixed.First().RemoveCandidate(4);
            
            Assert.Single(sut.Unfixed);
            Assert.Empty(result.Unfixed);
        }
    }
}