@file:Suppress("MemberVisibilityCanBePrivate")

import java.io.File
import java.nio.file.Paths

abstract class Day(private val day: Int) {
    protected val inputStringArray = readFile()

    protected fun readFile(): Array<String> {
        val prefix = if (day < 10) "0$day" else "$day"
        val filename = "$prefix.txt"

        val pwd = Paths.get("").toAbsolutePath().toString()
        val input = Paths.get(pwd, "/src/main/resources/$filename").toAbsolutePath().toString() // Yeah, this won't work

        val result = mutableListOf<String>()
        File(input).useLines { lines -> lines.forEach { result.add(it) }}

        return result.toTypedArray()
    }

    fun solve() {
        println("Day $day")
        println("\tPart 1: ${partA(inputStringArray)}")
        println("\tPart 2: ${partB(inputStringArray)}")
    }

    abstract fun partA(input: Array<String>) : Any

    abstract fun partB(input: Array<String>) : Any
}