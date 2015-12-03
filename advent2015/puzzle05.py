#!/usr/bin/env python
"""
http://adventofcode.com/day/3

Santa is delivering presents to an infinite two-dimensional grid of
houses.

He begins by delivering a present to the house at his starting
location, and then an elf at the North Pole calls him via radio and
tells him where to move next. Moves are always exactly one house to
the north (^), south (v), east (>), or west (<). After each move, he
delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much
eggnog, and so his directions are a little off, and Santa ends up
visiting some houses more than once. How many houses receive at least
one present?

For example:

  - > delivers presents to 2 houses: one at the starting location, and one to the east.
  - ^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
  - ^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.
"""

from __future__ import print_function, unicode_literals
import os
import sys

INFILE = 'inputs/input05.txt'


def main():
    # Starts at the first house
    prev = (0, 0)  # (x, y) coordinate of the house
    houses = [prev]

    with open(INFILE) as f:
        for line in f:
            for direction in line:
                if direction == '<':
                    current = (prev[0] - 1, prev[1])
                elif direction == '^':
                    current = (prev[0], prev[1] + 1)
                elif direction == '>':
                    current = (prev[0] + 1, prev[1])
                elif direction == 'v':
                    current = (prev[0], prev[1] - 1)
                
                houses.append(current)
                prev = current

        print(len(set(houses)))

if __name__ == '__main__':
    main()
