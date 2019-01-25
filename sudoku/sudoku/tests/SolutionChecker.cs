using System;
using System.Collections.Generic;
using System.Linq;

namespace sudoku
{
    class SolutionChecker
    {
        public static bool Check(int[,] sln)
            => CheckRows(sln) & CheckCols(sln) & CheckBoxes(sln);

        private static bool CheckRows(int[,] sln) {
            var size = sln.GetLength(1);
            for(var row=0; row<size; row++)
                if (CheckRow(sln, size, row) is false)
                    return false;
            return true;
        }

        internal static bool CheckRow(int[,] sln, int size, int row) {
            var numbers = Enumerable.Range(0, size).Select(col => sln[row, col]);
            var expected = Enumerable.Range(1, size).ToArray();
            return expected.Intersect(numbers).Count() == expected.Length;
        }

        static bool CheckCols(int[,] sln) {
            var size = sln.GetLength(0);
            for(var col=0; col<size; col++)
                if (CheckCol(sln, size, col) is false)
                    return false;
            return true;
        }
        
        internal static bool CheckCol(int[,] sln, int size, int col) {
            var numbers = Enumerable.Range(0, size).Select(row => sln[row, col]);
            var expected = Enumerable.Range(1, size).ToArray();
            return expected.Intersect(numbers).Count() == expected.Length;
        }

        static bool CheckBoxes(int[,] sln) {
            var n = (int)Math.Sqrt(sln.GetLength(0));
            for(var brow=0; brow<n; brow++)
            for(var bcol=0; bcol<n; bcol++)
                if (CheckBox(sln, n, brow * n, brow * n) is false)
                    return false;
            return true;
        }

        internal static bool CheckBox(int[,] sln, int n, int toprow, int leftcol) {
            var numbers = new List<int>();
            for(var row=toprow; row < toprow+n; row++)
            for (var col = leftcol; col < leftcol+n; col++)
                numbers.Add(sln[row, col]);

            var expected = Enumerable.Range(1, n*n).ToArray();
            return expected.Intersect(numbers).Count() == expected.Length;
        }
    }
}