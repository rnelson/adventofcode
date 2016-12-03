#!/usr/bin/env python3
"""
http://adventofcode.com/day/22

Part 1
------
Little Henry Case decides that defeating bosses with swords and stuff is
boring. Now he's playing the game with a wizard. Of course, he gets stuck
on another boss and needs your help again.

In this version, combat still proceeds with the player and the boss taking
alternating turns. The player still goes first. Now, however, you don't
get any equipment; instead, you must choose one of your spells to cast.
The first character at or below 0 hit points loses.

Since you're a wizard, you don't get to wear armor, and you can't attack
normally. However, since you do magic damage, your opponent's armor is
ignored, and so the boss effectively has zero armor as well. As before,
if armor (from a spell, in this case) would reduce damage below 1, it
    becomes 1 instead - that is, the boss' attacks always deal at least
    1 damage.

On each of your turns, you must select one of your spells to cast. If you
cannot afford to cast any spell, you lose. Spells cost mana; you start with
500 mana, but have no maximum limit. You must have enough mana to cast a
spell, and its cost is immediately deducted when you cast it. Your spells
are Magic Missile, Drain, Shield, Poison, and Recharge.

  - Magic Missile costs 53 mana. It instantly does 4 damage.
  - Drain costs 73 mana. It instantly does 2 damage and heals you for 2
     hit points.
  - Shield costs 113 mana. It starts an effect that lasts for 6 turns. While
     it is active, your armor is increased by 7.
  - Poison costs 173 mana. It starts an effect that lasts for 6 turns. At
     the start of each turn while it is active, it deals the boss 3 damage.
  - Recharge costs 229 mana. It starts an effect that lasts for 5 turns. At
     the start of each turn while it is active, it gives you 101 new mana.

Effects all work the same way. Effects apply at the start of both the
player's turns and the boss' turns. Effects are created with a timer (the
number of turns they last); at the start of each turn, after they apply
any effect they have, their timer is decreased by one. If this decreases
the timer to zero, the effect ends. You cannot cast a spell that would
start an effect which is already active. However, effects can be started
on the same turn they end.

For example, suppose the player has 10 hit points and 250 mana, and that
the boss has 13 hit points and 8 damage:

    -- Player turn --
    - Player has 10 hit points, 0 armor, 250 mana
    - Boss has 13 hit points
    Player casts Poison.

    -- Boss turn --
    - Player has 10 hit points, 0 armor, 77 mana
    - Boss has 13 hit points
    Poison deals 3 damage; its timer is now 5.
    Boss attacks for 8 damage.

    -- Player turn --
    - Player has 2 hit points, 0 armor, 77 mana
    - Boss has 10 hit points
    Poison deals 3 damage; its timer is now 4.
    Player casts Magic Missile, dealing 4 damage.

    -- Boss turn --
    - Player has 2 hit points, 0 armor, 24 mana
    - Boss has 3 hit points
    Poison deals 3 damage. This kills the boss, and the player wins.

Now, suppose the same initial conditions, except that the boss has 14 hit
points instead:

    -- Player turn --
    - Player has 10 hit points, 0 armor, 250 mana
    - Boss has 14 hit points
    Player casts Recharge.

    -- Boss turn --
    - Player has 10 hit points, 0 armor, 21 mana
    - Boss has 14 hit points
    Recharge provides 101 mana; its timer is now 4.
    Boss attacks for 8 damage!

    -- Player turn --
    - Player has 2 hit points, 0 armor, 122 mana
    - Boss has 14 hit points
    Recharge provides 101 mana; its timer is now 3.
    Player casts Shield, increasing armor by 7.

    -- Boss turn --
    - Player has 2 hit points, 7 armor, 110 mana
    - Boss has 14 hit points
    Shield's timer is now 5.
    Recharge provides 101 mana; its timer is now 2.
    Boss attacks for 8 - 7 = 1 damage!

    -- Player turn --
    - Player has 1 hit point, 7 armor, 211 mana
    - Boss has 14 hit points
    Shield's timer is now 4.
    Recharge provides 101 mana; its timer is now 1.
    Player casts Drain, dealing 2 damage, and healing 2 hit points.

    -- Boss turn --
    - Player has 3 hit points, 7 armor, 239 mana
    - Boss has 12 hit points
    Shield's timer is now 3.
    Recharge provides 101 mana; its timer is now 0.
    Recharge wears off.
    Boss attacks for 8 - 7 = 1 damage!

    -- Player turn --
    - Player has 2 hit points, 7 armor, 340 mana
    - Boss has 12 hit points
    Shield's timer is now 2.
    Player casts Poison.

    -- Boss turn --
    - Player has 2 hit points, 7 armor, 167 mana
    - Boss has 12 hit points
    Shield's timer is now 1.
    Poison deals 3 damage; its timer is now 5.
    Boss attacks for 8 - 7 = 1 damage!

    -- Player turn --
    - Player has 1 hit point, 7 armor, 167 mana
    - Boss has 9 hit points
    Shield's timer is now 0.
    Shield wears off, decreasing armor by 7.
    Poison deals 3 damage; its timer is now 4.
    Player casts Magic Missile, dealing 4 damage.

    -- Boss turn --
    - Player has 1 hit point, 0 armor, 114 mana
    - Boss has 2 hit points
    Poison deals 3 damage. This kills the boss, and the player wins.

You start with 50 hit points and 500 mana points. The boss's actual stats
are in your puzzle input. What is the least amount of mana you can spend and
still win the fight? (Do not include mana recharge effects as "spending"
negative mana.)

Part 2
------
"""

