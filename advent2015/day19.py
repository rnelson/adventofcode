#!/usr/bin/env python3
"""
http://adventofcode.com/day/19

Part 1
------
Rudolph the Red-Nosed Reindeer is sick! His nose isn't
shining very brightly, and he needs medicine.

Red-Nosed Reindeer biology isn't similar to regular reindeer
biology; Rudolph is going to need custom-made medicine.
Unfortunately, Red-Nosed Reindeer chemistry isn't similar to
regular reindeer chemistry, either.

The North Pole is equipped with a Red-Nosed Reindeer nuclear
fusion/fission plant, capable of constructing any Red-Nosed
Reindeer molecule you need. It works by starting with some input
molecule and then doing a series of replacements, one per step
until it has the right molecule.

However, the machine has to be calibrated before it can be
used. Calibration involves determining the number of molecules
that can be generated in one step from a given starting point.

For example, imagine a simpler machine that supports only the
following replacements:

  - H => HO
  - H => OH
  - O => HH

Given the replacements above and starting with HOH, the following
molecules could be generated:

  - HOOH (via H => HO on the first H).
  - HOHO (via H => HO on the second H).
  - OHOH (via H => OH on the first H).
  - HOOH (via H => OH on the second H).
  - HHHH (via O => HH).

So, in the example above, there are 4 distinct molecules (not five,
because HOOH appears twice) after one replacement from HOH. Santa's
favorite molecule, HOHOHO, can become 7 distinct molecules (over
nine replacements: six from H, and three from O).

The machine replaces without regard for the surrounding characters.
For example, given the string H2O, the transition H => OO would
result in OO2O.

Your puzzle input describes all of the possible replacements and,
at the bottom, the medicine molecule for which you need to calibrate
the machine. How many distinct molecules can be created after all the
different ways you can do one replacement on the medicine molecule?

Part 2
------
Now that the machine is calibrated, you're ready to begin molecule
fabrication.

Molecule fabrication always begins with just a single electron, e,
and applying replacements one at a time, just like the ones during
calibration.

For example, suppose you have the following replacements:

  - e => H
  - e => O
  - H => HO
  - H => OH
  - O => HH

If you'd like to make HOH, you start with e, and then make the following
replacements:

  - e => O to get O
  - O => HH to get HH
  - H => OH (on the second H) to get HOH

So, you could make HOH after 3 steps. Santa's favorite molecule, HOHOHO,
can be made in 6 steps.

How long will it take to make the medicine? Given the available replacements
and the medicine molecule in your puzzle input, what is the fewest number of
steps to go from e to the medicine molecule?
"""

from __future__ import print_function, unicode_literals
from random import shuffle
import os
import re
import sys

INFILE = 'inputs/input19.txt'
EXPR = r'(\S+) => (\S+)'


def main():
    formula = ''
    replacements = dict()

    with open(INFILE) as f:
        r = re.compile(EXPR)

        for line in f:
            if not len(line.strip()):
                break

            m = r.match(line.strip())
            original = m.group(1)
            replacement = m.group(2)

            if original not in replacements:
                replacements[original] = set()
            replacements[original].add(replacement)

        formula = f.readlines()[-1].strip()

    # Part 1
    # ------
    p1sol = set()
    for old, news in replacements.items():
        for new in news:
            for c in range(len(formula)):
                if formula[c:c + len(old)] == old:
                    left = formula[:c]
                    right = formula[c + len(old):]

                    # Bananas are good for you
                    banana = '{}{}{}'.format(left, new, right)
                    p1sol.add(banana)

    # Part 2
    # ------
    # Move from a dictionary to a flat list of all pairs
    r = list()
    for o, n in replacements.items():
        for n2 in n:
            r.append((o, n2))

    # Go through every possible replacement and try to use it; in case
    # we don't happen upon the minimum the first time, try 100 times and
    # use whatever the smallest step count is
    counts = list()
    for i in range(100):
        shuffle(r)
        steps = 0
        molecule = formula
        while 'e' != molecule:
            start = molecule
            for o, n in r:
                while n in molecule:
                    steps += molecule.count(n)
                    molecule = molecule.replace(n, o)

            # Did nothing replace? Shuffle the list around and
            # try again
            if start == molecule:
                shuffle(r)
                molecule = formula
                steps = 0

        counts.append(steps)

    msg = '[Python]  Puzzle 19-1: {}'
    print(msg.format(len(p1sol)))

    msg = '[Python]  Puzzle 19-2: {}'
    print(msg.format(min(counts)))


if __name__ == '__main__':
    main()
