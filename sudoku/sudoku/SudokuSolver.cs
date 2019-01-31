using System.Data;
using System.Diagnostics;

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
            while (SolutionFound() is false)
                Constrain(workbench);
    
            bool SolutionFound() => workbench.Unfixed.Length == 0;
        }
    
        static void Constrain(Workbench workbench) {
            foreach (var fixedCell in workbench.Fixed)
                foreach (var horizonCell in workbench.Horizon(fixedCell))
                    horizonCell.RemoveCandidate(fixedCell.SolutionNumber);
        }
    }
}