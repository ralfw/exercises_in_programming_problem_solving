using System;
using System.Collections.Generic;
using System.Linq;
using nqueen.data;


namespace nqueen {
    public class NQueenProblem {
        private const int MINIMAL_N = 4;

        public static Solution[] Solve(int n) {
            if (n < MINIMAL_N) return new Solution[0];
            return new NQueenProblem(n).Solve();
        }

        
        private readonly Board _board;
        internal NQueenProblem(int n) => _board = new Board(n);
        
        private Solution[] Solve() {
            var partialSolutions = new List<Queens>();
            PlaceQueens(0, new Queens(),
                        partialSolutions.Add);
            return Map(partialSolutions).ToArray();
        }

        private IEnumerable<Solution> Map(IEnumerable<Queens> partialSolutions) {
            return partialSolutions.Select(qs => new Solution(qs.Squares.Select(Map).ToArray()));

            Solution.Position Map(Square square)
                => new Solution.Position( 
                    col:(char)((byte)'a' + square.column), 
                    row:square.row+1
                );
        }
        
        internal void PlaceQueens(int column, Queens queens, Action<Queens> onSolutionFound) {
            if (column >= _board.Size) { onSolutionFound(queens); return; }
            
            GenerateRowSquares(candidateSquare => {
                if (IsSafe(candidateSquare.column, candidateSquare.row, queens)) {
                    PlaceQueens(column+1, queens.Add(candidateSquare), 
                                onSolutionFound);
                }
            });

            
            void GenerateRowSquares(Action<Square> onRowSquare)
                => Enumerable.Range(0, _board.Size)
                             .Select(row => new Square {column = column, row = row})
                             .ToList()
                             .ForEach(onRowSquare);
        }
        
        internal bool IsSafe(int column, int row, Queens queens) {
            if (queens.Squares.Any() is false) return true;

            var threatVectors = BuildThreatVectors(column, row);
            return NoOtherQueenIsThreatened(threatVectors, queens);
        }

        private Square[] BuildThreatVectors(int column, int row) 
            =>          _board.SquaresInVector(column, row, -1, -1) // nw
                .Concat(_board.SquaresInVector(column, row, 0, -1)) // n
                .Concat(_board.SquaresInVector(column, row, 1, -1)) // ne
                .Concat(_board.SquaresInVector(column, row, 1, 0)) // e
                .Concat(_board.SquaresInVector(column, row, 1, 1)) // se
                .Concat(_board.SquaresInVector(column, row, 0, 1)) // s
                .Concat(_board.SquaresInVector(column, row, -1, 1)) // sw
                .Concat(_board.SquaresInVector(column, row, -1, 0)) // w
                .ToArray();
        
        private bool NoOtherQueenIsThreatened(Square[] threatVectors, Queens queens)
            // no other queen is threatened if all threat vectors' squares are unoccupied
            => queens.StillUnoccupied(threatVectors).Count() == threatVectors.Length;
    }
}