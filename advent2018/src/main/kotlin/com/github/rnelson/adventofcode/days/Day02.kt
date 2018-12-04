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
        input.mapIndexed { index, s ->
            val futureInputs = input.drop(index + 1)
            futureInputs.forEach { f ->
                val pairs = s.zip(f)
                val same = pairs.filter { p -> p.first == p.second }
                val count = same.count()

                if (count == s.length - 1) {
                    val sb = StringBuilder()
                    same.forEach {p ->
                        sb.append(p.first)
                    }
                    return sb.toString()
                }
            }
        }

        return "<unknown>"
    }
}