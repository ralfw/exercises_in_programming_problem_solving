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
This time a specific algorithm is to solve the problem: [Quicksort](https://en.wikipedia.org/wiki/Quicksort).

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
* `[4,3]` -> `4`
* `[4,3,2]` -> `3`

#### Partitioning
Partitioning rearranges the values in a array with regard to a pivot. Values less than the pivot are moved to the left, values larger are moved to the right.

Example: `[6,3,7,2,4,5]` with pivot `4` results (for example) in `[3,2,4,6,7,5]`.

The order of the values to the left and right of the pivot is not important, though.

The pivot always is a value in the array.

Test cases for the partitioning function `int[] Partition(int[] values, int pivot)`

* `[4], 4` -> `[4]`
* `[3,4], 4` -> `[3,4]`

After these test cases what's the next slightly more difficult one? Is it

a. `[4,3], 4` -> `[3,4]`

or

b. `[3,4,5], 4` -> `[3,4,5]`

This depends on the solution approach:

Only a single pass over the array should be necessary in any case.

* A value at i is compared to the pivot.
* If it's less or equal it's put in the left partition; else it's put in the right partition.
* Both partitions grow from the edges towards the center of the array.

With this in mind the next test case should be:

`[4,3], 4` -> `[3,4]`

It only requires an addition to the left partition. And then

`[3,4,5], 4` -> `[3,4,5]`

which is equivalent to

`[5,4,3], 4` -> `[3,4,5]`

At least if taken seriously.
 
#### Descending
Descending means partitioning is repeated on the partitions of an array (recursion!).

If `[3,2,4,6,7,5]` is partitioned around pivot `4`, then the whole process descends down into `[3,2,4]` and `[6,7,5]`.

And then again down into `[2]/[3,4]` and `[6,5]/[7]`.

Arrays of length <= 2 don't have to be descended into, though, after they have been through partitioning.

At the core of descending is separating the partitions, e.g.

* `[3,4,2,6,7,5], 4` -> `[3,4,2], [6,7,5]`

The function signature for this:

`(int[] lower, int[] upper) SeparatePartitions(int[] values, int pivot)`

Alternatively the partitioning phase could deliver the partitions right away:

`(int[] lower, int[] upper) Partition(int[] values, int pivot)`

Descending then mainly is picking the pivot and partitoning for each partition in turn - and finally appending the results during ascension:

* `[2]`+`[3,4]` -> `[2,3,4]` and `[5,6]` + `[7]` -> `[5,6,7]`
* `[2,3,4]` + `[5,6,7]` -> `[2,3,4,5,6,7]`










