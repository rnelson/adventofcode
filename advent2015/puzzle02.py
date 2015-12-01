#!/usr/bin/python
"""
http://adventofcode.com/day/1

Now, given the same instructions, find the position of the first character
that causes him to enter the basement (floor -1). The first character in the
instructions has position 1, the second character has position 2, and so on.

For example:

  - ) causes him to enter the basement at character position 1.
  - ()()) causes him to enter the basement at character position 5.

What is the position of the character that causes Santa to first enter the
basement?
"""

from __future__ import print_function, unicode_literals
import os
import sys

INFILE = 'inputs/input2.txt'


def print_and_exit(message, return_code=0):
    print(message)
    sys.exit(return_code)


def main():
    with open(INFILE) as f:
        data = f.read()

        floor = 0
        position = 1
        for character in data:
            if '(' == character:
                floor = floor + 1
            elif ')' == character:
                floor = floor - 1
            else:
                print('error: invalid input: {}'.format(character))
                sys.exit(1)

            if floor < 0:
                print_and_exit(position)

            position = position + 1

        print('error: did not enter the floor')

if __name__ == '__main__':
    main()
