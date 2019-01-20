using System;
using System.Collections.Generic;
using System.Linq;
using nqueen.data;
using nqueen.data.domain;


namespace nqueen {
    public static class NQueenProblem {
        private const int MINIMAL_N = 4;

        public static Solution[] Solve(int n) {
            if (n < MINIMAL_N) return new Solution[0];
            
            var partialSolutions = new List<Queens>();
            PlaceQueens(0, new ChessBoard(n),
                        partialSolutions.Add);
            return Map(partialSolutions).ToArray();
        }

        private static IEnumerable<Solution> Map(IEnumerable<Queens> partialSolutions) {
            return partialSolutions.Select(qs => new Solution(qs.Squares.Select(Map).ToArray()));

            Solution.Position Map(Square square)
                => new Solution.Position( 
                    col:(char)((byte)'a' + square.column), 
                    row:square.row+1
                );
        }
        
        
        internal static void PlaceQueens(int column, ChessBoard board, Action<Queens> onSolutionFound) {
            if (column >= board.Size) { onSolutionFound(board.Queens); return; }

            foreach (var candidateSquare in board.SquaresInColumn(column)) {
                if (board.IsSafe(candidateSquare.column, candidateSquare.row)) {
                    PlaceQueens(column + 1, board.PlaceQueen(candidateSquare),
                                onSolutionFound);
                }
            }
        }
    }
}