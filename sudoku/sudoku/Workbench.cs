using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace sudoku
{
    public class Workbench_test
    {
        [Fact]
        public void Cell() {
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
    
    
    class Workbench
    {
        public class Cell {
            private readonly List<int> _candidateNumbers;

            public Cell(int n) {
                _candidateNumbers = new List<int>(Enumerable.Range(1,n));
            }

            public bool IsFixed => _candidateNumbers.Count == 1;
            public int SolutionNumber => _candidateNumbers.First();

            public void RemoveCandidate(int number) => _candidateNumbers.Remove(number);
        }
    
        
        private Cell[,] _cells;
        
        
        public Workbench(int[,] matrix) {}
        
        public Cell[] Fixed { get; }
        public Cell[] Unfixed { get; }
        
        public Cell[] Horizon(Cell center) { throw new NotImplementedException(); }
        
        public int[,] Matrix { get; }
    }
}