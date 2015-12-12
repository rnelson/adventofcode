#!/usr/bin/env python
"""
http://adventofcode.com/day/9

Part 1
------
Every year, Santa manages to deliver all of his presents in
a single night.

This year, however, he has some new locations to visit; his
elves have provided him the distances between every pair of
locations. He can start and end at any two (different) locations
he wants, but he must visit each location exactly once. What is
the shortest distance he can travel to achieve this?

For example, given the following distances:

  - London to Dublin = 464
  - London to Belfast = 518
  - Dublin to Belfast = 141

The possible routes are therefore:

  - Dublin -> London -> Belfast = 982
  - London -> Dublin -> Belfast = 605
  - London -> Belfast -> Dublin = 659
  - Dublin -> Belfast -> London = 659
  - Belfast -> Dublin -> London = 605
  - Belfast -> London -> Dublin = 982

The shortest of these is London -> Dublin -> Belfast = 605, and
so the answer is 605 in this example.

What is the distance of the shortest route?


Part 2
------
The next year, just to show off, Santa decides to take the route
with the longest distance instead.

He can still start and end at any two (different) locations he
wants, and he still must visit each location exactly once.

For example, given the distances above, the longest route would
be 982 via (for example) Dublin -> London -> Belfast.

What is the distance of the longest route?
"""

from __future__ import print_function, unicode_literals
import os
import re
import sys

INFILE = 'inputs/input09.txt'
EXPR = r'(\w+) to (\w+) = (\d+)'


def find_all_paths(graph, start, end, path=[]):
    """
    Shamelessly stolen from
    https://www.python.org/doc/essays/graphs/
    """
    path = path + [start]
    if start == end:
        return [path]
#    if not graph.has_key(start):
    if start not in graph:
        return []
    paths = []
    for node, dist in graph[start]:
        if node not in path:
            newpaths = find_all_paths(graph, node, end, path)
            for newpath in newpaths:
                paths.append(newpath)
    return paths


def find(graph):
    cities = graph.keys()
    min_path = None
    min_distance = sys.maxint
    max_path = None
    max_distance = -sys.maxint - 1

    for c1 in cities:
        for c2 in cities:
            if c1 != c2:
                paths = find_all_paths(graph, c1, c2)

                for path in paths:
                    if len(path) == len(cities):
                        new_path = [c1]
                        length = 0
                        for i in range(0, len(path) - 1):
                            cOne = path[i]
                            cTwo = path[i+1]

                            for d, l in graph[cOne]:
                                if d == cTwo:
                                    length += l
                                    new_path.append(d)

                        if length < min_distance:
                            min_distance = length
                            min_path = new_path
                        if length > max_distance:
                            max_distance = length
                            max_path = new_path

    return ((min_path, min_distance), (max_path, max_distance))


def main():
    graph = dict()

    with open(INFILE) as f:
        # Read the input
        for line in f:
            input = line.strip()

            # Pull out the values we care about
            m = re.search(EXPR, input)
            source = m.group(1)
            destination = m.group(2)
            length = int(m.group(3))

            # Add this edge
            if source in graph:
                graph[source].append([destination, length])
            else:
                graph[source] = [[destination, length]]
            if destination in graph:
                graph[destination].append([source, length])
            else:
                graph[destination] = [[source, length]]

        minimum, maximum = find(graph)

        # Part 1
        path, distance = minimum
        msg = '[Python]  Puzzle 9-1: {} = {}'
        print(msg.format(path, distance))

        # Part 2
        path, distance = maximum
        msg = '[Python]  Puzzle 9-2: {} = {}'
        print(msg.format(path, distance))


if __name__ == '__main__':
    main()
