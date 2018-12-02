package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day02: Day() {
    init {
        super.setup("02")
    }

    override fun solveA(): String {
        var two = 0
        var three = 0

        input.forEach { i ->
            if (i.groupingBy { it }.eachCount().filterValues { v -> v == 2 }.count() > 0) { two++ }
            if (i.groupingBy { it }.eachCount().filterValues { v -> v == 3 }.count() > 0) { three++ }
        }

        val result = two * three
        return result.toString()
    }

    override fun solveB(): String {
        return ""
    }
}