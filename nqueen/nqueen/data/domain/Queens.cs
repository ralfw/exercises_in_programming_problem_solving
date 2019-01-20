using System.Collections.Generic;
using System.Linq;

namespace nqueen.data.domain
{
    class Queens
    {
        private readonly List<Square> _squaresWithQueens;

        private Queens(List<Square> squaresWithQueens) => _squaresWithQueens = squaresWithQueens;
        public Queens() : this(new List<Square>()) {}
        
        public IEnumerable<Square> Squares => _squaresWithQueens;

        public Queens Add(Square newSquareWithQueen) {
            var squaresWithQueens = new List<Square>(_squaresWithQueens);
            
            if (!squaresWithQueens.Exists(sq => sq.column == newSquareWithQueen.column && 
                                                sq.row == newSquareWithQueen.row))
                squaresWithQueens.Add(newSquareWithQueen);
            
            return new Queens(squaresWithQueens);
        }

        public IEnumerable<Square> StillUnoccupied(params Square[] candidateSquares)
            => candidateSquares.Except(_squaresWithQueens, new CompareSquares());

        private class CompareSquares : IEqualityComparer<Square> {
            public bool Equals(Square x, Square y) => x.column == y.column && x.row == y.row;
            public int GetHashCode(Square obj) => 0; // 0 forces Equals() to be called
        }
    }
}