import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day07Test {
    private val input = arrayOf("32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483")

    @Test
    fun partA() {
        val actual = Day07().partA(input)
        assertEquals(6440, actual)
    }

    @Test
    fun partB() {
        val actual = Day07().partB(input)
        assertEquals(0, actual)
    }
}