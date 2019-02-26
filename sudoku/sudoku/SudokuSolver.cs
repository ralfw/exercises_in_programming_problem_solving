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
    
        static (bool success, Workbench workbench) Solve(Workbench workbench) {
            if (Constrain() is false) return (false, null);
            if (SolutionFound()) return (true, workbench);
            return While_a_cell_can_be_fixed(
                        Solve);

            
            bool Constrain() {
                while(true) {
                    var status = Constraining.Constrain(workbench);
                    if (status == Constraining.Status.Failure) return false;
                    if (status == Constraining.Status.DeadEnd) break;
                }
                return true;
            }
            
            bool SolutionFound() => workbench.Unfixed.Length == 0;
            
            (bool success, Workbench workbench) While_a_cell_can_be_fixed(Func<Workbench, (bool success, Workbench workbench)> onTryAgain) {
                foreach (var unfixed in workbench.Unfixed) {
                    var (unfixedRow, unfixedCol) = workbench.DetermineCoordinates(unfixed);
                    foreach (var tentativeSolutionNumber in unfixed.CandidateNumbers) {
                        //TODO: Optimize - Cloning for every trial is an expensive operation
                        var tentativeWorkbench = workbench.Clone();
                        tentativeWorkbench[unfixedRow, unfixedCol].SolutionNumber = tentativeSolutionNumber;
                    
                        var (success, solutionWorkbenche) = onTryAgain(tentativeWorkbench);
                    
                        if (success) return (true, solutionWorkbenche);
                    }
                }
                return (false, null);
            }
        }
    }
}