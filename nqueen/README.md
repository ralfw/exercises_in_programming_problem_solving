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



### Finding complementary subproblems





