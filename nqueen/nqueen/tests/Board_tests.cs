using System.Linq;
using FluentAssertions;
using nqueen.data;
using Xunit;

namespace nqueen.tests
{
    public class Board_tests
    {
        [Fact]
        public void Vector_squares_of_3x3_board()
        {
            var n = 3;
            var sut = new Board(n);

            var col = 1;
            var row = 1;

            var result = sut.SquaresInVector(col, row, -1, -1);
            result.Should().BeEquivalentTo(new[] {new Square {column = col-1, row = row-1}});
            
            result = sut.SquaresInVector(col, row, 1, 1);
            result.Should().BeEquivalentTo(new[] {new Square {column = col+1, row = row+1}});
            
            result = sut.SquaresInVector(col, row, 0, 1);
            result.Should().BeEquivalentTo(new[] {new Square {column = col, row = row+1}});
        }
        
        [Fact]
        public void Vector_squares_of_nxn_board()
        {
            var n = 5;
            var sut = new Board(n);

            var col = 2;
            var row = 2;

            // north
            var result = sut.SquaresInVector(col, row, 0, -1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = col, row = 0},
                new Square {column = col, row = 1},
            });
            
            // south
            result = sut.SquaresInVector(col, row, 0, 1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = col, row = 3},
                new Square {column = col, row = 4},
            });
            
            // west
            result = sut.SquaresInVector(col, row, -1, 0);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 0, row = row},
                new Square {column = 1, row = row},
            });
            
            // east
            result = sut.SquaresInVector(col, row, 1, 0);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 3, row = row},
                new Square {column = 4, row = row},
            });
            
            // north-west
            result = sut.SquaresInVector(col, row, -1, -1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 0, row = 0},
                new Square {column = 1, row = 1},
            });
            
            // south-east
            result = sut.SquaresInVector(col, row, 1, 1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 3, row = 3},
                new Square {column = 4, row = 4},
            });
            
            // north-east
            result = sut.SquaresInVector(col, row, 1, -1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 4, row = 0},
                new Square {column = 3, row = 1},
            });
            
            // south-west
            result = sut.SquaresInVector(col, row, -1, 1);
            result.Should().BeEquivalentTo(new[] {
                new Square {column = 0, row = 4},
                new Square {column = 1, row = 3},
            });
        }
        
        
        [Fact]
        public void Col_squares_in_1x1_board()
        {
            var n = 1;
            var sut = new Board(n);
            var col = 0;

            var result = sut.SquaresInColumn(col).ToArray();

            result.Should().BeEquivalentTo(new[] {new Square {column = col, row = 0}});
        }
        
        [Fact]
        public void Col_squares_in_nxn_board()
        {
            var n = 3;
            var sut = new Board(n);
            var col = 1;
            
            var result = sut.SquaresInColumn(col).ToArray();

            result.Should().BeEquivalentTo(new[] {
                new Square {column = col, row = 0},
                new Square {column = col, row = 1},
                new Square {column = col, row = 2},
            });
        }
    }
}