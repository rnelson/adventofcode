import kotlin.math.abs

class Day03 : Day(3) {
    override fun partA(input: Array<String>): Any {
        val data = parse(input)
        val symbols = data.first
        val numbers = data.second
        var result = 0

        numbers.forEach { n ->
            val neighbors = symbols.filter { s ->
                abs(s.location.first - n.startLocation.first) <= 1 &&
                        s.location.second >= n.startLocation.second - 1 &&
                        s.location.second <= n.endLocation.second + 1
            }.toTypedArray()

            if (neighbors.isNotEmpty()) { result += n.value }
        }

        return result
    }

    override fun partB(input: Array<String>): Any {
        val data = parse(input)
        val symbols = data.first
        val numbers = data.second
        var result = 0

        symbols.forEach { s ->
            val neighbors = numbers.filter { n ->
                s.symbol == '*' &&
                        abs(s.location.first - n.startLocation.first) <= 1 &&
                        s.location.second >= n.startLocation.second - 1 &&
                        s.location.second <= n.endLocation.second + 1
            }.toTypedArray()

            if (neighbors.size == 2) {
                val bits = mutableListOf<Int>()

                neighbors.forEach {
                    bits.add(it.value)
                }

                result += bits.reduce { acc, i -> acc * i}
            }
        }

        return result
    }

    private fun parse(input: Array<String>): Pair<Array<Symbol>, Array<Number>> {
        val symbols = mutableListOf<Symbol>()
        val numbers = mutableListOf<Number>()

        for (i in input.indices) {
            val line = input[i]
            val lineArray = line.toCharArray()

            var num = ""
            var numStart = -1

            for (j in line.indices) {
                val char = lineArray[j]

                if (char.isDigit()) {
                    num += char

                    if (numStart < 0) {
                        numStart = j
                    }

                    continue
                }

                // We hit a non-number but had started tracking one,
                // so now we can add it to the list
                if (numStart > -1) {
                    numbers.add(Number(num.toInt(), Pair(i, numStart), Pair(i, j - 1)))

                    num = ""
                    numStart = -1
                }

                if (char == '.') {
                    continue
                }

                symbols.add(Symbol(char, Pair(i, j)))
            }

            if (numStart > -1) {
                numbers.add(Number(num.toInt(), Pair(i, numStart), Pair(i, line.length)))

                num = ""
                numStart = -1
            }
        }

        return Pair(symbols.toTypedArray(), numbers.toTypedArray())
    }

    data class Symbol(val symbol: Char, val location: Pair<Int, Int>)
    data class Number(val value: Int, val startLocation: Pair<Int, Int>, val endLocation: Pair<Int, Int>)
}