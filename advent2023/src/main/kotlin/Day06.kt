class Day06 : Day(6) {
    override fun partA(input: Array<String>): Any {
        val data = parse(input)
        var product = 1

        for (i in data.first.indices) {
            product *= findWins(data.first[i], data.second[i]).size
        }

        return product
    }

    override fun partB(input: Array<String>): Any {
        val badData = parse(input)
        val data = Pair(badData.first.smoosh().toLong(), badData.second.smoosh().toLong())

        return findWins(data.first, data.second).size
    }

    private fun findWins(time: Long, recordDistance: Long): Array<Pair<Long, Long>> {
        val results = mutableListOf<Pair<Long, Long>>()

        for (held in 0..time) {
            val distance = held * (time - held)
            if (distance > recordDistance)
                results.add(Pair(held, distance))
        }

        return results.toTypedArray()
    }

    private fun parse(input: Array<String>): Pair<Array<Long>, Array<Long>> {
        val timesString = input[0].substring(5).trim()
        val distanceString = input[1].substring(9).trim()

        return Pair(
            timesString.getLongs().toList().toTypedArray(),
            distanceString.getLongs().toList().toTypedArray()
        )
    }
}
