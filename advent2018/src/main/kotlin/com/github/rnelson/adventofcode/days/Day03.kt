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
        val maxX = lines.maxBy { it.startX + it.spanX }
        val maxY = lines.maxBy { it.startY + it.spanY }

        val width = maxX!!.startX + maxX.spanX + 10
        val height = maxY!!.startY + maxY.spanY + 10

        var matrix = arrayOf<Array<Int>>()
        for (i in 0..width) {
            var array = arrayOf<Int>()
            for (j in 0..height) {
                array += 0
            }
            matrix += array
        }

        for (line in lines) {
            val startX = line.startX + 1
            val startY = line.startY + 1
            val endX = startX + line.spanX
            val endY = startY + line.spanY

            for (i in startX..endX) {
                for (j in startY..endY) {
                    matrix[i][j]++
                }
            }
        }

        var count = 0
        for (i in 0..width) {
            for (j in 0..height) {
                if (matrix[i][j] > 1) {
                    count++
                }
            }
        }

        count = 0
        for (row in matrix) {
            count += matrix[row].asSequence().filter { it > 1 }.count()
        }

        return count.toString()
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

    // https://stackoverflow.com/a/28548648
//    class Matrix<T> private(width: Int, height: Int, arrayFactory: (Int) -> Array<T>) {
//
//        class object {
//            inline fun <reified T>invoke(width: Int, height: Int)
//                    = Matrix(width, height, { size -> arrayOfNulls<T>(size) })
//        }
//
//        val data: Array<Array<T>> = Array(width, { size -> arrayFactory(size) })
//    }
}