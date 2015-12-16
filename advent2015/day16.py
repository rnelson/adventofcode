#!/usr/bin/env python3
"""
http://adventofcode.com/day/16

Part 1
------
Your Aunt Sue has given you a wonderful gift, and you'd like
to send her a thank you card. However, there's a small problem:
she signed it "From, Aunt Sue".

You have 500 Aunts named "Sue".

So, to avoid sending the card to the wrong person, you need
to figure out which Aunt Sue (which you conveniently number
1 to 500, for sanity) gave you the gift. You open the present
and, as luck would have it, good ol' Aunt Sue got you a My
First Crime Scene Analysis Machine! Just what you wanted. Or
needed, as the case may be.

The My First Crime Scene Analysis Machine (MFCSAM for short)
can detect a few specific compounds in a given sample, as well
as how many distinct kinds of those compounds there are.
According to the instructions, these are what the MFCSAM can
detect:

  - children, by human DNA age analysis.
  - cats. It doesn't differentiate individual breeds.
  - Several seemingly random breeds of dog: samoyeds, pomeranians,
       akitas, and vizslas.
  - goldfish. No other kinds of fish.
  - trees, all in one group.
  - cars, presumably by exhaust or gasoline or something.
  - perfumes, which is handy, since many of your Aunts Sue wear
       a few kinds.

In fact, many of your Aunts Sue have many of these. You put the
wrapping from the gift into the MFCSAM. It beeps inquisitively
at you a few times and then prints out a message on ticker tape:

    children: 3
    cats: 7
    samoyeds: 2
    pomeranians: 3
    akitas: 0
    vizslas: 0
    goldfish: 5
    trees: 3
    cars: 2
    perfumes: 1

You make a list of the things you can remember about each Aunt
Sue. Things missing from your list aren't zero - you simply
don't remember the value.

What is the number of the Sue that got you the gift?

Part 2
------
As you're about to send the thank you note, something in the
MFCSAM's instructions catches your eye. Apparently, it has an
outdated retroencabulator, and so the output from the machine
isn't exact values - some of them indicate ranges.

In particular, the cats and trees readings indicates that there
are greater than that many (due to the unpredictable nuclear
decay of cat dander and tree pollen), while the pomeranians
and goldfish readings indicate that there are fewer than that
many (due to the modial interaction of magnetoreluctance).

What is the number of the real Aunt Sue?
"""

from __future__ import print_function, unicode_literals
import os
import re
import sys

INFILE = 'inputs/input16.txt'
EXPR = 'Sue (\d+): (\w+): (\d+), (\w+): (\d+), (\w+): (\d+)'
MFCSAM_RESULTS = {
    'children': 3,
    'cats': 7,
    'samoyeds': 2,
    'pomeranians': 3,
    'akitas': 0,
    'vizslas': 0,
    'goldfish': 5,
    'trees': 3,
    'cars': 2,
    'perfumes': 1
}


def get_sues():
    sues = dict()

    with open(INFILE) as f:
        for line in f:
            input = line.strip()

            m = re.search(EXPR, input)
            number = int(m.group(1))
            p1 = m.group(2)
            p1n = int(m.group(3))
            p2 = m.group(4)
            p2n = int(m.group(5))
            p3 = m.group(6)
            p3n = int(m.group(7))

            props = dict()
            props[p1] = p1n
            props[p2] = p2n
            props[p3] = p3n
            sues[number] = props

    return sues


def find_sue(aunts, props, fix_retro=False):
    sue = -1

    for auntK, auntV in aunts.items():
        found = True

        for k, v in props.items():
            if k in auntV.keys():
                if fix_retro:
                    if k in ('cats', 'trees'):
                        if auntV[k] <= v:
                            found = False
                    elif k in ('pomeranians', 'goldfish'):
                        if auntV[k] >= v:
                            found = False
                    else:
                        if auntV[k] != v:
                            found = False
                elif auntV[k] != v:
                    found = False

        if found:
            sue = auntK

    return sue


def main():
    aunts = get_sues()

    # Part 1
    winner = find_sue(aunts, MFCSAM_RESULTS)
    msg = '[Python]  Puzzle 16-1: {}'
    print(msg.format(winner))

    # Part 2
    winner = find_sue(aunts, MFCSAM_RESULTS, True)
    msg = '[Python]  Puzzle 16-2: {}'
    print(msg.format(winner))


if __name__ == '__main__':
    main()
