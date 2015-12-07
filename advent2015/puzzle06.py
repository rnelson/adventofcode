#!/usr/bin/env python
"""
http://adventofcode.com/day/6

Part 1
------
Because your neighbors keep defeating you in the holiday house
decorating contest year after year, you've decided to deploy one
million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa
has mailed you instructions on how to display the ideal lighting
configuration.

Lights in your grid are numbered from 0 to 999 in each direction;
the lights at each corner are at 0,0, 0,999, 999,999, and 999,0.
The instructions include whether to turn on, turn off, or toggle
various inclusive ranges given as coordinate pairs. Each coordinate
pair represents opposite corners of a rectangle, inclusive; a
coordinate pair like 0,0 through 2,2 therefore refers to 9 lights
in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up
your lights by doing the instructions Santa sent you in order.

For example:

  - turn on 0,0 through 999,999 would turn on (or leave on) every
     light.
  - toggle 0,0 through 999,0 would toggle the first line of 1000
     lights, turning off the ones that were on, and turning on the
     ones that were off.
  - turn off 499,499 through 500,500 would turn off (or leave
     off) the middle four lights.

After following the instructions, how many lights are lit?


Part 2
------
You just finish implementing your winning light pattern when you
realize you mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness
controls; each light can have a brightness of zero or more. The
lights all start at zero.

The phrase turn on actually means that you should increase the
brightness of those lights by 1.

The phrase turn off actually means that you should decrease the
brightness of those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the
brightness of those lights by 2.

What is the total brightness of all lights combined after
following Santa's instructions?

For example:

  - turn on 0,0 through 0,0 would increase the total brightness
     by 1.
  - toggle 0,0 through 999,999 would increase the total brightness
     by 2000000.
"""

from __future__ import print_function, unicode_literals
import os
import re
import sys

INFILE = 'inputs/input06.txt'

# Part 1 parameters
P1REGEX = r'(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)'


def main():
    with open(INFILE) as f:
        # Part 1
        lights = [[0 for x in range(1000)] for x in range(1000)]

        for line in f:
            input = line.strip()
            m = re.search(P1REGEX, input)

            command = m.group(1)
            x1 = int(m.group(2))
            y1 = int(m.group(3))
            x2 = int(m.group(4))
            y2 = int(m.group(5))

            for i in range(x1, x2 + 1):
                for j in range(y1, y2 + 1):
                    if command == 'turn off':
                        lights[i][j] = 0
                    elif command == 'turn on':
                        lights[i][j] = 1
                    elif command == 'toggle':
                        lights[i][j] = 0 if lights[i][j] == 1 else 1
                    else:
                        print('error: unknown command {}: {}'.format(command, input))

        on_count = sum(row.count(1) for row in lights)
        off_count = sum(row.count(0) for row in lights)
        msg = '[Python]  Puzzle 6-1: {} lights on, {} lights off'
        print(msg.format(on_count, off_count))


        # Part 2
        f.seek(0)
        lights = [[0 for x in range(1000)] for x in range(1000)]

        for line in f:
            input = line.strip()
            m = re.search(P1REGEX, input)

            command = m.group(1)
            x1 = int(m.group(2))
            y1 = int(m.group(3))
            x2 = int(m.group(4))
            y2 = int(m.group(5))

            for i in range(x1, x2 + 1):
                for j in range(y1, y2 + 1):
                    if command == 'turn off':
                        lights[i][j] -= 1

                        if lights[i][j] < 0:
                            lights[i][j] = 0
                    elif command == 'turn on':
                        lights[i][j] += 1
                    elif command == 'toggle':
                        lights[i][j] += 2
                    else:
                        print('error: unknown command {}: {}'.format(command, input))

        brightness = sum(sum(row) for row in lights)
        msg = '[Python]  Puzzle 6-2: brightness is {}'
        print(msg.format(brightness))


if __name__ == '__main__':
    main()
