class Day04 : Day(4) {
    override fun partA(input: Array<String>): Any {
        val map = score(input)
        return map.values.reduce { acc, i -> acc + i }
    }

    override fun partB(input: Array<String>): Any {
        return 0
    }

    private fun score(input: Array<String>): Map<Int, Int> {
        val results = mutableMapOf<Int, Int>()

        input.forEach { line ->
            val split1 = line.split(":")
            val split2 = split1[1].split(" | ")

            val gameNumber = split1[0].trim().substring(5).trim().toInt()
            val myNumbers = split2[0].trim().replace("\\s+".toRegex(), " ").split(" ").map { it.trim().toInt() }
            val winningNumbers = split2[1].trim().replace("\\s+".toRegex(), " ").split(" ").map { it.trim().toInt() }

            var total = 0
            var add = 1

            winningNumbers.forEach { n ->
                if (myNumbers.contains(n)) {
                    total = add
                    add *= 2
                }
            }

            results[gameNumber] = total
        }

        return results;
    }
}