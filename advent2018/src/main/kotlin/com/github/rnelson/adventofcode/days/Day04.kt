package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class Day04: Day() {
    init {
        super.setup("04")
    }

    override fun solveA(): String {
        val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm")
        val sleeps = mutableMapOf<Int, MutableList<Pair<Int, Int>>>()
        val sleepsExpanded = mutableMapOf<Int, MutableList<Int>>()
        val sums = mutableMapOf<Int, Int>()

        var guard = -1
        var zonk = -1

        input.sorted().forEach {
            val timestamp = LocalDateTime.parse(it.substring(1, 17), formatter)
            val action = it.substring(19)

            when {
                action.startsWith("Guard") -> {
                    val idStart = action.indexOf('#') + 1
                    val idEnd = action.indexOf(' ', idStart)

                    guard = action.substring(idStart, idEnd).toInt()

                    if (!sleeps.containsKey(guard)) {
                        sleeps[guard] = mutableListOf()
                    }
                    if (!sleepsExpanded.containsKey(guard)) {
                        sleepsExpanded[guard] = mutableListOf()
                    }
                    if (!sums.containsKey(guard)) {
                        sums[guard] = 0
                    }
                }
                action.startsWith("falls") -> {
                    zonk = timestamp.minute
                }
                action.startsWith("wakes") -> {
                    val alarm = timestamp.minute - 1
                    val span = alarm - zonk

                    sleeps[guard]!!.add(Pair(zonk, alarm))
                    sums[guard] = sums[guard]!!.plus(span)

                    for (minute in zonk..alarm) {
                        sleepsExpanded[guard]!!.add(minute)
                    }
                }
                else -> {
                    println("ERROR: Unexpected action \"$action\"")
                }
            }
        }

        val sleepDeprived = sums.maxBy { it.value }!!.key
        val commonMinute = sleepsExpanded[sleepDeprived]!!.groupingBy { it }.eachCount().maxBy { it.value }!!.key

        return (sleepDeprived * commonMinute).toString()
    }

    override fun solveB(): String {val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm")
        val sleepsExpanded = mutableMapOf<Int, MutableList<Int>>()
        val sums = mutableMapOf<Int, Int>()

        var guard = -1
        var zonk = -1

        input.sorted().forEach {
            val timestamp = LocalDateTime.parse(it.substring(1, 17), formatter)
            val action = it.substring(19)

            when {
                action.startsWith("Guard") -> {
                    val idStart = action.indexOf('#') + 1
                    val idEnd = action.indexOf(' ', idStart)

                    guard = action.substring(idStart, idEnd).toInt()

                    if (!sleepsExpanded.containsKey(guard)) {
                        sleepsExpanded[guard] = mutableListOf()
                    }
                    if (!sums.containsKey(guard)) {
                        sums[guard] = 0
                    }
                }
                action.startsWith("falls") -> {
                    zonk = timestamp.minute
                }
                action.startsWith("wakes") -> {
                    val alarm = timestamp.minute - 1
                    val span = alarm - zonk

                    sums[guard] = sums[guard]!!.plus(span)

                    for (minute in zonk..alarm) {
                        sleepsExpanded[guard]!!.add(minute)
                    }
                }
                else -> {
                    println("ERROR: Unexpected action \"$action\"")
                }
            }
        }

        val allMaxValues: MutableMap<Int, Pair<Int, Int>> = mutableMapOf()
        sums.keys.forEach {
            // Guard 2179 starts a shift but never falls asleep. Repeatedly. What a good employee!
            if (sleepsExpanded[it] != null && sleepsExpanded[it]!!.count() > 0) {
                val mostCommon = sleepsExpanded[it]!!.groupingBy { it2 -> it2 }.eachCount().maxBy { it3 -> it3.value }!!
                allMaxValues[mostCommon.value] = Pair(it, mostCommon.key)
            }
        }

        val winningCount = allMaxValues.keys.sortedDescending()[0]
        val winningGuard = allMaxValues[winningCount]!!.second
        val winningMinute = allMaxValues[winningCount]!!.first

        return (winningGuard * winningMinute).toString()
    }
}