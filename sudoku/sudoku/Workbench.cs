using System;
using System.Collections.Generic;

namespace sudoku
{
    class Workbench
    {
        public class Cell {
            private List<int> _candidateNumbers;
            
            public bool IsFixed { get; }
            public int SolutionNumber { get; }
            
            public void RemoveCandidate(int number) {}
        }
    
        
        private Cell[,] _cells;
        
        
        public Workbench(int[,] matrix) {}
        
        public Cell[] Fixed { get; }
        public Cell[] Unfixed { get; }
        
        public Cell[] Horizon(Cell center) { throw new NotImplementedException(); }
        
        public int[,] Matrix { get; }
    }
}