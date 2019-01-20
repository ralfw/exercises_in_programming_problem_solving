using Xunit;
using FluentAssertions;
using nqueen.data;

namespace nqueen.tests
{
    public class NQueenProblem_tests
    {
        [Fact]
        public void IsSafe_with_no_queens_yet() {
            var sut = new NQueenProblem(3);
            var queens = new Queens();

            sut.IsSafe(1, 1, queens).Should().BeTrue();
        }
        
        [Theory]
        [InlineData(0,0, 2,2, "nw")]
        [InlineData(1,0, 1,2, "n")]
        [InlineData(2,0, 0,2, "ne")]
        [InlineData(2,1, 0,1, "e")]
        [InlineData(2,2, 0,0, "se")]
        [InlineData(1,2, 1,0, "s")]
        [InlineData(0,2, 2,0, "sw")]
        [InlineData(0,1, 2,1, "w")]
        public void Not_safe_in_different_positions(int queenColumn, int queenRow, int candidateColumn, int candidateRow, string direction) {
            var sut = new NQueenProblem(3);
            var queens = new Queens().Add(new Square {column = queenColumn, row = queenRow});

            sut.IsSafe(candidateColumn, candidateRow, queens).Should().BeFalse();
        }
    }
}