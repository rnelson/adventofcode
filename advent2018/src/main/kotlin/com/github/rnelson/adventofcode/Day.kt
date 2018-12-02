package com.github.rnelson.adventofcode

import java.io.File

abstract class Day {
    protected val input: ArrayList<String> = ArrayList()

    fun setup(dayNumber: String) {
        File("input/day$dayNumber.txt").forEachLine { input.add(it) }
    }

    abstract fun solveA(): String
    abstract fun solveB(): String
}