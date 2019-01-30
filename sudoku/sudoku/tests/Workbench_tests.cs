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