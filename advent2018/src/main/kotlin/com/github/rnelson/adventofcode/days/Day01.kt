package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day01: Day() {
    init {
        super.setup("01")
    }

    override fun solveA(): String {
        val sb = StringBuffer()
        input.forEach {
            sb.append(it)
            sb.append('\n')
        }

        return sb.toString()
    }

    override fun solveB(): String {
        return ""
    }
}