using System.Collections.Generic;
using System.Linq;

namespace nqueen.data.domain
{
    class ChessBoard
    {
        private readonly Board _board;

        
        public ChessBoard(int n) {
            _board = new Board(n);
            Queens = new Queens();
        }

        internal ChessBoard(Board board, Queens queens) {
            _board = board;
            Queens = queens;
        }

        
        public readonly Queens Queens;
        public int Size => _board.Size;
        
        public IEnumerable<Square> SquaresInColumn(int column) => _board.SquaresInColumn(column);

        public ChessBoard PlaceQueen(Square newQueen)
            => new ChessBoard(_board, Queens.Add(newQueen));
        
        
        public bool IsSafe(int column, int row) {
            if (Queens.Squares.Any() is false) return true;

            var threatVectors = BuildThreatVectors(column, row);
            return NoOtherQueenIsThreatened(threatVectors);
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
        
        private bool NoOtherQueenIsThreatened(Square[] threatVectors)
            // no other queen is threatened if all threat vectors' squares are unoccupied
            => Queens.StillUnoccupied(threatVectors).Count() == threatVectors.Length;
    }
}