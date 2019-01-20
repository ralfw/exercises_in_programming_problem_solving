using System;
using System.Collections.Generic;
using System.Linq;

namespace nqueen
{
    class Board {
        public class Square{
            public int column;
            public int row;
        }

        private int _n;

        public Board(int n) => _n = n;

        public IEnumerable<Square> SquaresInColumn(int column)
            => Enumerable.Range(0, _n).Select(row => new Square {column = column, row = row});

        public IEnumerable<Square> SquaresInVector(int column, int row, int deltaColumn, int deltaRow) {
            while (true) {
                column += deltaColumn;
                row += deltaRow;
                if (column < 0 || column >= _n || row < 0 || row >= _n) break;
                
                yield return new Square {column = column, row = row};
            }
        }
    }
}