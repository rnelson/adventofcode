#!/usr/bin/env python
"""
http://adventofcode.com/day/23

Part 1
------
Little Jane Marie just got her very first computer for Christmas
from some unknown benefactor. It comes with instructions and an
example program, but the computer itself seems to be malfunctioning.
She's curious what the program does, and would like you to help her
run it.

The manual explains that the computer supports two registers and
six instructions (truly, it goes on to remind the reader, a
state-of-the-art technology). The registers are named a and b, can
hold any non-negative integer, and begin with a value of 0. The
instructions are as follows:

    hlf r sets register r to half its current value, then continues with
       the next instruction.
    tpl r sets register r to triple its current value, then continues with
       the next instruction.
    inc r increments register r, adding 1 to it, then continues with the
       next instruction.
    jmp offset is a jump; it continues with the instruction offset away
       relative to itself.
    jie r, offset is like jmp, but only jumps if register r is even ("jump
       if even").
    jio r, offset is like jmp, but only jumps if register r is 1 ("jump if
       one", not odd).

All three jump instructions work with an offset relative to that
instruction. The offset is always written with a prefix + or - to
indicate the direction of the jump (forward or backward, respectively).
For example, jmp +1 would simply continue with the next instruction, while
jmp +0 would continuously jump back to itself forever.

The program exits when it tries to run an instruction beyond the ones
defined.

For example, this program sets a to 2, because the jio instruction causes
it to skip the tpl instruction:

    inc a
    jio a, +2
    tpl a
    inc a

What is the value in register b when the program in your puzzle input is
finished executing?


Part 2
------
The unknown benefactor is very thankful for releasi-- er, helping little Jane
Marie with her computer. Definitely not to distract you, what is the value in
register b after the program is finished executing if register a starts as 1
instead?
"""

from __future__ import print_function, unicode_literals
from collections import namedtuple
import os
import re
import sys

INFILE = '../../aoc-inputs/2015/input23.txt'
VERBOSE = False


Instruction = namedtuple('Instruction', 'instruction, register, offset')


class Computer:
    def __init__(self):
        self.instructions = list()
        self.sp = 0
        self.top = -1
        self.registers = dict()

        self.registers['a'] = 0
        self.registers['b'] = 0

    def set_register(self, register, value):
        self.registers[register] = value

    def add_instruction(self, instruction):
        self.instructions.append(instruction)
        self.top += 1

    def execute(self):
        while self.sp <= self.top:
            self.step()

    def step(self):
        if VERBOSE:
            print('top={} sp={}'.format(self.top, self.sp))

        inst = self.instructions[self.sp]
        i = inst.instruction
        r = inst.register
        o = inst.offset

        if inst.instruction == 'hlf':
            self.registers[r] = self.registers[r] / 2
            self.sp += 1
        elif inst.instruction == 'tpl':
            self.registers[r] = self.registers[r] * 3
            self.sp += 1
        elif inst.instruction == 'inc':
            self.registers[r] = self.registers[r] + 1
            self.sp += 1
        elif inst.instruction == 'jmp':
            self.sp += o
        elif inst.instruction == 'jie':
            if self.registers[r] % 2 == 0:
                self.sp += o
            else:
                self.sp += 1
        elif inst.instruction == 'jio':
            if self.registers[r] == 1:
                self.sp += o
            else:
                self.sp += 1


def main():
    c = Computer()
    c2 = Computer()

    with open(INFILE) as f:
        # Part 1
        for line in f:
            bits = line.strip().split()

            inst = None
            reg = None
            off = None

            inst = bits[0]

            if len(bits) == 2:
                if inst == 'jmp':
                    reg = ''
                    off = int(bits[1])
                else:
                    reg = bits[1]
                    off = 0
            elif len(bits) == 3:
                reg = bits[1][:-1]
                off = int(bits[2])
            else:
                print('ERROR: invalid instruction: "{}"'.format(line.strip()))

            i = Instruction(inst, reg, off)
            c.add_instruction(i)
            c2.add_instruction(i)

    # Part 1
    c.execute()
    msg = '[Python]  Puzzle 23-1: a={}, b={}'
    print(msg.format(c.registers['a'], c.registers['b']))

    # Part 2
    c2.set_register('a', 1)
    c2.execute()
    msg = '[Python]  Puzzle 23-2: a={}, b={}'
    print(msg.format(c2.registers['a'], c2.registers['b']))


if __name__ == '__main__':
    main()
