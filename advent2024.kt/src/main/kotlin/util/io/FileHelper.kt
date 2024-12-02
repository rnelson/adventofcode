@file:Suppress("MemberVisibilityCanBePrivate", "unused")

package net.libexec.adventofcode.util.io

import java.io.IOException
import java.io.InputStream

object FileHelper {
    private fun readResource(filename: String): String {
        val classLoader = Thread.currentThread().contextClassLoader
        val inputStream: InputStream = classLoader.getResourceAsStream(filename)
            ?: throw IOException("unable to open resource: $filename")

        val text = inputStream.bufferedReader().use { it.readText() }
        inputStream.close()

        return text
    }

    fun asString(filename: String) = readResource(filename)
    fun asStrings(filename: String) = asString(filename).split("\n")
    fun asDoubles(filename: String) = asStrings(filename).map { it.toDouble() }
    fun asInts(filename: String) = asStrings(filename).map { it.toInt() }
    fun asUInts(filename: String) = asStrings(filename).map { it.toUInt() }
    fun asLongs(filename: String) = asStrings(filename).map { it.toLong() }
    fun asULongs(filename: String) = asStrings(filename).map { it.toULong() }
}