#!/usr/bin/env python
"""
http://adventofcode.com/day/9

Part 1
------
Today, the Elves are playing a game called look-and-say. They
take turns making sequences by reading aloud the previous sequence
and using that reading as the next sequence. For example, 211 is
read as "one two, two ones", which becomes 1221 (1 2, 2 1s).

Look-and-say sequences are generated iteratively, using the
previous value as input for the next step. For each step, take
the previous value, and replace each run of digits (like 111)
with the number of digits (3) followed by the digit itself (1).

For example:

  - 1 becomes 11 (1 copy of digit 1).
  - 11 becomes 21 (2 copies of digit 1).
  - 21 becomes 1211 (one 2 followed by one 1).
  - 1211 becomes 111221 (one 1, one 2, and two 1s).
  - 111221 becomes 312211 (three 1s, two 2s, and one 1).

Starting with the digits in your puzzle input, apply this
process 40 times. What is the length of the result?

Part 2
------
Neat, right? You might also enjoy hearing John Conway talking
about this sequence (that's Conway of Conway's Game of Life fame).

Now, starting again with the digits in your puzzle input,
apply this process 50 times. What is the length of the new
result?
"""

from __future__ import print_function, unicode_literals
from itertools import groupby
import os
import re
import sys

INFILE = 'inputs/input10.txt'


def work(input, iterations):
    output = ''
    s = input

    for i in xrange(iterations):
        new_str = ''
        for k, g in groupby(s):
            new_str = new_str + str(len(list(g))) + str(k)
        s = new_str

    return s


def work2(input, iterations):
    output = input

    for i in xrange(iterations):
        output = ''.join([str(len(list(g))) + str(k) for k, g in groupby(output)])
    return output


def main():
    input = ''
    with open(INFILE) as f:
        # Read the input
        for line in f:
            input = line.strip()

    # Part 1
    result = work2(input, 40)
    msg = '[Python]  Puzzle 10-1: {}'
    print(msg.format(len(result)))

    # Part 2
    result = work2(input, 50)
    msg = '[Python]  Puzzle 10-2: {}'
    print(msg.format(len(result)))


if __name__ == '__main__':
    main()
