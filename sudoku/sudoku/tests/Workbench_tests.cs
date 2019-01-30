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