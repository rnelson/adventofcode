#!/usr/bin/python
"""
http://adventofcode.com/day/2

The elves are also running low on ribbon. Ribbon is all the same width, so
they only have to worry about the length they need to order, which they
would again like to be exact.

The ribbon required to wrap a present is the shortest distance around its
sides, or the smallest perimeter of any one face. Each present also
requires a bow made out of ribbon as well; the feet of ribbon required for
the perfect bow is equal to the cubic feet of volume of the present. Don't
ask how they tie the bow, though; they'll never tell.

For example:

  - A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to
       wrap the present plus 2*3*4 = 24 feet of ribbon for the bow, for a
       total of 34 feet.
  - A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to
       wrap the present plus 1*1*10 = 10 feet of ribbon for the bow, for a
       total of 14 feet.

How many total feet of ribbon should they order?
"""

from __future__ import print_function, unicode_literals
from copy import deepcopy
from operator import mul
import os
import sys

INFILE = 'inputs/input04.txt'


def main():
    total = 0

    with open(INFILE) as f:
        for line in f:
            dim = map(int, line.split('x'))
            small = deepcopy(dim)
            small.remove(max(small))

            total = total + 2 * sum(small) + reduce(mul, dim)

        print(total)

if __name__ == '__main__':
    main()