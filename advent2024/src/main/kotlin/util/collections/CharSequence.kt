package net.libexec.adventofcode.util.collections

fun CharSequence.splitIntegers(c: Char): List<Int> {
    val regex = "[$c]+".toRegex()
    return this.splitIntegers(regex)
}

fun CharSequence.splitIntegers(r: Regex): List<Int> {
    val result = ArrayList<Int>()
    val bits = this.trim().split(r)

    bits.forEach {
        result.add(it.toInt())
    }

    return result
}