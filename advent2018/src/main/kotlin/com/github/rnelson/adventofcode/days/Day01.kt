package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day01: Day() {
    init {
        super.setup("01")
    }

    override fun solveA(): String {
        return input.sumOf { it.toInt() }.toString()
    }

    override fun solveB(): String {
        val set: MutableSet<Int> = mutableSetOf(0)
        var sum = 0

        input.map { it.toInt() }.asSequence().repeat().forEach {
            sum += it

            if (!set.add(sum)) {
                return sum.toString()
            }
        }

        return "<unknown>"
    }
}

fun <T> Sequence<T>.repeat() = sequence { while (true) yieldAll(this@repeat) }