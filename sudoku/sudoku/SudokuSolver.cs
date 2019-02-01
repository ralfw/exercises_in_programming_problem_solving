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
            Solve(wb);
            return wb.Matrix;
        }
    
        static void Solve(Workbench workbench) {
            var n_fixed_before_pass = workbench.Fixed.Length;
            
            while (SolutionFound() is false) {
                Constrain(workbench);
                Trial_required(() => {
                    Fix_first_unfixed_cell();
                    Solve(workbench);
                });
            }

            
            bool SolutionFound() => workbench.Unfixed.Length == 0;

            void Trial_required(Action onTrial) {
                if (n_fixed_before_pass == workbench.Fixed.Length) {
                    onTrial();
                    return;
                }
                n_fixed_before_pass = workbench.Fixed.Length;
            }

            void Fix_first_unfixed_cell() {
                var toFix = workbench.Unfixed.First();
                toFix.CandidateNumbers.Skip(1).ToList().ForEach(toFix.RemoveCandidate);
            }
        }
    
        
        static void Constrain(Workbench workbench) {
            foreach (var fixedCell in workbench.Fixed)
                foreach (var horizonCell in workbench.Horizon(fixedCell))
                    horizonCell.RemoveCandidate(fixedCell.SolutionNumber);
        }
    }
}