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

//                    println("Guard $guard slept for $span minutes ($zonk to $alarm)")
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

    override fun solveB(): String {
        return ""
    }
}