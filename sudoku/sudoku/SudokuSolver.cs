using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace sudoku
{
    public static class SudokuSolver
    {
        public static int[,] Solve(int[,] puzzle) {
            var wb = new Workbench(puzzle);
            
            var (success, solution) = Solve(wb);
            
            if (success) return solution.Matrix;
            throw new InvalidOperationException("No solution found for puzzle!");
        }
    
        static (bool success, Workbench workbench) Solve(Workbench workbench)
        {
            // solution numbers the same in row/col/box
            int numberOfCellsFixed;
            while(true) {
                if (TryConstrain(workbench, out numberOfCellsFixed) is false)
                    return (false, null);
                if (numberOfCellsFixed == 0)
                    break;
            }

            if (SolutionFound()) return (true, workbench);

            foreach (var unfixed in workbench.Unfixed) {
                var (unfixedRow, unfixedCol) = workbench.DetermineCoordinates(unfixed);
                foreach (var tentativeSolutionNumber in unfixed.CandidateNumbers) {
                    var tentativeWorkbench = workbench.Clone();
                    tentativeWorkbench[unfixedRow, unfixedCol].SolutionNumber = tentativeSolutionNumber;
                    
                    var (success, solutionWorkbenche) = Solve(tentativeWorkbench);
                    
                    if (success) return (true, solutionWorkbenche);
                }
            }

            return (false, null);

            
            bool SolutionFound() => workbench.Unfixed.Length == 0;
        }
    
        
        internal static bool TryConstrain(Workbench workbench, out int numberOfCellsFixed) {
            numberOfCellsFixed = 0;

            foreach (var fixedCell in workbench.Fixed) {
                var horizon = workbench.Horizon(fixedCell);
                
                foreach (var horizonCell in horizon.All) {
                    if (horizonCell.IsFixed) continue;

                    horizonCell.RemoveCandidate(fixedCell.SolutionNumber);
                    if (horizonCell.CandidateNumbers.Length == 0) return false;

                    numberOfCellsFixed += horizonCell.IsFixed ? 1 : 0;
                }
                
                
            }
            return true;
        }
    }
}