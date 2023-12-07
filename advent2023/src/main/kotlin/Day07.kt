class Day07 : Day(7) {
    override fun partA(input: Array<String>): Any {
        return solve(input)
    }

    override fun partB(input: Array<String>): Any {
        return 0
    }

    private fun solve(input: Array<String>): Int {
        val games = parse(input)
        var result = 0

        val sorted = games.sortedWith(HandComparator)

        for (i in sorted.indices) {
            result += ((i + 1) * sorted[i].bid)
        }

        return result
    }
    
    private fun parse(input: Array<String>): List<Hand> {
        val result = mutableListOf<Hand>()
        
        input.forEach { line ->
            val bits = line.uppercase().trim().split(" ")
            val hand = bits[0].trim()
            val bid = bits[1].trim().toInt()
            val type = findType(hand)

            result.add(Hand(hand, type, bid))
        }
        
        return result
    }

    private val HandComparator = Comparator<Hand> { hand1, hand2 ->
        if (hand1.type.points > hand2.type.points) return@Comparator 1
        if (hand2.type.points > hand1.type.points) return@Comparator -1

        val h1Array = hand1.cards.toCharArray()
        val h2Array = hand2.cards.toCharArray()

        for (i in h1Array.indices) {
            val c1 = getCardScore(h1Array[i])
            val c2 = getCardScore(h2Array[i])

            if (c1 > c2) return@Comparator 1
            if (c2 > c1) return@Comparator -1
        }

        return@Comparator 0
    }

    private fun findType(hand: String): HandType {
        val uniques = hand.toCharArray().distinct()

        // Five of a kind (AAAAA)
        if (uniques.size == 1) return HandType.FIVE_OF_A_KIND

        // Four of a kind (AA8AA)
        // Full house (23332)
        if (uniques.size == 2) {
            uniques.forEach {
                if (hand.filter { c -> c == it }.length == 4)
                    return HandType.FOUR_OF_A_KIND
            }

            return HandType.FULL_HOUSE
        }

        // Three of a kind (TTT98)
        // Two pair (23432)
        if (uniques.size == 3) {
            uniques.forEach {
                if (hand.filter { c -> c == it }.length == 3)
                    return HandType.THREE_OF_A_KIND
            }

            return HandType.TWO_PAIR
        }

        // One pair (A23A4)
        if (uniques.size == 4) return HandType.ONE_PAIR

        // High card (23456)
        if (uniques.size == 5) return HandType.HIGH_CARD

        throw Error("unable to determine hand type for '$hand'")
    }

    private fun getCardScore(card: Char): Int {
        if (card.isDigit())
            return card.digitToInt()

        return when (card.uppercase().trim()) {
            "T" -> 10
            "J" -> 11
            "Q" -> 12
            "K" -> 13
            "A" -> 14
            else -> throw Error("unknown card type '$card'")
        }
    }

    enum class HandType(val points: Int) {
        FIVE_OF_A_KIND(7),
        FOUR_OF_A_KIND(6),
        FULL_HOUSE(5),
        THREE_OF_A_KIND(4),
        TWO_PAIR(3),
        ONE_PAIR(2),
        HIGH_CARD(1)
    }

    data class Hand(val cards: String, val type: HandType, val bid: Int)
}
