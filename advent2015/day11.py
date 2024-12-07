#!/usr/bin/env python
"""
http://adventofcode.com/day/11

Part 1
------
Santa's previous password expired, and he needs help choosing a new
one.

To help him remember his new password after the old one expires,
Santa has devised a method of coming up with a password based on
the previous one. Corporate policy dictates that passwords must be
exactly eight lowercase letters (for security reasons), so he finds
his new password by incrementing his old password string repeatedly
until it is valid.

Incrementing is just like counting with numbers: xx, xy, xz, ya,
yb, and so on. Increase the rightmost letter one step; if it was
z, it wraps around to a, and repeat with the next letter to the
left until one doesn't wrap around.

Unfortunately for Santa, a new Security-Elf recently started, and
he has imposed some additional password requirements:

  - Passwords must include one increasing straight of at least
     three letters, like abc, bcd, cde, and so on, up to xyz. They
     cannot skip letters; abd doesn't count.
  - Passwords may not contain the letters i, o, or l, as these
     letters can be mistaken for other characters and are therefore
     confusing.
  - Passwords must contain at least two different, non-overlapping
     pairs of letters, like aa, bb, or zz.

For example:

  - hijklmmn meets the first requirement (because it contains the
     straight hij) but fails the second requirement requirement
     (because it contains i and l).
  - abbceffg meets the third requirement (because it repeats bb
     and ff) but fails the first requirement.
  - abbcegjk fails the third requirement, because it only has one
     double letter (bb).
  - The next password after abcdefgh is abcdffaa.
  - The next password after ghijklmn is ghjaabcc, because you
     eventually skip all the passwords that start with ghi...,
     since i is not allowed.

Given Santa's current password (your puzzle input), what should
his next password be?

Part 2
------
Santa's password expired again. What's the next one?
"""

from __future__ import print_function, unicode_literals
import os
import re
import sys

INFILE = '../../aoc-inputs/2015/input11.txt'

MINLENGTH = 8
MAXLENGTH = MINLENGTH
VALIDCHARS = 'abcdefghijklmnopqrstuvwxyz'
VALID = re.compile('^[a-z]+$')
BAD = re.compile(r'[ilo]')
double = [ch * 2 for ch in VALIDCHARS]
DOUBLE = re.compile('|'.join(double))
straights = [VALIDCHARS[i:i + 3] for i in xrange(len(VALIDCHARS) - 2)]
STRAIGHTS = re.compile('|'.join(straights))


def check(password):
    valid = True

    valid = valid and len(password) >= MINLENGTH
    valid = valid and len(password) <= MAXLENGTH
    valid = valid and VALID.match(password)
    valid = valid and len(STRAIGHTS.findall(password)) == 1
    valid = valid and not BAD.match(password)
    valid = valid and len(DOUBLE.findall(password)) >= 2

    return valid


def next(password):
    chars = [c for c in password]

    # Temporarily reverse the string so we can go
    # left to right
    chars.reverse()

    # Increment characters
    for i, char in enumerate(chars):
        try:
            current = VALIDCHARS.index(char)
            chars[i] = VALIDCHARS[current + 1]
        except IndexError:
            chars[i] = VALIDCHARS[0]
        else:
            # If we were able to increment a letter, we
            # don't need to move on to the next column
            break

    # Reverse the string again to make it normal
    chars.reverse()
    return ''.join(chars)


def make(current_password):
    new_password = next(current_password)
    while not check(new_password):
        new_password = next(new_password)
    return new_password


def main():
    current_password = None
    with open(INFILE) as f:
        for line in f:
            current_password = line.strip()

    if current_password is not None:
        # Part 1
        new_password = make(current_password)
        msg = '[Python]  Puzzle 11-1: {}'
        print(msg.format(new_password))

        # Part 2
        new_password = make(new_password)
        msg = '[Python]  Puzzle 11-2: {}'
        print(msg.format(new_password))


if __name__ == '__main__':
    main()
