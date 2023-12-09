class Day09 : Day(9) {
    override fun partA(input: Array<String>): Any {
        val seq = input.map { l ->
            generateSequence(l.getIntegers().toList()) {
                    s -> s.windowed(2) { (a, b) -> b - a }
            /*}.takeWhile {
                it.none { n -> n != 0 }
            }.toList()*/
                .take(1) }
        }
        //val sum = seq.sumOf { s -> s.sumOf { it.last() } }

        val a = seq.take(10)

        seq.forEach { println(it) }

        return 0
    }

    override fun partB(input: Array<String>): Any {
        return 0
    }

    private fun parse(input: Array<String>): Array<Long> {
        val result = mutableListOf<Long>()
        input.forEach { result.addAll(it.getLongs()) }
        return result.toTypedArray()
    }
}
