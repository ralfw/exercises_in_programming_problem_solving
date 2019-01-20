using System.Linq;
using Xunit;
using FluentAssertions;

namespace nqueen
{
    public class Board_tests
    {
        [Fact]
        public void Rows_in_1x1_board()
        {
            var n = 1;
            var sut = new Board(n);
            var col = 0;

            var result = sut.SquaresInColumn(col).ToArray();

            result.Should().BeEquivalentTo(new[] {new Board.Square {column = col, row = 0}});
        }
        
        [Fact]
        public void Rows_in_nxn_board()
        {
            var n = 3;
            var sut = new Board(n);
            var col = 1;
            
            var result = sut.SquaresInColumn(col).ToArray();

            result.Should().BeEquivalentTo(new[] {
                new Board.Square {column = col, row = 0},
                new Board.Square {column = col, row = 1},
                new Board.Square {column = col, row = 2},
            });
        }
    }
}