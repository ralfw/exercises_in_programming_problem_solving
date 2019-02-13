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
            while (Constrain(workbench) > 0) {}
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
    
        
        internal static int Constrain(Workbench workbench) {
            var numberOfCellsFixedInThisPass = 0;
            
            foreach (var fixedCell in workbench.Fixed)
            foreach (var horizonCell in workbench.Horizon(fixedCell)) {
                if (horizonCell.IsFixed) continue;
                
                horizonCell.RemoveCandidate(fixedCell.SolutionNumber);
                numberOfCellsFixedInThisPass += horizonCell.IsFixed ? 1 : 0;
            }
            
            return numberOfCellsFixedInThisPass;
        }
    }
}