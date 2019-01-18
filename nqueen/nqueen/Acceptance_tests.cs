using System;
using System.Linq;
using Xunit;

namespace nqueen
{
    public class Acceptance_tests
    {
        [Theory]
        [InlineData(4, new[]{"c1a2d3b4", "b1d2a3c4"})]
        [InlineData(6, new[]{"e1c2a3f4d5b6", "d1a2e3b4f5c6", "c1f2b3e4a5d6", "b1d2f3a4c5e6"})]
        public void RelevantN(int n, string[] expected)
        {
            var result = NQueenProblem.Solve(n);

            Assert.Equal(expected.Length, result.Length);

            var normalizedResult = result.Select(Normalize);
            Assert.Equal(expected, normalizedResult);
        }


        [Fact]
        public void IrrelevantN(){
            Assert.Equal(0, NQueenProblem.Solve(1).Length);
            Assert.Equal(0, NQueenProblem.Solve(2).Length);
            Assert.Equal(0, NQueenProblem.Solve(3).Length);
        }



        [Fact]
        public void Normalize_test() {
            var sol = new NQueenProblem.Solution();
            sol.Queens = new[]{
                new NQueenProblem.Solution.Position(){Col='a', Row=3},
                new NQueenProblem.Solution.Position(){Col='b', Row=2},
                new NQueenProblem.Solution.Position(){Col='c', Row=1},
            };

            var result = Normalize(sol);

            Assert.Equal("c1b2a3", result);
        }

        string Normalize(NQueenProblem.Solution solution)
            => string.Join("", solution.Queens.OrderBy(p => p.Row).Select(p => $"{p.Col}{p.Row}").ToArray());
    }
}