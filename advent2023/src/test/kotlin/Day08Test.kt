import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day08Test {
    private val input = arrayOf("RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)")

    @Test
    fun partA() {
        val actual = Day08().partA(input)
        assertEquals(2, actual)
    }

    @Test
    fun partB() {
        val actual = Day08().partB(input)
        assertEquals(0, actual)
    }
}