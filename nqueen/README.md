# The N-Queen Problem
Write a function to return all solutions to the n-queen problem for a given *n*:

From [Wikipedia](https://en.wikipedia.org/wiki/Eight_queens_puzzle):

> The [n] queens puzzle is the problem of placing [n] chess queens on an [n]×[n] chessboard so that no two queens threaten each other.

## Analysis
As it turns out, the n-queen problem only really is a problem for n>=4.

A visual list of all solutions for n=4..9 can be found [here](https://stamm-wilbrandt.de/en/xsl-list/n-queens/n-queens.xsl.xml).

* For n=1 there is no problem, because only a single queen can be placed and it never can threaten itself.
* For n=2 there is no problem, because after the first queen is placed all remaining squares are under threat. The remaining second queen cannot be placed.
* For n=3 the first queen puts almost all squares under threat, and the second queen covers the rest.

But from n=4 on things become different:

![](images/4queens.png)

A queen on some _square_ of the _board_ threatens all squares in the same _column_:

![](images/3col.png)

And all squares in the same _row_:

![](images/3row.png)

And all squares in the _diagonals_ "radiating" from it:

![](images/3diag.png)

The image first shows the *descending* and then the *ascending* diagonal.

A queen placed right in the middle of a 3x3 board thus threatens all remaining squares. But one placed on any other squares leaves room to place a second one:

![](images/3threats.jpg)

After a queen has been placed on a square not under threat, the next queen of course can only be place on one of the remaining squares still not under threat.

There is no rule or restriction other than that on how to place queens. Just do it until either all n queens have been placed - or there are no more _safe_ squares (i.e. squares not threatened).

### Acceptance criteria
The solution will be accepted if the following function has been implemented:

```
Solution[] Solve(int n)
```

with

```
class Solution {
  public Position[] Queens;
  
  public class Position {
    public char Col; // 'a'..char(n) (e.g. 'd' for n=4)
    public int Row; // 1..n
  }
}
```

And the function returns the solutions for n=4 and n=6.

Also, if n<4 no solution will be returned.

## Solution design
### Finding simpler subproblems
I don't see a way to simplify the problem except for reducing n from the usual 8 to the minimal 4 for a start. But that does not help making the problem to (initially) solve smaller. Solving it for n=4 requires the same features as for n=6 or n=8 etc.

Of course it would be simpler if a queen would be less threatening, e.g. if she only threatened a row instead of row + column + diagonals. But a test relying on this simplification would not be valid anymore once the queens "power" is increased.

What's left is that n<4 is easier to solve then n>=4. But that's trivial.

### Finding complementary subproblems
The overall problem seems to consist of three major complementary subproblems:

* Repeatedly placing a queen on a safe square.
* Determining the squares still safe.
* Roll back a solution if it turns out a queen cannot be placed due to a lack of safe squares. Then one or more previous queens have to be repositioned to remove their threats from some squares.

#### Placing a queen
The position for the current queen could be chosen at random. But that would maybe make it harder to roll back, in any case it would lead to collisions with already placed queens. Better to progress from queen to queen in a more systematic way.

Queens could be placed column by column. The first queen somewhere in the first column (a1..an)m the second queen in the second column (b1..bn) etc.

Once queen i has been place in column i other squares will be under threat. Of course, then not all squares in column i+1 are safe anymore for queen i+1.

Within a column placement can be attempted from row 1 to row n.

#### Determining a square's safety
When a queen is placed all squares threatened by her could be marked as unsafe by registering the positions in some data structure. That could be a list of unsafe positions or a square board with tri-state squares (safe,unsafe,occupied).

Alternatively only the positions of placed queens could be recorded - and then to check if a candidate square is safe it's checked against the "threat vectors" of the queens present.

It seems that marking threatened squares would require the same effort as checking a candidate square for a threat - plus keeping a data structure around.

#### Rolling back a solution
If queens would be placed systematically column by column and row by row and only queen positions would be recorded, it would be easy to roll back a solution.

If queen i cannot be placed anywhere in its column i then queen i-1 has to be repositioned. That means it needs to move to the next row with a safe square in its column. From there placing queen i is tried again.

If no safe squares are left in column i-1 then the solution has to be rolled back to queen i-2 etc.

This is a backtracking problem to be best solved with recursion, I guess.

#### Integration
How could solutions for these subproblems be shaped so that I can easily assemble them into an overall solution?

I think the easiest part is checking for square safety:

`bool IsSafe(Position candidatePosition, Position[] queens)`

Then there is placing the next queen:

`void PlaceQueen(int n, int i, Position[] queens, Action<Solution> onSolutionFound)`

Placement needs to know which queen it's about (i.e. into which column it goes) and which queens have already been placed so it can be determined which squares in the column are safe.

But since the solution is recursive this is the method to call itself. It thus needs to know if there are any queens left to place.

Plus it needs to have a way to report a solution if one was found. I like to use a continuation for that instead of a data structure which accumulates results.

The hardest problem, it seems, is checking for square safety. All else is enumeration (columns, rows) and tracking of queen positions.

* Rows are enumerated in `PlaceQueen()`.
* Columns are enumerated by calling `PlaceQueen()` with the next queen's number.
* The queen positions passed to `PlaceQueen()` in the end are what goes into a `Solution{}`. Collecting all solutions is a trivial matter.

The wrapper for all this is `Solve()` which kicks-off queen placement and collects the solutions in a continuation.

Bottom-up development seems feasible:

1. `IsSafe()`
2. `PlaceQueen()`
3. `Solve()`

### Solving the safety check




