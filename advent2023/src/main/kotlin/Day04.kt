class Day04 : Day(4) {
    override fun partA(input: Array<String>): Any {
        val map = scoreA(input)
        return map.values.reduce { acc, i -> acc + i }
    }

    override fun partB(input: Array<String>): Any {
        return scoreB(input)
    }

    private fun scoreA(input: Array<String>, double: Boolean = true): Map<Int, Int> {
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
                    add = if (double) add * 2 else add + 1
                }
            }

            results[gameNumber] = total
        }

        return results;
    }

    private fun scoreB(input: Array<String>): Int {
        val scores = scoreA(input, false)
        val instances = mutableMapOf<Int, Int>()

        println(scores)

        // Initially, we have one copy of each
        for (i in 1..scores.size) {
            instances[i] = 1
        }

        // Now go through our scores and add copies of cards
        for (i in 1..scores.size) {
            val points = scores[i] ?: 0
            println("Card $i got $points points and ${instances[i]} turns [${scores[i]}]")

            if (points > 0) {
                val copies = instances[i] ?: 0
                println("Card $i got $points points and $copies turns [${scores[i]}]")

                for (turn in 1..copies) {
                    println("Turn $turn")
                    val start = i
                    val end = i + points

                    for (j in start..<end) {
                        val newCount = (instances[j] ?: 0) + (instances[i] ?: 0)
                        instances[j] = newCount

                        println("Added another card ${j + 1} (now $newCount copies)")
                    }
                }
            }

            println("")
        }

        return instances.values.reduce { acc, i -> acc + i }
    }
}