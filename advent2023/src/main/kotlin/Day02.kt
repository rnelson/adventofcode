class Day02 : Day(2) {
    private val available = mapOf(BeadColor.RED to 12, BeadColor.GREEN to 13, BeadColor.BLUE to 14)

    override fun partA(input: Array<String>): Any {
        var result = 0

        input.forEach {
            val game = splitLine(it)
            var good = true

            game.second.forEach { pull ->
                available.keys.forEach { color ->
                    if (pull.containsKey(color) && (pull[color] ?: 0) > (available[color] ?: 0))
                        good = false
                }
            }

            if (good)
                result += game.first
        }

        return result
    }

    override fun partB(input: Array<String>): Any {
        var result = 0

        input.forEach {
            val game = splitLine(it)

            var mostR = 0
            var mostG = 0
            var mostB = 0

            game.second.forEach { pull ->
                if (pull.containsKey(BeadColor.RED) && (pull[BeadColor.RED] ?: 0) > mostR)
                    mostR = (pull[BeadColor.RED] ?: 0)
                if (pull.containsKey(BeadColor.GREEN) && (pull[BeadColor.GREEN] ?: 0) > mostG)
                    mostG = (pull[BeadColor.GREEN] ?: 0)
                if (pull.containsKey(BeadColor.BLUE) && (pull[BeadColor.BLUE] ?: 0) > mostB)
                    mostB = (pull[BeadColor.BLUE] ?: 0)
            }

            val product = mostR * mostG * mostB
            result += product
        }

        return result
    }

    private fun splitLine(input: String): Pair<Int, List<Map<BeadColor, Int>>> {
        val halves = input.split(":")
        val gameNumber = halves[0].split("Game ")[1].trim().toInt()

        val list = mutableListOf<Map<BeadColor, Int>>()

        halves[1].split(";").forEach { group ->
            val map = mutableMapOf<BeadColor, Int>()

            group.split(",").forEach { bead ->
                val bits = bead.trim().split(" ")
                val count = bits[0].trim().toInt()

                when (val color = bits[1].trim().lowercase()) {
                    "red" -> map[BeadColor.RED] = count
                    "green" -> map[BeadColor.GREEN] = count
                    "blue" -> map[BeadColor.BLUE] = count
                    else -> throw Error("unexpected color: $color")
                }
            }

            list.add(map)
        }

        return Pair<Int, List<Map<BeadColor, Int>>>(gameNumber, list)
    }

    enum class BeadColor {
        RED, GREEN, BLUE
    }

    data class Game(val gameNumber: Int, val counts: Map<BeadColor, Int>)
}