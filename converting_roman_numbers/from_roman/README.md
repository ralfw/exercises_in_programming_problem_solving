# From Roman

With this problem I wanted to explicitly try different ways of testing.

## Approach no. 1
Approach no. 1 uses classic TDD (cTDD). The test cases rise incrementally
in their difficulty.

Converting "I" is easier than converting "VI" which is easier than converting "IV".

Nevertheless the first test I wrote was an overall acceptance test. That way I'm drawing a finish
line in the ground. Once the acceptance tests pass I know I have a mature implementation of
a solution.

Also the acceptance tests explicitly describe my understanding of the problem in the first place.

Converging on a solution with incremental tests through the one and only function signature was easy.

What I did not expect, though, was how I solved the final sub-problem: obeying the subtraction law.

```
return roman.Select(MapDigit)
            .Aggregate(new List<int>(), AdjustValuesForSubtractionRule)
            .Sum();
```

Just slipping the `Aggregate()` between `Select()` and `Sum()` took me by surprise, so to speak.
At first I was unhappy with the mapping being done by `Select()` without a way to intervene.
But then I thought, why not work just on the values without knowing where they came from.

## Approach no. 2
In this approach I consciously did not start with looking for test increments.
After the acceptance tests I asked myself: What could a *process* look like to solve the problem.

By process I mean a sequence of steps connected in a data flow.

The driving force behind my thinking was to get rid of some limiting factor.
"Wouldn't it be easier, if the roman digits where gone, so I can deal only with decimal values?"
Bam! That led to a first tentative process:

1. Get rid of roman digits
2. Handle the remaining problem

Now, what's the limiting factor in handling the rest? The pesky subtraction law (e.g. IV -> 4).
"Wouldn't it be easier, if I could just focus on adding digit values?"

1. Get rid of roman digits
2. Handle the remaining problem
    1. Sum-up all the values

And finally of course the subtraction problem:

1. Get rid of roman digits
2. Handle the remaining problem
    1. Handle subtraction law
    2. Sum-up all the values

It's "obvious" the original problem would be solved if I could solve each of the smaller problems.

I call them *complementary* problems, because their solutions are all needed and need to work together to
solve the original problem. They fit together like puzzle pieces.

With incremental problems each level adds to the solution. I could even stop after each level and be content
with a less powerful solution.

With complementary problems it's different. I cannot stop after solving one. All must be solved and stringed
together in a process to solve the original problem.

Focusing on each of the complementary problems then is like attacking a new problem. I'm back to square one.
I have to decide how to tackle this one sub-problem: with incremental tests or again with complementary sub-problems?

Problem solving is a recursive process.

## Approach no. 3
This time I just wanted to see if I could come up with a different process.

Thinking more in terms of a compilation problem here I start with finding "units of meaning" (tokens) in roman numbers:

* an "M" or "V" can be such a token (a single digit token)
* or "CM" or "IV" can be such a token

The question behind that was "What if IV was just a single roman symbol as M? Then there would not be a subtraction problem."

## Retrospective
Attacking the problem with increments or complements did not make a big difference.

But it became (once more) clear to me: I personally like to start with looking for complements first.
I find it hard to simplify a problem first (beyond the trivial). Better then to first cut it into smaller
problems and solve them separately.