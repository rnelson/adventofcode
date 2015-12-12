#!/usr/bin/env python
"""
http://adventofcode.com/day/12

Part 1
------
Santa's Accounting-Elves need help balancing the books after a
recent order. Unfortunately, their accounting software uses a
peculiar storage format. That's where you come in.

They have a JSON document which contains a variety of things:
arrays ([1,2,3]), objects ({"a":1, "b":2}), numbers, and strings.
Your first job is to simply find all of the numbers throughout
the document and add them together.

For example:

  - [1,2,3] and {"a":2,"b":4} both have a sum of 6.
  - [[[3]]] and {"a":{"b":4},"c":-1} both have a sum of 3.
  - {"a":[-1,1]} and [-1,{"a":1}] both have a sum of 0.
  - [] and {} both have a sum of 0.

You will not encounter any strings containing numbers.

What is the sum of all numbers in the document?

Part 2
------
Uh oh - the Accounting-Elves have realized that they double-counted
everything red.

Ignore any object (and all of its children) which has any property
with the value "red". Do this only for objects ({...}), not
arrays ([...]).

  - [1,2,3] still has a sum of 6.
  - [1,{"c":"red","b":2},3] now has a sum of 4, because the
     middle object is ignored.
  - {"d":"red","e":[1,2,3,4],"f":5} now has a sum of 0, because
     the entire structure is ignored.
  - [1,"red",5] has a sum of 6, because "red" in an array has
     no effect.
"""

from __future__ import print_function, unicode_literals
import json
import os
import re
import sys

INFILE = 'inputs/input12.txt'
P1REGEX = r'-?\d+'


def do_sum(o):
    """
    Everything is either a string (str/unicode), a number (int/long),
    an array (list), or an object (dict). Depending on what the passed
    in object is, return/compute the sum.
    """
    if isinstance(o, (str, unicode)):
        return 0
    elif isinstance(o, (int, long)):
        return o
    elif isinstance(o, list):
        return sum([do_sum(b) for b in o])
    else:
        if 'red' in o.values():
            return 0
        else:
            return do_sum(list(o.values()))


def main():
    total = 0
    no_red_total = 0

    with open(INFILE) as f:
        for line in f:
            # Part 1
            numbers = re.findall(P1REGEX, line)
            total += sum(map(int, numbers))

            # Part 2
            no_red_total += do_sum(json.loads(line))

    msg = '[Python]  Puzzle 12-1: {}'
    print(msg.format(total))

    # Part 2
    msg = '[Python]  Puzzle 12-2: {}'
    print(msg.format(no_red_total))


if __name__ == '__main__':
    main()
