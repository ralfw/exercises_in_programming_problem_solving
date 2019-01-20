using System.Linq;
using FluentAssertions;
using Xunit;
using nqueen.data;

namespace nqueen.tests
{
    public class Queens_tests
    {
        [Fact]
        public void Check_for_queens_on_squares()
        {
            var sut = new Queens()
                        .Add(new Square {column = 0, row = 0})
                        .Add(new Square {column = 0, row = 1})
                        .Add(new Square {column = 1, row = 0});

            sut.StillUnoccupied(new Square[0]).Should().BeEmpty();
            sut.StillUnoccupied(new Square{column = 0, row = 1}).Should().BeEmpty();
            sut.StillUnoccupied(new Square {column = 1, row = 1})
                .Should().BeEquivalentTo(new Square {column = 1, row = 1});
            sut.StillUnoccupied(
                    new Square {column = 1, row = 1}, 
                    new Square {column = 1, row = 0},
                    new Square {column = 2, row = 3})
                .Should().BeEquivalentTo(new Square {column = 1, row = 1}, new Square {column = 2, row = 3});
        }
        
        [Fact]
        public void All_queens_are_unique()
        {
            var sut = new Queens();
            sut = sut.Add(new Square {column = 0, row = 1});
            sut = sut.Add(new Square {column = 0, row = 1});
            Assert.Single(sut.Squares);
        }
        
        [Fact]
        public void Add_queens()
        {
            var sut = new Queens();
            
            Assert.Empty(sut.Squares);

            var sut1 = sut.Add(new Square {column = 0, row = 0});
            Assert.Empty(sut.Squares);
            sut1.Squares.Should().BeEquivalentTo(new Square {column = 0, row = 0});
            
            var sut2 = sut1.Add(new Square {column = 1, row = 1});
            sut2.Squares.Should().BeEquivalentTo(
                new Square {column = 0, row = 0},
                new Square {column = 1, row = 1}
            );
        }

        [Fact]
        public void Immutability()
        {
            var sut = new Queens();
            var result = sut.Add(new Square {column = 0, row = 0});
            Assert.NotSame(sut, result);
        }
    }
}