class Day01 : Day(1) {
    override fun partA(input: Array<String>): Any {
        var result = 0

        input.forEach { line -> result += findNumber(line) }

        return result
    }

    override fun partB(input: Array<String>): Any {
        var result = 0

        input.forEach { line ->
            result += findNumber(line
                .replace("one", "one1one")
                .replace("two", "two2two")
                .replace("three", "three3three")
                .replace("four", "four4four")
                .replace("five", "five5five")
                .replace("six", "six6six")
                .replace("seven", "seven7seven")
                .replace("eight", "eight8eight")
                .replace("nine", "nine9nine")
            )
        }

        return result
    }

    private fun findNumber(input: String): Int {
        val digits = input.filter { it.isDigit() }
        return (digits.first().toString() + digits.last().toString()).toInt()
    }
}