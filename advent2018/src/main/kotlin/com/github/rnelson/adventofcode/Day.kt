package com.github.rnelson.adventofcode

import java.io.File

abstract class Day {
    protected var input: ArrayList<String> = ArrayList()

    fun setup(dayNumber: String) {
        input = ArrayList()
        File("../aoc-inputs/2018/day$dayNumber.txt").forEachLine { input.add(it) }
    }

    abstract fun solveA(): String
    abstract fun solveB(): String
}