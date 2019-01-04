# Sorting
Write a function to sort an array of integers (or any other type, if your like where comparing values is no problem of its own) in ascending order.

The signature of the function: `int[] Sort(int[] values)`

The function should not sort the values in place in the array passed in. Treat the parameter `values` as immutable.

## Acceptance test cases
### Test case 1
* Input: `[3,10,7,-2,5,10,2,0,9,4]`
* Output: `[-2,0,2,3,4,5,7,9,10,10]`

### Test case 2
* Input: `[]`
* Output: `[]`

## Solving the problem
### Approach 1
It seems the problem of sorting can be made successively more difficult starting from a very simple case.

* `[]` -> `[]` // a new empty array should be returned
* `[2]` -> `[2]` // a new array with the same content should be returned
* `[3,2]` -> `[2,3]` // swapping of two values
* `[2,3]` -> `[2,3]` // no swapping of values required (an alternative is introduced)

So far, so easy. But what's the next slightly more difficult test?

Is it `[3,1,2]` -> `[1,2,3]` or would `[1,3,2]` -> `[1,2,3]` be easier – or would even `[3,2,1]` -> `[1,2,3]` be ok?

Without an idea of how to actually do the sorting, that's hard to say.

Two approaches come to mind:

a. Find the largest value in slice [1..m] and swap with value [m+1]; start with m=n-1. Repeat with m-1 etc.

b. Go through all value pairs and swap if needed. Repeat until no swaps were needed anymore.

Both approaches require swapping which is already implemented at this point. But approach a. introduces a new concept: a maximum value. Hence approach b. seems simpler; it sticks to swapping which now is just done several times.

A slightly more difficult test case for approach b. would be one where only a single pass needs to be done over all values, e.g.

* `[3,1,2]` -> `[1,2,3]`

After that the next even more difficult test case should require several passes. It also should finish the implementation; the acceptance test also should turn green.

* `[3,2,1]` -> `[1,2,3]`

Approach b. is what's called [Bubble Sort](https://en.wikipedia.org/wiki/Sorting_algorithm#Bubble_sort).

#### Refactoring
The solution less than 20 lines long. There's no duplication in it. Refactoring does not seem to be required.

Nevertheless the solution consists of some concepts which are not easily discernable in the code, e.g. swapping, a pass, repetition until no more swaps are needed.

