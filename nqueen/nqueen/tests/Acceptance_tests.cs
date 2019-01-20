using System.Diagnostics;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace nqueen.tests
{
    public class Acceptance_tests
    {
        [Theory]
        [InlineData(4, new[]{"c1a2d3b4", "b1d2a3c4"})]
        [InlineData(6, new[]{"e1c2a3f4d5b6", "d1a2e3b4f5c6", "c1f2b3e4a5d6", "b1d2f3a4c5e6"})]
        // See here (https://stamm-wilbrandt.de/en/xsl-list/n-queens/n-queens.xsl.xml) for these
        // and more solutions.
        public void RelevantN(int n, string[] expected) {
            var result = NQueenProblem.Solve(n);

            Assert.Equal(expected.Length, result.Length);

            var serializedResult = result.Select(Serialize);
            serializedResult.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void IrrelevantN(){
            Assert.Empty(NQueenProblem.Solve(1));
            Assert.Empty(NQueenProblem.Solve(2));
            Assert.Empty(NQueenProblem.Solve(3));
        }


        string Serialize(Solution solution)
            => string.Join("", solution.Queens.OrderBy(p => p.Row).Select(p => $"{p.Col}{p.Row}").ToArray());


        [Fact]
        public void Serialize_test() {
            var sol = new Solution(new[]{
                new Solution.Position('a',3),
                new Solution.Position('b', 2),
                new Solution.Position('c', 1),
            });

            var result = Serialize(sol);

            Assert.Equal("c1b2a3", result);
        }
    }
}