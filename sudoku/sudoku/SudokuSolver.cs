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
            return Solve(wb).Matrix;
        }
    
        static Workbench Solve(Workbench workbench) {
            while (SolutionFound() is false) {
                if (Constrain(workbench) > 0) continue;
                
                Fix_first_unfixed_cell();
                workbench = Solve(workbench);
            }
            return workbench;

            
            bool SolutionFound() => workbench.Unfixed.Length == 0;

            void Fix_first_unfixed_cell() {
                var toFix = workbench.Unfixed.First();
                toFix.CandidateNumbers.Skip(1).ToList().ForEach(toFix.RemoveCandidate);
            }
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