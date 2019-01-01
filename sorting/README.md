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
* `[3,2,1]` -> `[1,2,3]` // swapping needs to be done several times (a loop is introduced)




