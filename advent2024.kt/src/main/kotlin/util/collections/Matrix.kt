package net.libexec.adventofcode.util.collections

@Suppress("unused")
class Matrix(dim: Int) {
    private var size: Int? = null
    private var data: IntArray? = null

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