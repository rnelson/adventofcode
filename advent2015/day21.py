#!/usr/bin/env python3
"""
http://adventofcode.com/day/20

Part 1
------
Little Henry Case got a new video game for Christmas. It's
an RPG, and he's stuck on a boss. He needs to know what equipment
to buy at the shop. He hands you the controller.

In this game, the player (you) and the enemy (the boss) take
turns attacking. The player always goes first. Each attack
reduces the opponent's hit points by at least 1. The first
character at or below 0 hit points loses.

Damage dealt by an attacker each turn is equal to the attacker's
damage score minus the defender's armor score. An attacker always
does at least 1 damage. So, if the attacker has a damage score of
8, and the defender has an armor score of 3, the defender loses
5 hit points. If the defender had an armor score of 300, the
defender would still lose 1 hit point.

Your damage score and armor score both start at zero. They can be
increased by buying items in exchange for gold. You start with no
items and have as much gold as you need. Your total damage or
armor is equal to the sum of those stats from all of your items.
You have 100 hit points.

Here is what the item shop is selling:

    Weapons:    Cost  Damage  Armor
    Dagger        8     4       0
    Shortsword   10     5       0
    Warhammer    25     6       0
    Longsword    40     7       0
    Greataxe     74     8       0

    Armor:      Cost  Damage  Armor
    Leather      13     0       1
    Chainmail    31     0       2
    Splintmail   53     0       3
    Bandedmail   75     0       4
    Platemail   102     0       5

    Rings:      Cost  Damage  Armor
    Damage +1    25     1       0
    Damage +2    50     2       0
    Damage +3   100     3       0
    Defense +1   20     0       1
    Defense +2   40     0       2
    Defense +3   80     0       3

You must buy exactly one weapon; no dual-wielding. Armor is
optional, but you can't use more than one. You can buy 0-2 rings
(at most one for each hand). You must use any items you buy.
The shop only has one of each item, so you can't buy, for example,
two rings of Damage +3.

For example, suppose you have 8 hit points, 5 damage, and 5 armor,
and that the boss has 12 hit points, 7 damage, and 2 armor:

  - The player deals 5-2 = 3 damage; the boss goes down to 9 hit points.
  - The boss deals 7-5 = 2 damage; the player goes down to 6 hit points.
  - The player deals 5-2 = 3 damage; the boss goes down to 6 hit points.
  - The boss deals 7-5 = 2 damage; the player goes down to 4 hit points.
  - The player deals 5-2 = 3 damage; the boss goes down to 3 hit points.
  - The boss deals 7-5 = 2 damage; the player goes down to 2 hit points.
  - The player deals 5-2 = 3 damage; the boss goes down to 0 hit points.

In this scenario, the player wins! (Barely.)

You have 100 hit points. The boss's actual stats are in your puzzle
input. What is the least amount of gold you can spend and still
win the fight?

Part 2
------
Turns out the shopkeeper is working with the boss, and can persuade you to
buy whatever items he wants. The other rules still apply, and he still
only has one of each item.

What is the most amount of gold you can spend and still lose the fight?
"""

from __future__ import print_function, unicode_literals
from collections import namedtuple
from itertools import combinations
import os
import re
import sys

INFILE = 'inputs/input21.txt'
VERBOSE = True

Item = namedtuple('Item', 'type, name, cost, damage, armor')

class Character:
    def __init__(self, name, hp, damage, armor, items):
        self.name = name
        self.hp = hp
        self.damage = damage
        self.armor = armor
        self.items = items


