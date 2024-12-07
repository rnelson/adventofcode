#!/usr/bin/env python
"""
http://adventofcode.com/day/3

Part 1
------
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

  - > delivers presents to 2 houses: one at the starting location,
     and one to the east.
  - ^>v< delivers presents to 4 houses in a square, including twice
     to the house at his starting/ending location.
  - ^v^v^v^v^v delivers a bunch of presents to some very lucky children
     at only 2 houses.


Part 2
------
The next year, to speed up the process, Santa creates a robot version
of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents
to the same starting house), then take turns moving based on instructions
from the elf, who is eggnoggedly reading from the same script as the previous
year.

This year, how many houses receive at least one present?

For example:

  - ^v delivers presents to 3 houses, because Santa goes north, and then
     Robo-Santa goes south.
  - ^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end
     up back where they started.
  - ^v^v^v^v^v now delivers presents to 11 houses, with Santa going one
     direction and Robo-Santa going the other.
"""

from __future__ import print_function, unicode_literals
import os
import sys

INFILE = '../../aoc-inputs/2015/input03.txt'


def follow_route(route):
    prev = (0, 0)
    houses = [prev]

    for direction in route:
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

    return houses


def main():
    with open(INFILE) as f:
        route = f.read()

        # Part 1
        houses = follow_route(route)
        count = len(set(houses))

        print('[Python]  Puzzle 3-1: {}'.format(count))

        # Part 2
        santa_route = route[::2]
        robo_santa_route = route[1::2]
        santa_houses = follow_route(santa_route)
        robo_santa_houses = follow_route(robo_santa_route)
        combined_count = len(set(santa_houses + robo_santa_houses))

        print('[Python]  Puzzle 3-2: {}'.format(combined_count))


if __name__ == '__main__':
    main()
