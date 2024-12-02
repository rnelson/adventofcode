@file:Suppress("unused")

package net.libexec.adventofcode.util.collections

fun <T> Sequence<T>.repeat() = sequence { while (true) yieldAll(this@repeat) }