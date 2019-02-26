using System;

namespace sudoku_the_impossible
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                Source: http://norvig.com/sudoku.html (the "impossible puzzle that took 1439 seconds")
                This is taking veeeery long. The solution so far clearly would benefit from
                some optimization. See TODOs for suggestions.
            */
            var puzzle = new[,] {
                {0,0,0, 0,0,5, 0,8,0},
                {0,0,0, 6,0,1, 0,4,3},
                {0,0,0, 0,0,0, 0,0,0},
                
                {0,1,0, 5,0,0, 0,0,9},
                {0,0,0, 1,0,6, 0,0,0},
                {3,0,0, 0,0,0, 0,0,5},
                
                {5,3,0, 0,0,0, 0,6,1},
                {0,0,0, 0,0,0, 0,0,4},
                {0,0,0, 0,0,0, 0,0,0}
            };

            var start = DateTime.Now;
            Console.WriteLine($"Started: {start}");
            
            var result = sudoku.SudokuSolver.Solve(puzzle);
            
            Console.WriteLine($"Solution found? {sudoku.tests.SolutionChecker.Check(result)} / {DateTime.Now}, duration: {DateTime.Now.Subtract(start)}");
            DumpMatrix(result);
        }
        
        static void DumpMatrix(int[,] matrix) {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                Console.Write($"{row}: ");
                for (var col = 0; col < matrix.GetLength(1); col++) {
                    Console.Write($"{matrix[row,col]} ");
                }
                Console.WriteLine("");
            }
        }
    }
}