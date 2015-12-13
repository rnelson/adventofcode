#!/usr/bin/env python
"""
http://adventofcode.com/day/13

Part 1
------
iIn years past, the holiday feast with your family hasn't gone
so well. Not everyone gets along! This year, you resolve, will
be different. You're going to find the optimal seating
arrangement and avoid all those awkward conversations.

You start by writing up a list of everyone invited and the
amount their happiness would increase or decrease if they were
to find themselves sitting next to each other person. You have
a circular table that will be just big enough to fit everyone
comfortably, and so each person will have exactly two neighbors.

For example, suppose you have only four attendees planned, and
you calculate their potential happiness as follows:

  - Alice would gain 54 happiness units by sitting next to Bob.
  - Alice would lose 79 happiness units by sitting next to Carol.
  - Alice would lose 2 happiness units by sitting next to David.
  - Bob would gain 83 happiness units by sitting next to Alice.
  - Bob would lose 7 happiness units by sitting next to Carol.
  - Bob would lose 63 happiness units by sitting next to David.
  - Carol would lose 62 happiness units by sitting next to Alice.
  - Carol would gain 60 happiness units by sitting next to Bob.
  - Carol would gain 55 happiness units by sitting next to David.
  - David would gain 46 happiness units by sitting next to Alice.
  - David would lose 7 happiness units by sitting next to Bob.
  - David would gain 41 happiness units by sitting next to Carol.

Then, if you seat Alice next to David, Alice would lose 2
happiness units (because David talks so much), but David would
gain 46 happiness units (because Alice is such a good
listener), for a total change of 44.

If you continue around the table, you could then seat Bob next
to Alice (Bob gains 83, Alice gains 54). Finally, seat Carol,
who sits next to Bob (Carol gains 60, Bob loses 7) and David
(Carol gains 55, David gains 41). The arrangement looks like
this:

         +41 +46
    +55   David    -2
    Carol       Alice
    +60    Bob    +54
         -7  +83

After trying every other seating arrangement in this
hypothetical scenario, you find that this one is the most
optimal, with a total change in happiness of 330.

What is the total change in happiness for the optimal seating
arrangement of the actual guest list?

Part 2
------
In all the commotion, you realize that you forgot to seat
yourself. At this point, you're pretty apathetic toward the
whole thing, and your happiness wouldn't really go up or down
regardless of who you sit next to. You assume everyone else
would be just as ambivalent about sitting next to you, too.

So, add yourself to the list, and give all happiness
relationships that involve you a score of 0.

What is the total change in happiness for the optimal seating
arrangement that actually includes yourself?
"""

from __future__ import print_function, unicode_literals
from getpass import getuser
from itertools import permutations
import os
import re
import sys

INFILE = 'inputs/input13.txt'
EXPR = r'(\w+) would (lose|gain) (\d+) happiness units by sitting next to (\w+).'


def find(people, values):
    min_path = None
    min_distance = sys.maxint
    max_path = None
    max_distance = -sys.maxint - 1

    for p in permutations(people):
        value = 0

        for i in zip(p, p[1:]):
            value += values[i]
        for i in zip(p[::-1], p[-2::-1]):
            value += values[i]
        value += values[(p[0], p[-1])]
        value += values[(p[-1], p[0])]

        if value < min_distance:
            min_distance = value
            min_path = p
        if value > max_distance:
            max_distance = value
            max_path = p

    return ((min_path, min_distance), (max_path, max_distance))


def main():
    people = set()
    values = dict()

    with open(INFILE) as f:
        for line in f:
            input = line.strip()

            m = re.search(EXPR, input)
            source = m.group(1)
            destination = m.group(4)
            length = int(m.group(3))
            change = m.group(2)

            if 'lose' == change:
                length = -length

            people.add(source)
            people.add(destination)
            values[(source, destination)] = length

        # Part 1
        minimum, maximum = find(people, values)
        path, distance = maximum
        msg = '[Python]  Puzzle 13-1: {} = {}'
        print(msg.format(path, distance))

        # Part 2
        me = getuser()
        people.add(me)
        for p in people:
            values[(me, p)] = 0
            values[(p, me)] = 0

        minimum, maximum = find(people, values)
        path, distance = maximum
        msg = '[Python]  Puzzle 13-2: {} = {}'
        print(msg.format(path, distance))


if __name__ == '__main__':
    main()
