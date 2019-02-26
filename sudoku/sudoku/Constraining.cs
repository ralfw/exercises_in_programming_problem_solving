using System.Collections.Generic;
using System.Linq;

namespace sudoku
{
    static class Constraining
    {
        public enum Status {
            Success,
            DeadEnd,
            Failure
        }
        
        
        public static Status Constrain(Workbench workbench) {
            var allHorizonsToFix = Collect_horizons(workbench).ToArray();
            
            if (Fix_cells(allHorizonsToFix) is false) return Status.DeadEnd;
            if (Check_cells(allHorizonsToFix) is false) return Status.Failure;
            return Status.Success;
        }

        
        static IEnumerable<(Workbench.CellHorizon horizon, int solutionNumber)> Collect_horizons(Workbench workbench) 
            => workbench.Fixed.Select(fc => (workbench.Horizon(fc), fc.SolutionNumber));
        
        static bool Fix_cells(IEnumerable<(Workbench.CellHorizon horizon, int solutionNumber)> horizonsToFix) {
            var allCellsToFix = horizonsToFix.SelectMany(htf => htf.horizon.All.Select(ctf => new{cell=ctf,htf.solutionNumber}))
                .Where(ctf => ctf.cell.IsFixed is false);
            var numberOfCellsFixed = 0;
            foreach (var ctf in allCellsToFix) {
                ctf.cell.RemoveCandidate(ctf.solutionNumber);
                numberOfCellsFixed += ctf.cell.IsFixed ? 1 : 0;
            }
            return numberOfCellsFixed != 0;
        }

        static bool Check_cells((Workbench.CellHorizon horizon, int solutionNumber)[] horizonsToFix) {
            if (Check_for_remaining_candidates(horizonsToFix) is false) return false;
            return Check_for_all_unique_fixes(horizonsToFix);
        }

        static bool Check_for_remaining_candidates(IEnumerable<(Workbench.CellHorizon horizon, int solutionNumber)> horizonsToFix) {
            var allCellsInHorizons = horizonsToFix.SelectMany(htf => htf.horizon.All);
            return allCellsInHorizons.Any(c => c.CandidateNumbers.Length == 0) is false;
        }

        static bool Check_for_all_unique_fixes(IEnumerable<(Workbench.CellHorizon horizon, int solutionNumber)> horizonsToFix) {
            foreach (var h in horizonsToFix) {
                if ((Check_for_unique_fixes(h.horizon.Row) &&
                    Check_for_unique_fixes(h.horizon.Col) &&
                    Check_for_unique_fixes(h.horizon.Box)) == false) return false;
            }
            return true;
        }

        static bool Check_for_unique_fixes(IEnumerable<Workbench.Cell> context) {
            var solutionNumberFrequencies = new Dictionary<int,int>();
            foreach(var c in context)
                if (c.IsFixed) {
                    if (solutionNumberFrequencies.ContainsKey(c.SolutionNumber))
                        solutionNumberFrequencies[c.SolutionNumber]++;
                    else
                        solutionNumberFrequencies[c.SolutionNumber] = 1;
                }
            return solutionNumberFrequencies.Values.Any(v => v > 1) is false;
        }
    }
}