#!/usr/bin/env python
"""
http://adventofcode.com/day/7

Part 1
------
This year, Santa brought little Bobby Tables a set of wires and
bitwise logic gates! Unfortunately, little Bobby is a little
under the recommended age range, and he needs help assembling
the circuit.

Each wire has an identifier (some lowercase letters) and can
carry a 16-bit signal (a number from 0 to 65535). A signal is
provided to each wire by a gate, another wire, or some specific
value. Each wire can only get a signal from one source, but can
provide its signal to multiple destinations. A gate provides no
signal until all of its inputs have a signal.

The included instructions booklet describe how to connect the
parts together: x AND y -> z means to connect wires x and y to
an AND gate, and then connect its output to wire z.

For example:

  - 123 -> x means that the signal 123 is provided to wire x.
  - x AND y -> z means that the bitwise AND of wire x and wire
       y is provided to wire z.
  - p LSHIFT 2 -> q means that the value from wire p is
       left-shifted by 2 and then provided to wire q.
  - NOT e -> f means that the bitwise complement of the value
       from wire e is provided to wire f.

Other possible gates include OR (bitwise OR) and RSHIFT
(right-shift). If, for some reason, you'd like to emulate the
circuit instead, almost all programming languages (for example,
C, JavaScript, or Python) provide operators for these gates.

For example, here is a simple circuit:

    123 -> x
    456 -> y
    x AND y -> d
    x OR y -> e
    x LSHIFT 2 -> f
    y RSHIFT 2 -> g
    NOT x -> h
    NOT y -> i

After it is run, these are the signals on the wires:

    d: 72
    e: 507
    f: 492
    g: 114
    h: 65412
    i: 65079
    x: 123
    y: 456

In little Bobby's kit's instructions booklet (provided as your
puzzle input), what signal is ultimately provided to wire a?


Part 2
------
Now, take the signal you got on wire a, override wire b to that
signal, and reset the other wires (including wire a). What new
signal is ultimately provided to wire a?
"""

from __future__ import print_function, unicode_literals
import os
import sys

INFILE = '../../aoc-inputs/2015/input07.txt'


class Wires:
    def reset(self):
        self.run += 1
        self.state = dict()
        self.expressions = dict()

    def __init__(self):
        self.run = 0
        self.answers = [0, 0]
        self.reset()

    def set_expression(self, var, value):
        self.expressions[var] = value

    def save(self, answer):
        self.answers[self.run - 1] = answer

    def get(self, run):
        return self.answers[run - 1]

    def calc(self, var):
        if self.run == 2 and var == 'b':
            return self.answers[0]

        try:
            return int(var)
        except ValueError:
            pass

        if var not in self.state:
            expr = self.expressions[var]

            if len(expr) == 1:
                result = self.calc(expr[0])
            else:
                op = expr[-2]
                if op == 'NOT':
                    result = ~self.calc(expr[1])
                elif op == 'AND':
                    result = self.calc(expr[0]) & self.calc(expr[2])
                elif op == 'OR':
                    result = self.calc(expr[0]) | self.calc(expr[2])
                elif op == 'LSHIFT':
                    result = self.calc(expr[0]) << self.calc(expr[2])
                elif op == 'RSHIFT':
                    result = self.calc(expr[0]) >> self.calc(expr[2])

            self.state[var] = result

        return self.state[var]


def main():
    w = Wires()

    with open(INFILE) as f:
        # Part 1
        for line in f:
            (expr, dest) = line.strip().split(' -> ')
            w.set_expression(dest, expr.strip().split(' '))

        w.save(w.calc('a'))
        msg = '[Python]  Puzzle 7-1: a={}'
        print(msg.format(w.get(1)))

        # Part 2
        w.reset()
        f.seek(0)

        for line in f:
            (expr, dest) = line.strip().split(' -> ')
            w.set_expression(dest, expr.strip().split(' '))

        w.save(w.calc('a'))
        msg = '[Python]  Puzzle 7-2: a={}'
        print(msg.format(w.get(2)))


if __name__ == '__main__':
    main()
