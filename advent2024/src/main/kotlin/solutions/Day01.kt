package net.libexec.adventofcode.solutions

import net.libexec.adventofcode.Day
import net.libexec.adventofcode.util.collections.splitIntegers
import kotlin.math.abs

class Day01(loadData: Boolean = true) : Day("01", loadData) {
    override fun solveA(): String {
        val values = parseInput()
        var distance = 0

        values.forEach {
            distance += abs(it.first - it.second)
        }

        return distance.toString()
    }

    override fun solveB(): String {
        val values = parseInput()
        var similarity = 0

        values.forEach { l ->
            similarity += l.first * values.count { r -> r.second == l.first }
        }

        return similarity.toString()
    }

    private fun parseInput(): List<Pair<Int, Int>> {
        val result = ArrayList<Pair<Int, Int>>()
        val left = ArrayList<Int>()
        val right = ArrayList<Int>()

        input.forEach {
            val bits = it.splitIntegers("\\s+".toRegex())
            left.add(bits[0])
            right.add(bits[1])
        }

        val sortedLeft = left.sorted()
        val sortedRight = right.sorted()

        for (i in sortedLeft.indices) {
            result.add(Pair(sortedLeft[i], sortedRight[i]))
        }

        return result
    }
}