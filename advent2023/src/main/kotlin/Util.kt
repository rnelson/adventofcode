fun String.getIntegers(): Array<Int> {
    return this
        .trim()
        .replace("\\s+".toRegex(), " ")
        .split(" ")
        .map { it.trim().toInt() }
        .toTypedArray()
}

fun String.getLongs() : Array<Long> {
    return this
        .trim()
        .replace("\\s+".toRegex(), " ")
        .split(" ")
        .map { it.trim().toLong() }
        .toTypedArray()
}