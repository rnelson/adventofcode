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
        val m = Matrix(1000)
        lines.forEach {
            for (x in it.startX..(it.startX + it.spanX - 1)) {
                for (y in it.startY..(it.startY + it.spanY - 1)) {
                    m.inc(x, y)
                }
            }
        }

        return m.filter { it > 1 }.count().toString()
    }

    override fun solveB(): String {
        val m = Matrix(1000)
        lines.forEach {
            for (x in it.startX..(it.startX + it.spanX - 1)) {
                for (y in it.startY..(it.startY + it.spanY - 1)) {
                    m.inc(x, y)
                }
            }
        }

        lines.forEach {
            var solo = true
            for (x in it.startX..(it.startX + it.spanX - 1)) {
                for (y in it.startY..(it.startY + it.spanY - 1)) {
                    solo = solo && (m.get(x, y) == 1)
                }
            }

            if (solo) {
                return it.id.toString()
            }
        }

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

    class Matrix(dim: Int) {
        var size: Int? = null
        var data: IntArray? = null
        init {
            size = dim
            data = IntArray(dim * dim) { 0 }
        }

        fun get(x: Int, y: Int): Int {
            val location = xyToIndex(x, y)
            return data!![location]
        }

        fun set(x: Int, y: Int, value: Int) {
            val location = xyToIndex(x, y)
            data!![location] = value
        }

        fun inc(x: Int, y: Int) {
            val location = xyToIndex(x, y)
            data!![location]++
        }

        fun filter(predicate: (Int) -> Boolean): List<Int> {
            return data!!.filter(predicate)
        }

        private fun xyToIndex(x: Int, y: Int): Int {
            val yOffset = (x * size!!) + y //((y - 1) % size!!)
            return x + yOffset
        }

        private fun print() {
            for (row in 0..size!!) {
                println(data.toString().substring(row, size!!))
            }
        }
    }
}