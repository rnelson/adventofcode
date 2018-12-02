package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day01: Day() {
    init {
        super.setup("01")
    }

    override fun solveA(): String {
        return input.map { it.toInt() }.sum().toString()
    }

    override fun solveB(): String {
        return ""
    }
}