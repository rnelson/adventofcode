#!/usr/bin/env python
from __future__ import print_function, unicode_literals
from os import chdir, getcwd, getenv
from subprocess import call

PYTHON = getenv('PYTHON', 'python')
FC = getenv('FC', 'f95')
DIR = getenv('DIR', './advent2015')

puzzles = ['01', '02', '03', '04']

for puzzle in puzzles:
    print('Puzzle {}'.format(puzzle))

    py_src = './puzzle{}.py'.format(puzzle)
    f95_src = './puzzle{}.f95'.format(puzzle)

    py_cmd = [PYTHON, py_src]
    f95_cmd_1 = [FC, f95_src]
    f95_cmd_2 = ['./a.out']

    chdir(DIR)
    call(py_cmd)
    chdir('..')
    chdir('{}.f'.format(DIR))
    call(f95_cmd_1)
    call(f95_cmd_2)
    chdir('..')
    print('')
