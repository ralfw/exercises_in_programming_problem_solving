# Writing a Sudoku-Solver
#### Motivation
This is a challenge I wanted to take up since long. I read about Ron Jeffries not completing it with classical TDD (cTDD). So I'd like to give it a try and see how far I get without the urge or need to stick to cTDD or any specific method.

For that I explicitly do not look at Ron Jeffries' or anybody else's code. 

## Problem description
Sudoku is described [by Wikipedia](https://en.wikipedia.org/wiki/Sudoku) like this:

> The objective is to fill a 9×9 grid with digits so that each column, each row, and each of the nine 3×3 subgrids that compose the grid (also called "boxes", "blocks", or "regions") contains all of the digits from 1 to 9. The puzzle setter provides a partially completed grid, which for a well-posed puzzle has a single solution.

> Completed games are always a type of Latin square with an additional constraint on the contents of individual regions. For example, the same single integer may not appear twice in the same row, column, or any of the nine 3×3 subregions of the 9x9 playing board.

As an example this is the puzzle from Wikipedia:

![](images/500px-Sudoku_Puzzle_by_L2G-20050714_standardized_layout.svg.png)

And this is the solution from Wikipedia:

![](images/500px-Sudoku_Puzzle_by_L2G-20050714_solution_standardized_layout.svg.png)

## Analysis
A Sudoku puzzle consist of a square *grid* of *boxes*, with each box again being a square grid of *cells*. The nested grids (grid of boxes, grid of cells) are of the same *size* n.

Typically n is 3, but I think any number >1 would be ok:

* a puzzle consists of n x n boxes
* a box consists of n x n cells
* a puzzle consists of n x n x n x n cells

![](images/domainlang.jpg)

...and each *column* and *row* of the puzzle would contain n x n cells:

![](images/domainlang2.jpg)

Solving a puzzle means to put the numbers 1 .. n x n into all cells in a way so that they only appear once in each box, in each row, and each column:

![](images/fillingout.jpg)

In order to gain some experience playing Sudoku I played it online [here](https://sudoku.game) and [here](https://www.websudoku.com).

### Function to deliver
The Sudoku solver can look quite simple:

`int[,] Solve(int[,] puzzle)`

The interface with the outside world needs to be just a square integer matrix with a width being the square of some (e.g. 4 (n=2), 9 (n=3), 16 (n=4)). Each array element represents a cell of the puzzle. Cells not set in the puzzle contain 0. All other cells contain a number in the range of 1 .. n x n.

A matrix with all cells set to a number in that range is returned as the solution. If none can be found an exception is thrown. Only square matrixes with a size > 1 can be processed; other matrixes cause an exception.

The function only returns a single result even if multiple results might exist. Picking the first result found during the solution process is fine.

### Acceptance criteria
The solution is accepted if it's able to solve the following puzzle with n=2

![](images/puzzle2-01.png)

and this with n=3 taken from [here](http://elmo.sbs.arizona.edu/sandiway/sudoku/examples.html):

![](images/puzzle3.1-01.png)

Checking the correctness of a solution can be done without sample data. All boxes, rows, and columns just have to be checked for completeness: have all numbers 1 .. n x n been assigned?

The encoding of the first puzzle should look like this:

```
var puzzle = new[,]{
  {0,0,3,0},
  {3,0,0,2},
  {0,1,0,0},
  {4,0,0,1}
}
```

## Design


