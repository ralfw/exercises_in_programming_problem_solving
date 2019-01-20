using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using nqueen.data;
using nqueen.data.domain;

namespace nqueen.tests
{
    public class NQueenProblem_tests
    {
        [Fact]
        public void Placing_the_only_queen() {
            var board = new ChessBoard(new Board(1), new Queens());   
            
            Queens result = null;
            NQueenProblem.PlaceQueens(0, board, 
                                      queens => result=queens);
            
            result.Squares.Should().BeEquivalentTo(new Square{column=0, row=0});
        }
        
        [Fact]
        public void Placing_the_final_queen() {
            var board = new ChessBoard(new Board(3), new Queens().Add(new Square {column = 1, row = 0}));
            
            Queens result = null;
            NQueenProblem.PlaceQueens(2, board, 
                                      queens => result=queens);
            
            result.Squares.Should().BeEquivalentTo(
                new Square{column=1, row=0}, 
                new Square{column = 2, row = 2});
        }
        
        [Fact]
        public void No_solution_with_backtracking() {
            var board = new ChessBoard(new Board(3), new Queens());
            
            Queens result = null;
            NQueenProblem.PlaceQueens(0, board, 
                                      queens => result=queens);
            
            Assert.Null(result);
        }
        
        
        [Fact]
        public void Solve_4x4() {
            var board = new ChessBoard(new Board(4), new Queens());
            
            var result = new List<Queens>();
            NQueenProblem.PlaceQueens(0, board, 
                                      queens => result.Add(queens));

            result.Count.Should().Be(2);
        }
        
        
        [Fact]
        public void IsSafe_with_no_queens_yet() {
            var sut = new ChessBoard(3);

            sut.IsSafe(1, 1).Should().BeTrue();
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
            var queens = new Queens().Add(new Square {column = queenColumn, row = queenRow});
            var sut = new ChessBoard(new Board(3), queens);

            sut.IsSafe(candidateColumn, candidateRow).Should().BeFalse();
        }
    }
}