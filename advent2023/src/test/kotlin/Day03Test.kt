import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day03Test {
    @Test
    fun partA() {
        val input = arrayOf("467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...\$.*....",
                ".664.598..")
        val actual = Day03().partA(input)
        assertEquals(4361, actual)
    }

    @Test
    fun partB() {
        val input = arrayOf("467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...\$.*....",
            ".664.598..")
        val actual = Day03().partB(input)
        assertEquals(467835, actual)
    }
}