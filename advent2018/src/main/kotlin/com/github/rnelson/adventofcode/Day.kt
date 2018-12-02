package com.github.rnelson.adventofcode

import java.nio.file.Files
import java.nio.file.Paths

abstract class Day {
    protected val input: ArrayList<String> = ArrayList()

    fun setup(dayNumber: String) {
        val stream = Files.newInputStream(Paths.get("input/day${dayNumber}.txt"))
        stream.buffered().reader().use { reader ->
            input.add(reader.readText())
        }
    }

    abstract fun solveA()
    abstract fun solveB()
}