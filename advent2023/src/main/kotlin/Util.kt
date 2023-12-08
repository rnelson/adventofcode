import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

fun String.getIntegers(): Array<Int> {
    return this
        .trim()
        .replace("\\s+".toRegex(), " ")
        .split(" ")
        .map { it.trim().toInt() }
        .toTypedArray()
}

fun String.getLongs(): Array<Long> {
    return this
        .trim()
        .replace("\\s+".toRegex(), " ")
        .split(" ")
        .map { it.trim().toLong() }
        .toTypedArray()
}

fun Array<Long>.smoosh(): String {
    var result = ""
    this.forEach { result += it.toString() }
    return result
}

fun <T>time(block: () -> T) {
    val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss")
    val start = LocalDateTime.now().format(formatter)
    val startMS = System.currentTimeMillis()

    try {
        block.invoke()
    }
    finally {
        val end = LocalDateTime.now().format(formatter)
        val endMS = System.currentTimeMillis()
        val duration = endMS - startMS

        print("Took $duration ms")
        if (duration > 1000) {
            print(" (started $start, finished $end)")
        }
        println()
    }
}

fun Long.lcm(other: Long): Long {
    val larger = if (this > other) this else other
    val maxLcm = this * other
    var lcm = larger

    while (lcm <= maxLcm) {
        if (lcm % this == 0L && lcm % other == 0L) {
            return lcm
        }

        lcm += larger
    }

    return maxLcm
}

fun Array<Long>.lcm(): Long {
    var result = this[0]
    for (i in 1..<this.size) {
        result = result.lcm(this[i])
    }

    return result
}