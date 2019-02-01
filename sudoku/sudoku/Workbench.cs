using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace sudoku
{
    class Workbench
    {
        public class Cell {
            private readonly List<int> _candidateNumbers;

            public Cell(int n)
            {
                _candidateNumbers = new List<int>(Enumerable.Range(1, n));
            }

            public bool IsFixed => _candidateNumbers.Count == 1;
            public int SolutionNumber => _candidateNumbers.First();
            public int[] CandidateNumbers => _candidateNumbers.ToArray();

            public void RemoveCandidate(int number) {
                _candidateNumbers.Remove(number);
            }
        }


        private Cell[,] _cells;


        public Workbench(int[,] matrix)
        {
            var size = Calc_size_for_any_matrix();
            
            Initialize();
            PlaceGivenNumbers();


            int Calc_size_for_any_matrix() // this also works for non-square matrices
                => (int) Math.Sqrt(matrix.GetLength(0)) * (int) Math.Sqrt(matrix.GetLength(1));

            
            void Initialize() {
                _cells = new Cell[matrix.GetLength(0), matrix.GetLength(1)];
                foreach (var coord in AllCoordinates())
                    _cells[coord.row, coord.col] = new Cell(size);   
            }

            
            void PlaceGivenNumbers() {
                foreach (var coord in AllCoordinates()) {
                    var givenNumber = matrix[coord.row, coord.col];
                    if (givenNumber == 0) continue;
                    
                    foreach (var i in Enumerable.Range(1,size))
                        if (i != givenNumber) _cells[coord.row,coord.col].RemoveCandidate(i);
                }
            }
        }

        
        private IEnumerable<(int row, int col)> AllCoordinates() { 
            for(var row= 0; row<_cells.GetLength(0); row++)
            for(var col= 0; col<_cells.GetLength(1); col++)
                yield return (row, col);
        }

        private IEnumerable<Cell> AllCells() 
            => AllCoordinates().Select(coord => _cells[coord.row, coord.col]);

        public Cell[] Fixed => AllCells().Where(cell => cell.IsFixed).ToArray();
        public Cell[] Unfixed => AllCells().Where(cell => cell.IsFixed is false).ToArray();

        public Cell[] Horizon(Cell center) {
            var centerCoords = DetermineCoordinates(center);
            return HorizonCoordinates(centerCoords.row, centerCoords.col)
                    .Select(coord => _cells[coord.row, coord.col])
                    .ToArray();
        }

        internal (int row, int col) DetermineCoordinates(Cell cell) {
            foreach(var coord in AllCoordinates())
                if (_cells[coord.row, coord.col] == cell)
                    return (coord.row, coord.col);
            throw new InvalidOperationException("Cell not present in workbench!");
        }
        
        internal IEnumerable<(int row, int col)> HorizonCoordinates(int row, int col)
        {
            // box coordinates
            var boxHeight = (int)Math.Sqrt(_cells.GetLength(0));
            var boxWidth = (int) Math.Sqrt(_cells.GetLength(1));
            var boxTop = row / boxHeight * boxHeight;
            var boxLeft = col / boxWidth * boxWidth;
            for(var dr = 0; dr < boxHeight; dr++)
            for (var dc = 0; dc < boxWidth; dc++) {
                var r = boxTop + dr;
                var c = boxLeft + dc;
                if ((r == row && c == col) is false)
                    yield return (r, c);
            }

            // row coordinates
            for (var c = 0; c < _cells.GetLength(1); c++)
                if (c != col)
                    yield return (row, c);
            
            // column coordinates
            for (var r = 0; r < _cells.GetLength(0); r++)
                if (r != row)
                    yield return (r, col);
        }
        
        public int[,] Matrix { 
            get {
                var matrix = new int[_cells.GetLength(0), _cells.GetLength(1)];
                foreach (var coord in AllCoordinates())
                    matrix[coord.row, coord.col] = _cells[coord.row, coord.col].SolutionNumber;
                return matrix;
            }
        }
    }
}