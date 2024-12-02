package net.libexec.adventofcode

import net.libexec.adventofcode.util.io.FileHelper

@Suppress("MemberVisibilityCanBePrivate")
abstract class Day(dayNumber: String, loadData: Boolean = true) {
    protected var dayNumber = "UNKNOWN"
    protected var filename = "solutions/$dayNumber.txt"
    protected var input: Collection<String> = ArrayList()

    init {
        this.dayNumber = dayNumber
        if (loadData) { setup() }
    }

    fun setup() {
        input = FileHelper.asStrings(filename)
    }

    fun setup(data: Iterable<String>) {
        val tempInput = ArrayList<String>()
        data.forEach { tempInput.add(it) }

        input = tempInput
    }

    abstract fun solveA(): String
    abstract fun solveB(): String
}