import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day09Test {
    private val input = arrayOf("0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45")

    @Test
    fun partA() {
        val actual = Day09().partA(input)
        assertEquals(114, actual)
    }

    @Test
    fun partB() {
        val actual = Day09().partB(input)
        assertEquals(0, actual)
    }
}