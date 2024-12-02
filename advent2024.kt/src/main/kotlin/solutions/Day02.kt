package net.libexec.adventofcode.solutions

import net.libexec.adventofcode.Day
import net.libexec.adventofcode.util.collections.isAscending
import net.libexec.adventofcode.util.collections.isDescending
import net.libexec.adventofcode.util.collections.splitIntegers
import kotlin.math.abs

class Day02(loadData: Boolean = true) : Day("02", loadData) {
    override fun solveA(): String {
        var safe = 0

        input.forEach {
            val values = it.splitIntegers(' ')
            var isSafe = true

            if (values.isAscending() || values.isDescending()) {
                for (i in 0..<values.size-1) {
                    val diff = abs(values[i] - values[i + 1])
                    if (diff != 1 && diff != 2 && diff != 3) {
                        isSafe = false
                    }
                }

                if (isSafe) safe++
            }
        }

        return safe.toString()
    }

    override fun solveB(): String {
        var safe = 0

        input.forEach {
            val values = it.splitIntegers(' ')

            if (values.isAscending() || values.isDescending()) {
                val candidates = getCandidatesB(values)
                var foundGood = false

                for (vi in candidates.size-1 downTo 0) {
                    val v = candidates[vi]
                    var failures = 0

                    for (i in 0..<v.size - 1) {
                        val diff = abs(v[i] - v[i + 1])
                        if (diff != 1 && diff != 2 && diff != 3) {
                            failures++
                        }
                    }

                    if (!foundGood && failures <= 2) {
                        foundGood = true
                        break
                    }
                }

                if (foundGood) safe++
            }
        }

        return safe.toString()
    }

    private fun getCandidatesB(v: List<Int>): List<List<Int>> {
        val candidates = ArrayList<List<Int>>()
        candidates.add(v)

        // Add copies with the first and
        candidates.add(v.slice(1..v.size-1))
        candidates.add(v.slice(0..v.size-2))

        for (i in 0..v.size-1) {
            candidates.add(v.slice(0..<i) + v.slice(i+1..v.size-1))
        }

        return candidates
    }
}