def build_shop():
    w = list()
    a = list()
    r = list()

    w.append(Item('weapon', 'Dagger', 8, 4, 0))
    w.append(Item('weapon', 'Shortsword', 10, 5, 0))
    w.append(Item('weapon', 'Warhammer', 25, 6, 0))
    w.append(Item('weapon', 'Longsword', 40, 7, 0))
    w.append(Item('weapon', 'Greataxe', 74, 8, 0))

    a.append(Item('armor', 'None', 0, 0, 0))
    a.append(Item('armor', 'Leather', 13, 0, 1))
    a.append(Item('armor', 'Chainmail', 31, 0, 2))
    a.append(Item('armor', 'Splintmail', 53, 0, 3))
    a.append(Item('armor', 'Bandedmail', 75, 0, 4))
    a.append(Item('armor', 'Platemail', 102, 0, 5))

    r.append(Item('ring', 'None', 0, 0, 0))
    r.append(Item('ring', 'None', 0, 0, 0))
    r.append(Item('ring', 'Damage +1', 25, 1, 0))
    r.append(Item('ring', 'Damage +2', 50, 2, 0))
    r.append(Item('ring', 'Damage +3', 100, 3, 0))
    r.append(Item('ring', 'Defense +1', 20, 0, 1))
    r.append(Item('ring', 'Defense +2', 40, 0, 2))
    r.append(Item('ring', 'Defense +3', 80, 0, 3))

    return (w, a, r)


def build_character(player, opponent):
    # Default to the player's stats
    c = Character(player.name, player.hp, player.damage, player.armor, player.items)
    armor = c.armor
    damage = c.damage

    # Compute the opponent's armor (which lessen's player's damage)
    oarmor = opponent.armor
    for item in opponent.items:
        oarmor += item.armor

    # Remove opponent's armor from player's damage
    damage -= oarmor

    # Add damage/armor from player's items
    for item in player.items:
        damage += item.damage
        armor += item.armor

    # Minimum damage is 1
    damage = max(1, damage)

    # Update properties
    c.damage = damage
    c.armor = armor

    return c



def battle(player, boss):
    p1 = build_character(player, boss)
    p2 = build_character(boss, player)

    while True:
        # Attack
        p2.hp -= p1.damage

        if VERBOSE:
            print('> {} hits {} for {} damage, down to {} HP'.format(p1.name, p2.name, p1.damage, p2.hp))

            if p2.hp <= 0:
                print('> {} is slain'.format(p2.name))

        # Did someone die?
        if p2.hp <= 0:
            break

        # If not, swap the players and go again
        p1, p2 = p2, p1

    return (p1, p2)


def main():
    boss, player = None, None

    with open(INFILE) as f:
        lines = [line.strip() for line in f.readlines()]
        hp = int(lines[0].split()[-1])
        damage = int(lines[1].split()[-1])
        armor = int(lines[2].split()[-1])
        boss = Character('Boss', hp, damage, armor, set())

    player = Character('Player', 100, 0, 0, set())
    weapons, armors, rings = build_shop()

    # Part 1
    wins = list()
    for weapon in weapons:
        for armor in armors:
            for ring1, ring2 in combinations(rings, 2):
                # Purchase items
                player.items.clear()
                player.items.add(weapon)
                player.items.add(armor)
                player.items.add(ring1)
                player.items.add(ring2)

                if VERBOSE:
                    for p in [player, boss]:
                        print('Player "{}": HP={}, Damage={}, Armor={}'.format(p.name, p.hp, p.damage, p.armor))
                    print('For {} gold, player bought:'.format(sum([i.cost for i in player.items])))
                    for i in player.items:
                        print('  - {}, {} (d:{}, a:{}, c:{})'.format(i.type, i.name, i.damage, i.armor, i.cost))

                p1, p2 = battle(player, boss)
                if p1.name == 'Player':
                    cost = sum([i.cost for i in player.items])
                    wins.append(cost)

                if VERBOSE:
                    print('Winner: {}'.format(p1.name))
                    print('HP: {}: {}, {}: {}'.format(p1.name, p1.hp, p2.name, p2.hp))
                    print()

    # Part 2
    loses = list()
    for weapon in weapons:
        for armor in armors:
            for ring1, ring2 in combinations(rings, 2):
                # Purchase items
                player.items.clear()
                player.items.add(weapon)
                player.items.add(armor)
                player.items.add(ring1)
                player.items.add(ring2)

                p1, p2 = battle(player, boss)
                if p1.name == 'Boss':
                    cost = sum([i.cost for i in player.items])
                    loses.append(cost)

    msg = '[Python]  Puzzle 20-1: {}'
    print(msg.format(min(wins)))

    msg = '[Python]  Puzzle 20-2: {}'
    print(msg.format(max(loses)))


if __name__ == '__main__':
    main()
