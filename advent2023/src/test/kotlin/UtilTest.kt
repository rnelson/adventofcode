import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*
import kotlin.math.exp

class UtilTest {

    @Test
    fun getIntegers() {
        val input = "42 69 0 420"
        val expectedValues = arrayOf<Long>(42, 69, 0, 420)
        val actual = input.getLongs()

        assertEquals(expectedValues.size, actual.size)
        expectedValues.forEach {
            assertTrue(actual.contains(it))
        }
    }

    @Test
    fun getLongs() {
        val input = "2804145277 283074539 358289757"
        val expectedValues = arrayOf<Long>(2804145277, 283074539, 358289757)
        val actual = input.getLongs()

        assertEquals(expectedValues.size, actual.size)
        expectedValues.forEach {
            assertTrue(actual.contains(it))
        }
    }
}