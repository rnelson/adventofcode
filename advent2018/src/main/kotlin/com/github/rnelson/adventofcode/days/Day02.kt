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
            val map = i.groupingBy { it }.eachCount()
            if (map.containsValue(2)) { two++ }
            if (map.containsValue(3)) { three++ }
        }

        val result = two * three
        return result.toString()
    }

    override fun solveB(): String {
        return ""
    }
}