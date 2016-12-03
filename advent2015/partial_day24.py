#!/usr/bin/env python
"""
http://adventofcode.com/day/24

Part 1
------
It's Christmas Eve, and Santa is loading up the sleigh for this year's
deliveries. However, there's one small problem: he can't get the sleigh
to balance. If it isn't balanced, he can't defy physics, and nobody gets
presents this year.

No pressure.

Santa has provided you a list of the weights of every package he needs
to fit on the sleigh. The packages need to be split into three groups
of exactly the same weight, and every package has to fit. The first group
goes in the passenger compartment of the sleigh, and the second and third
go in containers on either side. Only when all three groups weigh exactly
the same amount will the sleigh be able to fly. Defying physics has rules,
you know!

Of course, that's not the only problem. The first group - the one going in
the passenger compartment - needs as few packages as possible so that Santa
has some legroom left over. It doesn't matter how many packages are in either
of the other two groups, so long as all of the groups weigh the same.

Furthermore, Santa tells you, if there are multiple ways to arrange the
packages such that the fewest possible are in the first group, you need to
choose the way where the first group has the smallest quantum entanglement
to reduce the chance of any "complications". The quantum entanglement of
a group of packages is the product of their weights, that is, the value you
get when you multiply their weights together. Only consider quantum
entanglement if the first group has the fewest possible number of packages
in it and all groups weigh the same amount.

For example, suppose you have ten packages with weights 1 through 5 and 7
through 11. For this situation, the unique first groups, their quantum
entanglements, and a way to divide the remaining packages are as follows:

    Group 1;             Group 2; Group 3
    11 9       (QE= 99); 10 8 2;  7 5 4 3 1
    10 9 1     (QE= 90); 11 7 2;  8 5 4 3
    10 8 2     (QE=160); 11 9;    7 5 4 3 1
    10 7 3     (QE=210); 11 9;    8 5 4 2 1
    10 5 4 1   (QE=200); 11 9;    8 7 3 2
    10 5 3 2   (QE=300); 11 9;    8 7 4 1
    10 4 3 2 1 (QE=240); 11 9;    8 7 5
    9 8 3      (QE=216); 11 7 2;  10 5 4 1
    9 7 4      (QE=252); 11 8 1;  10 5 3 2
    9 5 4 2    (QE=360); 11 8 1;  10 7 3
    8 7 5      (QE=280); 11 9;    10 4 3 2 1
    8 5 4 3    (QE=480); 11 9;    10 7 2 1
    7 5 4 3 1  (QE=420); 11 9;    10 8 2

Of these, although 10 9 1 has the smallest quantum entanglement (90), the
configuration with only two packages, 11 9, in the passenger compartment
gives Santa the most legroom and wins. In this situation, the quantum
entanglement for the ideal configuration is therefore 99. Had there been
two configurations with only two packages in the first group, the one with
the smaller quantum entanglement would be chosen.

What is the quantum entanglement of the first group of packages in the
ideal configuration?


Part 2
------
"""

from __future__ import print_function, unicode_literals
from functools import reduce
from itertools import combinations
from operator import mul
import os
import sys

INFILE = 'inputs/input24.txt'


def find(weights, group_count):
    total = sum(weights)

    for i in range(len(weights)):
        qe_list = [reduce(mul, c) for c in combinations(weights, i)
                   if sum(c) == total]

        if qe_list:
            return min(qe_list)


def main():
    weights = list()
    options = list()

    with open(INFILE) as f:
        for line in f:
            weights.append(int(line.strip()))

    # Part 1
    for p in []:  # permutations(weights):
        for i in range(len(weights)):
            for j in range(len(weights) - i):
                k = len(weights) - i - j

                all_positive = i > 0 and j > 0 and k > 0
                all_included = i + j + k == len(weights)

                if all_positive and all_included:
                    group1 = weights[:i]
                    group2 = weights[i+1:i+j]
                    group3 = weights[i+j+1:]

                    if sum(group1) == sum(group2) == sum(group3):
                        print('> match! {} {} {} / {} {} {} / {} {} {}'.format(i, j, k, len(group1), len(group2), len(group3), sum(group1), sum(group2), sum(group3)))

                    #print('{}  j={} j={} k={}'.format(len(weights), i, j, k))
                    #print('    {}/{}/{}'.format(len(group1), len(group2), len(group3)))

    # Part 1
    msg = '[Python]  Puzzle 24-1: {}'
    print(msg.format(find(weights, 3)))

    # Part 2
    msg = '[Python]  Puzzle 24-2: {}'
    print(msg.format(find(weights, 4)))


if __name__ == '__main__':
    main()