### Approach 2
This time a specific algorithm is to be used to solve the problem: [Quicksort](https://en.wikipedia.org/wiki/Quicksort).

The overall behavior is the same as with approach no. 1, hence the same acceptance tests.

However since the solution approach now is clear there is no need to elicit the logic by incrementally increasing the difficulty of the test cases.

Rather, known aspects of the algorithms can be implemented separately and then be integrated.

#### Picking the pivot
The *pivot* is a value from the array to be sorted which divides all values into two partitions: the ones smaller than the pivot, and the ones larger than the pivot.

Example: `[6,3,7,2,4,5]` with pivot `4` results in partitions `[3,2]` and `[6,7,5]`.

Values in partitions are not ordered.

The pivot picking approach is to select the value in the middle of the array as the pivot.

Test cases for the pivot picking function `int PickPivot(int[] values)`

* `[]` -> exception
* `[4]` -> `4`
* `[4,3]` -> `3`
* `[4,3,2]` -> `3`

#### Partitioning
Partitioning rearranges the values in a array with regard to a pivot. Values less than the pivot are moved to the left of the pivot, values larger are moved to the right.

Example: `[6,3,7,2,4,5]` with pivot `4` results (for example) in `[3,2,4,6,7,5]`.

The order of the values to the left and right of the pivot is not important, though.

The pivot always is a value in the array.

Partitioning seemingly creates two partitions. But when looking more closely it's in fact three partitions: values less, values larger than the pivot, and the pivot itself! The first two partitions might be empty; the pivot partition might contain several elements.

A function creating these partitions can look like this:

`(int[] lessThan, int[] equalTo, int[] largerThan) Partition(int[] values, int pivot)`

Test cases:

* `[4], 4` -> `([], [4], [])`
* `[4,4], 4` -> `([], [4,4], [])`
* `[5,4], 4` -> `([], [4], [5])`
* `[4,2], 4` -> `([2], [4], [])`
* `[5,4,2], 4` -> `([2], [4], [5])`
* `[6,4,3,7,2,4,5], 4` -> `([3,2],[4,4],[6,7,5])`
 
#### Descending
Descending means to apply pivot picking and partitioning recursively to the partitions with the values less than and larger than the pivot.

The return value of such application is a sorted array which then needs to be joined with the other arrays.

Given these partitions

`[3,2],[4,4],[6,7,5]`

will lead to the sorted arrays `[2,3]` and `[5,6,7]` and the result

`[2,3] + [4,4] + [5,6,7]` -> `[2,3,4,4,5,6,7]`

Descending is what's done in the function to produce: `Sort()`

#### Retrospective
Implementing Quicksort did not go as easy as thought. Overall the algorithm is simple. Picking a pivot and integrating it with partitioning into a recursive descent was easy. But I underestimated partitioning.

Even though the description of Quicksort says

> After this partitioning, the pivot is in its final position. 

I did not represent this in the code. That was the reason why the first attempt at partitioning failed when put to the test with the acceptance test data. Since (incidentally) the maximum value was chosen as the pivot there was no larger-than partition - and that threw off the partitioning code.

Even though I tried to be careful during analysis of the partitioning problem I overlooked this case. Not good - but I guess such things just happen.

How to do it better next time?

1. Represent what's 'named' in the requirements. This is now the case with the three partitions returned by `Partition()`: the middle partition stands for the "pivot being in its final position".
2. When implementing complementary partital solutions (instead of progressing incrementally) an "eco-check" should be made: How do certain output values affect downstream processing steps? The test cases of `Partition()` covered the case where one partition was empty. That wasn't a problem with `Partition()` - but it turned out to be a problem with a subsequent recursive call to `Sort()` with the non-empty partition having the same content as the original array.

Partitioning is done along the lines of [Hoare's scheme](https://en.wikipedia.org/wiki/Quicksort#Hoare_partition_scheme), as it turns out. However, values are not treated in place, but copied to a separate array and later even extracted into separate partition arrays.

That's a working solution faithful to the Quicksort definition, but it's not memory efficient. I know.

Memory-efficiency (and CPU-efficiency beyond the Quicksort approach) are matters of optimization which now can be done that the algorithm has been implemented.

#### Optimization
The value copying did not let me sleep well. I had to make it more efficient.

The solution was to introduce slices, e.g. streches of values in the array to sort, e.g. those from index 4 to 7.

That could have been just a formal change, but since there was not array for the pivot values anymore during partitioning
I had to change the partitioning algorithm. It now moves the pivot to the center of a partition in 2 passes of
value seggregation (see code for an explanation).

Unfortunately I forgot to adapt the `PickPivot()` function to properly handle slices. That led to some bug hunting in the wrong
places. I suspected the partitioning to be the culprit despite quite some testing. It did not occurr to me that the pivot picking
wasn't working anymore. Bummer :-(

This version should be more memory efficient. But the paritioning now looks a bit bloated.

So far I haven't looked at any Quicksort implementations. But when I do now and compare it to Hoare's solution... then mine
clearly is less concise, less elegant :-( It does not need two passes to partition properly.

Hm... Why does my code look different? Have I been sloppy in designing test cases? Or did I simply lack a certain spark of creativity?

#### Retrospective II
Straightforward correct solutions are elusive. Programming is messy.

It did not go well with the `PickPivot()` function. Changing it so fundamentally should have prompted me to rethink testing it.
Its former scaffolding tests where gone - but why not add at least a couple of new ones? But the change seemed too small to bother.
How wrong I was...

Lesson no. 1: Changing an operation requires tests to back it. If there are none, then add some.

Lesson no. 2: "TDD as if you meant it" worked nicely for rewriting the partioning. I copied the whole function
into the test and changed its shape and interior. Only when it worked as expected I moved it into the production
code (and deleted the former version).

Putting a piece of code onto a workbench like this (instead of leaving it in place in production code) really
helps focussing (and decoupling parts of code).

Lesson no. 3: I like the use of a continuation to hide the termination condition for the recursion:

```
Descend(() => {
    var pivot = PickPivot(values, iStartOfSlice, iEndOfSlice);
    var partitions = Partition(values, iStartOfSlice, iEndOfSlice, pivot);

    Sort(values, iStartOfSlice, partitions.iEndOfLowerPartition);
    Sort(values, partitions.iStartOfUpperPartition, iEndOfSlice);
});
```

Local functions are great for hiding such details.

Finally I'm happy to have thought of adding some explanations to the code. Mostly that seems to be a burden, but once I started
with it it felt more like some kind of reflection and a way to reach closure. With some comments in place I'm ready to let go
of this solution.