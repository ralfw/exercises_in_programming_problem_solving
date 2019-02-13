using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xunit;

namespace sudoku.tests
{
    public class Incremental_tests
    {
        [Fact]
        public void Level1()
        {
            var puzzle = new[,] {
                {0,0, 3,0},
                {3,0, 0,2},
                
                {0,1, 0,0},
                {4,0, 0,1}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
        
        
        [Fact()]
        public void Level2()
        {
            var puzzle = new[,] {
                {0,2, 0,0},
                {0,0, 0,0},
                
                {0,0, 0,3},
                {4,0, 0,0}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            Assert.True(SolutionChecker.Check(result));
        }
        
        
        [Fact()]
        public void Level3()
        {
            var puzzle = new[,] {
                {6,0,0, 0,0,1, 4,8,0},
                {0,0,0, 0,0,0, 0,0,0},
                {2,0,0, 0,0,0, 6,0,5},
                
                {0,0,3, 0,0,7, 0,0,0},
                {0,9,0, 6,5,0, 2,7,8},
                {0,0,0, 9,0,0, 0,6,0},
                
                {3,0,7, 0,8,2, 9,4,0},
                {4,0,9, 0,1,0, 0,5,0},
                {5,0,0, 0,0,9, 7,0,0}
            };

            var result = SudokuSolver.Solve(puzzle);
            
            DumpMatrix(result);
            
            Assert.True(SolutionChecker.Check(result));
        }


        void DumpMatrix(int[,] matrix) {
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                Debug.Write($"{row}: ");
                for (var col = 0; col < matrix.GetLength(1); col++) {
                    Debug.Write($"{matrix[row,col]} ");
                }
                Debug.WriteLine("");
            }
        }
    }
}