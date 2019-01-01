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




