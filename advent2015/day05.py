#!/usr/bin/env python
"""
http://adventofcode.com/day/5

Part 1
------
Santa needs help figuring out which strings in his text
file are naughty or nice.

A nice string is one with all of the following properties:

  - It contains at least three vowels (aeiou only), like
     aei, xazegov, or aeiouaeiouaeiou.
  - It contains at least one letter that appears twice in
     a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc,
     or dd).
  - It does not contain the strings ab, cd, pq, or xy, even
     if they are part of one of the other requirements.

For example:

  - ugknbfddgicrmopn is nice because it has at least three
     vowels (u...i...o...), a double letter (...dd...), and
     none of the disallowed substrings.
  - aaa is nice because it has at least three vowels and a
     double letter, even though the letters used by different
     rules overlap.
  - jchzalrnumimnmhp is naughty because it has no double letter.
  - haegwjzuvuyypxyu is naughty because it contains the string xy.
  - dvszwmarrgswjxmb is naughty because it contains only one vowel.

How many strings are nice?


Part 2
------
Realizing the error of his ways, Santa has switched to a better model
of determining whether a string is naughty or nice. None of the old
rules apply, as they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

  - It contains a pair of any two letters that appears at least twice in
     the string without overlapping, like xyxy (xy) or aabcdefgaa (aa),
     but not like aaa (aa, but it overlaps).
  - It contains at least one letter which repeats with exactly one
     letter between them, like xyx, abcdefeghi (efe), or even aaa.

For example:

  - qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice
     (qj) and a letter that repeats with exactly one letter between
     them (zxz).
  - xxyxx is nice because it has a pair that appears twice and a letter
     that repeats with one between, even though the letters used by each
     rule overlap.
  - uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat
     with a single letter between them.
  - ieodomkazucvgmuy is naughty because it has a repeating letter with
     one between (odo), but no pair that appears twice.

How many strings are nice under these new rules?
"""

from __future__ import print_function, unicode_literals
import os
import re
import sys

INFILE = '../../aoc-inputs/2015/input05.txt'

# Part 1 parameters
VOWELS = r'[aeiou]'
MINVOWELS = 3
DOUBLE = r'.*([a-zA-Z])\1.*'
MINDOUBLES = 1
BAD = r'(ab|cd|pq|xy)'
MAXBAD = 0

# Part 2 parameters
TWODOUBLE = r'.*([a-zA-Z][a-zA-Z]).*\1.*'
TWOREPEAT = r'.*([a-zA-Z])[a-zA-Z]\1.*'


def check(input):
    return False


def main():
    with open(INFILE) as f:
        # Part 1
        naughty, nice = [], []
        doubleExpr = re.compile(DOUBLE)

        for line in f:
            input = line.strip()
            is_naughty = False

            if len(re.findall(VOWELS, input)) < MINVOWELS:
                is_naughty = True
            if not doubleExpr.match(input):
                is_naughty = True
            if len(re.findall(BAD, input)) > MAXBAD:
                is_naughty = True

            if is_naughty:
                naughty.append(input)
            else:
                nice.append(input)

        msg = '[Python]  Puzzle 5-1: {} naughty, {} nice'
        print(msg.format(len(naughty), len(nice)))

        # Part 2
        f.seek(0)
        naughty, nice = [], []
        doubleExpr = re.compile(TWODOUBLE)
        repeatExpr = re.compile(TWOREPEAT)

        for line in f:
            input = line.strip()
            is_naughty = False

            if not doubleExpr.match(input):
                is_naughty = True
            if not repeatExpr.match(input):
                is_naughty = True

            if is_naughty:
                naughty.append(input)
            else:
                nice.append(input)

        msg = '[Python]  Puzzle 5-2: {} naughty, {} nice'
        print(msg.format(len(naughty), len(nice)))


if __name__ == '__main__':
    main()
