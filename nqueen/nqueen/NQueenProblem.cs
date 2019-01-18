using System;
using System.Linq;

namespace nqueen {
    class NQueenProblem {
        public class Solution {
            public Position[] Queens;
            
            public class Position {
                public char Col; // 'a'..char(n) (e.g. 'd' for n=4)
                public int Row; // 1..n
            }
        }

        public static Solution[] Solve(int n) {
            throw new NotImplementedException();
        }
    }
}