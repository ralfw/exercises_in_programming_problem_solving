using System;
using System.Linq;

namespace nqueen {
    public class Solution {
        public Position[] Queens;
        
        public class Position {
            public char Col; // 'a'..char(n) (e.g. 'd' for n=4)
            public int Row; // 1..n
        }
    }

    public class NQueenProblem {
        private const int MINIMAL_N = 4;

        public static Solution[] Solve(int n) {
            if (n < MINIMAL_N) return new Solution[0];

            throw new NotImplementedException();
        }
    }
}