package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day02: Day() {
    init {
        super.setup("02")
    }

    override fun solveA(): String {
        var two = 0
        var three = 0

        input.forEach {
            var isTwo = false
            var isThree = false

            var counts = it.groupingBy { it2 -> it2 }.eachCount()
            counts.forEach { _, u ->
                if (u == 2) {
                    isTwo = true
                }
                if (u == 3) {
                    isThree = true
                }
            }

            if (isTwo) { two++ }
            if (isThree) { three++ }
        }

        val result = two * three
        return result.toString()
    }

    override fun solveB(): String {
        return ""
    }
}