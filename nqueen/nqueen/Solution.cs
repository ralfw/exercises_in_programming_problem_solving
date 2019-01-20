using System.Collections.Generic;

namespace nqueen
{
    public class Solution {
        public IEnumerable<Position> Queens { get; }

        public Solution(IEnumerable<Position> queens) => Queens = queens;
        
        public class Position {
            public char Col { get; } // 'a'..char(n) (e.g. 'd' for n=4)
            public int Row { get; } // 1..n

            public Position(char col, int row) {
                Col = col;
                Row = row;
            }
        }
    }
}