from __future__ import print_function, unicode_literals
from collections import namedtuple
from itertools import combinations
import os
import re
import sys

INFILE = 'inputs/input22.txt'
VERBOSE = True

Item = namedtuple('Item', 'type, name, cost, damage, armor')


class Spell:
    def __init__(self, name, cost=0, damage=0, heal=0, armor=0, mana=0, turns=1):
        self.name = name
        self.cost = cost
        self.damage = damage
        self.heal = heal
        self.armor = armor
        self.mana = mana
        self.turns = turns

    def use(self, opponent):
        if turns > 0:
            turns -= 1

            # XXX: do this
            pass

        if turns > 0:
            return (True, o)
        else:
            return (False, o)


class Character:
    def __init__(self, name, hp, damage, armor, items=None, spells=None):
        self.name = name
        self.hp = hp
        self.damage = damage
        self.armor = armor
        self.items = items
        self.spells = spells

        if self.items == None:
            self.spells = set()
        if self.spells == None:
            self.spells = set()


    @staticmethod
    def build(player, opponent):
        c = Character(player.name, player.hp, player.damage,
                      player.armor, player.items, player.spells)

        # Factor in opponent's armor
        oarmor = opponent.armor
        for item in opponent.items:
            oarmor += item.armor
        for item in opponent.spells:
            oarmor += item.armor
        c.damage -= oarmor

        # Add damage and armor from items and spells
        for item in c.items:
            c.damage += item.damage
            c.armor += item.armor
        for item in c.spells:
            c.damage += item.damage
            c.armor += item.armor

        # Minimum damage is 1
        c.damage = max(1, camage)

        return c


    def attack(self, opponent):
        p = Character.build(self, opponent)
        o = Character.build(opponent, self)

        if VERBOSE:
            print('-- {} turn --'.format(p.name))
            print('{} has {} hit points, {} armor, '
                  '{} mana'.format(p.name, p.hp, p.armor, p.mana))
            print('{} has {} hit points'.format(o.name, o.hp))
        
        for item in p.spells:
            keep, o = item.use(o)

            if not keep:
                p.spells.remove(item)

        o.hp -= p.damage

        if VERBOSE:
            print('')


def battle(player, boss):
    p1 = build_character(player, boss)
    p2 = build_character(boss, player)

    while True:
        # Attack
        p2.hp -= p1.damage

        if VERBOSE:
            print('> {} hits {} for {} damage, down to '
                  '{} HP'.format(p1.name, p2.name, p1.damage, p2.hp))

            if p2.hp <= 0:
                print('> {} is slain'.format(p2.name))

        # Did someone die?
        if p2.hp <= 0:
            break

        # If not, swap the players and go again
        p1, p2 = p2, p1

    return (p1, p2)



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


def build_magic_list():
    m = list()

    m.append(Spell('Magic Missile', cost=53, damage=4))
    m.append(Spell('Drain', cost=73, damage=2, heal=2))
    m.append(Spell('Shield', cost=113, turns=6, armor=7))
    m.append(Spell('Poison', cost=173, turns=6, damage=3))
    m.append(Spell('Recharge', cost=229, turns=5, mana=101))

    return m


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
    magic = build_magic_list()

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
                        print('Player "{}": HP={}, Damage={}, '
                              'Armor={}'.format(p.name, p.hp, p.damage,
                                                p.armor))
                    print('For {} gold, player bought:'.format(
                        sum([i.cost for i in player.items])))
                    for i in player.items:
                        print('  - {}, {} (d:{}, a:{}, '
                              'c:{})'.format(i.type, i.name, i.damage,
                                             i.armor, i.cost))

                p1, p2 = battle(player, boss)
                if p1.name == 'Player':
                    cost = sum([i.cost for i in player.items])
                    wins.append(cost)

                if VERBOSE:
                    print('Winner: {}'.format(p1.name))
                    print('HP: {}: {}, {}: {}'.format(p1.name, p1.hp,
                                                      p2.name, p2.hp))
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
