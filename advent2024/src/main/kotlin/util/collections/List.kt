package net.libexec.adventofcode.util.collections

fun List<Int>.isAscending(): Boolean {
    val sorted = this.sorted()
    return this.isEqualTo(sorted)
}

fun List<Int>.isDescending(): Boolean {
    val sorted = this.sortedDescending()
    return this.isEqualTo(sorted)
}

fun List<Int>.isEqualTo(other: List<Int>): Boolean {
    if (this.size != other.size) return false

    for ((i, v) in this.withIndex()) {
        if (v != other[i]) return false
    }

    return true
}