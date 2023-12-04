import kotlin.math.pow

class Day04 : Day(4) {
    override fun partA(input: Array<String>): Any {
        val map = scoreA(input)
        return map.values.reduce { acc, i -> acc + i }
    }

    override fun partB(input: Array<String>): Any {
        return scoreB(input)
    }

    private fun getMatchCount(input: String): Pair<Int, Int> {
        val split1 = input.split(":")
        val split2 = split1[1].split(" | ")

        val gameNumber = split1[0].trim().substring(5).trim().toInt()
        val myNumbers = split2[0].trim().replace("\\s+".toRegex(), " ").split(" ").map { it.trim().toInt() }
        val winningNumbers = split2[1].trim().replace("\\s+".toRegex(), " ").split(" ").map { it.trim().toInt() }.toTypedArray()

        return Pair(gameNumber, winningNumbers.intersect(myNumbers.toSet()).size)
    }

    private fun scoreA(input: Array<String>): Map<Int, Int> {
        val results = mutableMapOf<Int, Int>()

        input.forEach { line ->
            val matchDetails = getMatchCount(line)
            val gameNumber = matchDetails.first
            val matches = matchDetails.second

            results[gameNumber] = if (matches > 0) 2.toDouble().pow(matches - 1).toInt() else 0
        }

        return results
    }

    private fun scoreB(input: Array<String>): Int {
        val scores = scoreA(input)
        val instances = mutableMapOf<Int, Int>()

        for (i in 0..<scores.size) {
            instances[i] = 1
        }

        for (i in input.indices) {
            val line = input[i]
            val matches = getMatchCount(line).second

            for (j in i+1..<(i+1+matches)) {
                val c = instances[i] ?: 0
                instances[j] = (instances[j] ?: 0) + c
            }
        }

        return instances.values.reduce { acc, i -> acc + i }
    }
}
