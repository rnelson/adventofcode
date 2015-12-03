#!/usr/bin/python
"""
http://adventofcode.com/day/1

Santa is trying to deliver presents in a large apartment building, but he
can't find the right floor - the directions he got are a little confusing.
He starts on the ground floor (floor 0) and then follows the instructions
one character at a time.

An opening parenthesis, (, means he should go up one floor, and a closing
parenthesis, ), means he should go down one floor.

The apartment building is very tall, and the basement is very deep; he will
never find the top or bottom floors.

For example:

  - (()) and ()() both result in floor 0.
  - ((( and (()(()( both result in floor 3.
  - ))((((( also results in floor 3.
  - ()) and ))( both result in floor -1 (the first basement level).
  - ))) and )())()) both result in floor -3.

To what floor do the instructions take Santa?
"""

from __future__ import print_function, unicode_literals
import os
import sys

INFILE = 'inputs/input01.txt'


def main():
    with open(INFILE) as f:
        data = f.read()

        floor = 0
        for character in data:
            if '(' == character:
                floor = floor + 1
            elif ')' == character:
                floor = floor - 1
            else:
                print('error: invalid input: {}'.format(character))
                sys.exit(1)

        print('{}'.format(floor))

if __name__ == '__main__':
    main()