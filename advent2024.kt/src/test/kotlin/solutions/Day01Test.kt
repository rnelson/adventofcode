package solutions

import net.libexec.adventofcode.solutions.Day01
import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*

class Day01Test {
    private val dayReference = ::Day01
    private val inputExampleA = listOf(
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3"
    )
    private val inputExampleB = inputExampleA

    private val expectedExampleA = "11"
    private val expectedExampleB = "31"
    private val expectedSolutionA = "2378066"
    private val expectedSolutionB = "18934359"

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