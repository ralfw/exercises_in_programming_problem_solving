using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using nqueen.data;

namespace nqueen.tests
{
    public class NQueenProblem_tests
    {
        [Fact]
        public void Placing_the_only_queen() {
            var sut = new NQueenProblem(1);
            
            Queens result = null;
            sut.PlaceQueens(0, new Queens(), 
                            queens => result=queens);
            
            result.Squares.Should().BeEquivalentTo(new Square{column=0, row=0});
        }
        
        [Fact]
        public void Placing_the_final_queen() {
            var sut = new NQueenProblem(3);
            
            Queens result = null;
            var initialQueens = new Queens().Add(new Square {column = 1, row = 0});
            sut.PlaceQueens(2, initialQueens, 
                            queens => result=queens);
            
            result.Squares.Should().BeEquivalentTo(
                new Square{column=1, row=0}, 
                new Square{column = 2, row = 2});
        }
        
        [Fact]
        public void No_solution_with_backtracking() {
            var sut = new NQueenProblem(3);
            
            Queens result = null;
            sut.PlaceQueens(0, new Queens(), 
                            queens => result=queens);
            
            Assert.Null(result);
        }
        
        
        [Fact]
        public void Solve_4x4() {
            var sut = new NQueenProblem(4);
            
            var result = new List<Queens>();
            sut.PlaceQueens(0, new Queens(), 
                            queens => result.Add(queens));

            result.Count.Should().Be(2);
        }
        
        
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