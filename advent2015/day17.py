#!/usr/bin/env python3
"""
http://adventofcode.com/day/17

Part 1
------
The elves bought too much eggnog again - 150 liters this time. To
fit it all into your refrigerator, you'll need to move it into
smaller containers. You take an inventory of the capacities of
the available containers.

For example, suppose you have containers of size 20, 15, 10, 5,
and 5 liters. If you need to store 25 liters, there are four ways
to do it:

  - 15 and 10
  - 20 and 5 (the first 5)
  - 20 and 5 (the second 5)
  - 15, 5, and 5

Filling all containers entirely, how many different combinations
of containers can exactly fit all 150 liters of eggnog?

Part 2
------
While playing with all the containers in the kitchen, another load
of eggnog arrives! The shipping and receiving department is
requesting as many containers as you can spare.

Find the minimum number of containers that can exactly fit all
150 liters of eggnog. How many different ways can you fill that
number of containers and still hold exactly 150 litres?

In the example above, the minimum number of containers was two.
There were three ways to use that many containers, and so the
answer there would be 3.
"""

from __future__ import print_function, unicode_literals
from itertools import combinations
import os
import re
import sys

INFILE = 'inputs/input17.txt'


def main():
    containers = list()
    with open(INFILE) as f:
        for line in f:
            containers.append(int(line.strip()))

    # Part 1
    p1count = 0
    for s in range(len(containers)):
        for c in combinations(containers, s):
            if sum(c) == 150:
                p1count += 1

    # Part 2
    p2sizes = dict()
    p2min = len(containers)

    for i in range(p2min):
        p2sizes[i] = 0

    for s in range(len(containers)):
        for c in combinations(containers, s):
            if sum(c) == 150:
                if len(c) < p2min:
                    p2min = len(c)
                p2sizes[s] += 1

    msg = '[Python]  Puzzle 17-1: {}'
    print(msg.format(p1count))

    msg = '[Python]  Puzzle 17-2: {}'
    print(msg.format(p2sizes[p2min]))


if __name__ == '__main__':
    main()
