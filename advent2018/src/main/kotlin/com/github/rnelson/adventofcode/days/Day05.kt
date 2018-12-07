package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day

class Day05: Day() {
    init {
        super.setup("05")
    }

    override fun solveA(): String {
        var newInput = input[0]

        var letterFound = true
        while (letterFound) {
            letterFound = false

            for (c in 'a'..'z') {
                val oneMix = "${c.toUpperCase()}$c"
                val twoMix = "$c${c.toUpperCase()}"

                var found = true
                while (found) {
                    val fOneMix = newInput.indexOf(oneMix)
                    val fTwoMix = newInput.indexOf(twoMix)

                    if (fOneMix != -1) {
                        newInput = newInput.replace(oneMix, "")
                        letterFound = true
                    }
                    else if (fTwoMix != -1) {
                        newInput = newInput.replace(twoMix, "")
                        letterFound = true
                    }
                    else {
                        found = false
                    }
                }
            }
        }

        return newInput.length.toString()
    }

    override fun solveB(): String {
        return ""
    }
}