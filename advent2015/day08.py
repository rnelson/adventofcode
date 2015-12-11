#!/usr/bin/env python
"""
http://adventofcode.com/day/8

Part 1
------
Space on the sleigh is limited this year, and so Santa will be
bringing his list as a digital copy. He needs to know how much
space it will take up when stored.

It is common in many programming languages to provide a way to
escape special characters in strings. For example, C, JavaScript,
Perl, Python, and even PHP handle special characters in very
similar ways.

However, it is important to realize the difference between the
number of characters in the code representation of the string
literal and the number of characters in the in-memory string
itself.

(examples removed because the interpreter was complaining
 about the escaping - ha)

Disregarding the whitespace in the file, what is the number of
characters of code for string literals minus the number of characters
in memory for the values of the strings in total for the entire file?

For example, given the four strings above, the total number of
characters of string code (2 + 5 + 10 + 6 = 23) minus the total
number of characters in memory for string values (0 + 3 + 7 +
1 = 11) is 23 - 11 = 12.

Part 2
------
Now, let's go the other way. In addition to finding the number of
characters of code, you should now encode each code representation
as a new string and find the number of characters of the new encoded
representation, including the surrounding double quotes.

(examples removed because the interpreter was complaining
 about the escaping - ha)

Your task is to find the total number of characters to represent
the newly encoded strings minus the number of characters of code in
each original string literal. For example, for the strings above,
the total encoded length (6 + 9 + 16 + 11 = 42) minus the characters
in the original code representation (23, just like in the first
part of this puzzle) is 42 - 23 = 19.
"""

from __future__ import print_function
import os
import re
import sys

INFILE = 'inputs/input08.txt'


def main():
    total_length = 0
    unescaped_length = 0
    escaped_length = 0

    with open(INFILE) as f:
        # Part 1
        for line in f:
            input = line.strip()

            total_length += len(input)

            unescaped = input[1:-1].decode('string_escape')
            unescaped_length += len(unescaped)

            escaped = '"{}"'.format(re.escape(input))
            escaped_length += len(escaped)

        msg = '[Python]  Puzzle 8-1: {}'
        print(msg.format(total_length - unescaped_length))

        # Part 2
        msg = '[Python]  Puzzle 8-2: {}'
        print(msg.format(escaped_length - total_length))


if __name__ == '__main__':
    main()
