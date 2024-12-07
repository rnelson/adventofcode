#!/usr/bin/env python
"""
http://adventofcode.com/day/4

Part 1
------
Santa needs help mining some AdventCoins (very similar to bitcoins) to
use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with
at least five zeroes. The input to the MD5 hash is some secret key (your
puzzle input, given below) followed by a number in decimal. To mine
AdventCoins, you must find Santa the lowest positive number (no leading
zeroes: 1, 2, 3, ...) that produces such a hash.

For example:

  - If your secret key is abcdef, the answer is 609043, because the MD5 hash
       of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the
       lowest such number to do so.
  - If your secret key is pqrstuv, the lowest number it combines with to make
       an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash
       of pqrstuv1048970 looks like 000006136ef....


Part 2
------
Now find one that starts with six zeroes.
"""

from __future__ import print_function, unicode_literals
import hashlib
import os
import sys

INFILE = '../../aoc-inputs/2015/input04.txt'


def md5(input):
    m = hashlib.md5()
    m.update(input)
    return m.hexdigest()


def check(output, prefix):
    if output[:len(prefix)] == prefix:
        return True
    else:
        return False


def main():
    with open(INFILE) as f:
        input = f.read().strip()

        found = False
        i = -1

        while True:
            i += 1
            digest = md5(input + str(i))
            if check(digest, '00000'):
                msg = '[Python]  Puzzle 4-1: {} produces {}'
                print(msg.format(str(i), digest))
                break

        found = False
        i = -1
        while True:
            i += 1
            digest = md5(input + str(i))
            if check(digest, '000000'):
                msg = '[Python]  Puzzle 4-2: {} produces {}'
                print(msg.format(str(i), digest))
                break


if __name__ == '__main__':
    main()
