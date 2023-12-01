import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class Day01Test {
    @Test
    fun partA() {
        val input = arrayOf("1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet")
        val actual = Day01().partA(input)
        assertEquals(actual, 142)
    }

    @Test
    fun partB() {
        val input = arrayOf("two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen")
        val actual = Day01().partB(input)
        assertEquals(actual, 281)
    }
}