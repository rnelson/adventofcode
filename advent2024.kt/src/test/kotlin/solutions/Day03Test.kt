package solutions

import net.libexec.adventofcode.solutions.Day03
import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*

class Day03Test {
    private val dayReference = ::Day03
    private val inputExampleA = listOf(
        "",
        "",
        ""
    )
    private val inputExampleB = listOf(
        "",
        "",
        ""
    )

    private val expectedExampleA = ""
    private val expectedExampleB = ""
    private val expectedSolutionA = ""
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