using System;
using System.Data;
using System.Diagnostics;
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
            while(true) {
                var status = Constraining.Constrain(workbench);
                if (status == Constraining.Status.Failure) return (false, null);
                if (status == Constraining.Status.DeadEnd) break;
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
    }
}