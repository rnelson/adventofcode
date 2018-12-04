package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day03: Day() {
    var lines: MutableList<Line> = mutableListOf()

    init {
        super.setup("03")

        input.forEach {
            lines.add(Line.parse(it))
        }
    }

    override fun solveA(): String {
        return "<unknown>"
    }

    override fun solveB(): String {
        return "<unknown>"
    }

    class Line {
        var id: Int = -1
        var startX: Int = -1
        var startY: Int = -1
        var spanX: Int = -1
        var spanY: Int = -1

        override fun toString(): String {
            return "#$id @ $startX,$startY: ${spanX}x${spanY}"
        }

        companion object {
            fun parse(str: String): Line {
                val regex = """#(\d+) @ (\d+),(\d+): (\d+)x(\d+)""".toRegex()
                val result = regex.find(str)
                val (id, startX, startY, spanX, spanY) = result!!.destructured

                val line = Line()
                line.id = id.toInt()
                line.startX = startX.toInt()
                line.startY = startY.toInt()
                line.spanX = spanX.toInt()
                line.spanY = spanY.toInt()

                return line
            }
        }
    }
}