#!/usr/bin/env python3
"""
http://adventofcode.com/day/18

Part 1
------
After the million lights incident, the fire code has gotten
stricter: now, at most ten thousand lights are allowed. You
arrange them in a 100x100 grid.

Never one to let you down, Santa again mails you instructions
on the ideal lighting configuration. With so few lights, he
says, you'll have to resort to animation.

Start by setting your lights to the included initial
configuration (your puzzle input). A # means "on", and a .
means "off".

Then, animate your grid in steps, where each step decides the
next configuration based on the current one. Each light's next
state (either on or off) depends on its current state and the
current states of the eight lights adjacent to it (including
diagonals). Lights on the edge of the grid might have fewer
than eight neighbors; the missing ones always count as "off".

For example, in a simplified 6x6 grid, the light marked A has
the neighbors numbered 1 through 8, and the light marked B, which
is on an edge, only has the neighbors marked 1 through 5:

    1B5...
    234...
    ......
    ..123.
    ..8A4.
    ..765.

The state a light should have next is based on its current
state (on or off) plus the number of neighbors that are on:

  - A light which is on stays on when 2 or 3 neighbors are on, and
     turns off otherwise.
  - A light which is off turns on if exactly 3 neighbors are on,
     and stays off otherwise.
  - All of the lights update simultaneously; they all consider the
     same current state before moving to the next.

Here's a few steps from an example configuration of another 6x6 grid:

    Initial state:
    .#.#.#
    ...##.
    #....#
    ..#...
    #.#..#
    ####..

    After 1 step:
    ..##..
    ..##.#
    ...##.
    ......
    #.....
    #.##..

    After 2 steps:
    ..###.
    ......
    ..###.
    ......
    .#....
    .#....

    After 3 steps:
    ...#..
    ......
    ...#..
    ..##..
    ......
    ......

    After 4 steps:
    ......
    ......
    ..##..
    ..##..
    ......
    ......

After 4 steps, this example has four lights on.

In your grid of 100x100 lights, given your initial
configuration, how many lights are on after 100 steps?

Part 2
------
You flip the instructions over; Santa goes on to point out
that this is all just an implementation of Conway's Game of
Life. At least, it was, until you notice that something's
wrong with the grid of lights you bought: four lights, one
in each corner, are stuck on and can't be turned off. The
example above will actually run like this:

    Initial state:
    ##.#.#
    ...##.
    #....#
    ..#...
    #.#..#
    ####.#

    After 1 step:
    #.##.#
    ####.#
    ...##.
    ......
    #...#.
    #.####

    After 2 steps:
    #..#.#
    #....#
    .#.##.
    ...##.
    .#..##
    ##.###

    After 3 steps:
    #...##
    ####.#
    ..##.#
    ......
    ##....
    ####.#

    After 4 steps:
    #.####
    #....#
    ...#..
    .##...
    #.....
    #.#..#

    After 5 steps:
    ##.###
    .##..#
    .##...
    .##...
    #.#...
    ##...#

After 5 steps, this example now has 17 lights on.

In your grid of 100x100 lights, given your initial configuration,
but with the four corners always in the on state, how many
lights are on after 100 steps?
"""

from __future__ import print_function, unicode_literals
from copy import deepcopy
import os
import sys

INFILE = '../../aoc-inputs/2015/input18.txt'
DIMENSION = 100
STEPS = 100


def count(grid, x, y):
    neighbors = list()

    neighbors.append(grid[x-1][y-1])
    neighbors.append(grid[x-1][y])
    neighbors.append(grid[x-1][y+1])
    neighbors.append(grid[x][y-1])
    neighbors.append(grid[x][y+1])
    neighbors.append(grid[x+1][y-1])
    neighbors.append(grid[x+1][y])
    neighbors.append(grid[x+1][y+1])

    return sum(neighbors)


def life(grid, steps, stuck_corners=False):
    g = deepcopy(grid)

    for i in range(steps):
        g = _life(g, stuck_corners)

    return g


def _life(grid, stuck_corners=False):
    if stuck_corners:
        grid[1][1] = 1
        grid[1][100] = 1
        grid[100][1] = 1
        grid[100][100] = 1

    new_grid = deepcopy(grid)

    for x in range(1, DIMENSION + 1):
        for y in range(1, DIMENSION + 1):
            c = count(grid, x, y)
            s = grid[x][y]

            if s == 1 and c != 2 and c != 3:
                s = 0
            if s == 0 and c == 3:
                s = 1

            new_grid[x][y] = s

    if stuck_corners:
        new_grid[1][1] = 1
        new_grid[1][100] = 1
        new_grid[100][1] = 1
        new_grid[100][100] = 1

    return new_grid


def main():
    # Initial state
    lights = [[0 for x in range(DIMENSION + 2)] for x in range(DIMENSION + 2)]
    with open(INFILE) as f:
        current_line = 1
        current_char = 1

        for line in f:
            for char in line.strip():
                if '#' == char:
                    lights[current_line][current_char] = 1
                current_char += 1
            current_line += 1
            current_char = 1

    # Part 1
    grid = life(lights, STEPS)
    p1count = sum(map(sum, grid))

    # Part 2
    grid = life(lights, STEPS, True)
    p2count = sum(map(sum, grid))

    msg = '[Python]  Puzzle 18-1: {}'
    print(msg.format(p1count))

    msg = '[Python]  Puzzle 18-2: {}'
    print(msg.format(p2count))


if __name__ == '__main__':
    main()
