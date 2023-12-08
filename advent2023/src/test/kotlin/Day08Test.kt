import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day08Test {
    @Test
    fun partA() {
        val input = arrayOf("RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)")
        val actual = Day08().partA(input)
        assertEquals(2, actual)
    }

    @Test
    fun partB() {val input = arrayOf("LR",
            "",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)")
        val actual = Day08().partB(input)
        assertEquals(6L, actual)
    }
}