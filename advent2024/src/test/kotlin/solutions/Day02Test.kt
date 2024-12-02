package solutions

import net.libexec.adventofcode.solutions.Day02
import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*

class Day02Test {
    private val dayReference = ::Day02
    private val inputExampleA = listOf(
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
    )
    private val inputExampleB = inputExampleA

    private val expectedExampleA = "2"
    private val expectedExampleB = "4"
    private val expectedSolutionA = "510"
    private val expectedSolutionB = ""

    private fun build() = dayReference(true)
    private fun buildExampleA() = dayReference(false).also { it.setup(inputExampleA) }
    private fun buildExampleB() = dayReference(false).also { it.setup(inputExampleB) }

    @Test
    fun exampleA() {
        val actual = buildExampleA().solveA()
        assertEquals(expectedExampleA, actual)
    }

    @Test
    fun exampleB() {
        val actual = buildExampleB().solveB()
        assertEquals(expectedExampleB, actual)
    }

    @Test
    fun problemA() {
        val actual = build().solveA()
        assertEquals(expectedSolutionA, actual)
    }

    @Test
    fun problemB() {
        val actual = build().solveB()
        assertEquals(expectedSolutionB, actual)
    }
}