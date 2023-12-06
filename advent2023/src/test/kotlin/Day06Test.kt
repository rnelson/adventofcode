import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day06Test {
    @Test
    fun partA() {
        val input = arrayOf("Time:      7  15   30",
            "Distance:  9  40  200")
        val actual = Day06().partA(input)
        assertEquals(288, actual)
    }

    @Test
    fun partB() {
        val input = arrayOf("Time:      7  15   30",
            "Distance:  9  40  200")
        val actual = Day06().partB(input)
        assertEquals(71503, actual)
    }
}