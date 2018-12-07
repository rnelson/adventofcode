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

//from string import ascii_lowercase
//
//new_input = INPUT
//
//letter_found = True
//while letter_found:
//letter_found = False
//for c in ascii_lowercase:
//one_mix = c.upper() + c
//two_mix = c + c.upper()
//
//found = True
//while found:
//f_one_mix = new_input.find(one_mix)
//f_two_mix = new_input.find(two_mix)
//
//if f_one_mix != -1:
//new_input = new_input.replace(one_mix, '')
//letter_found = True
//elif f_two_mix != -1:
//new_input = new_input.replace(two_mix, '')
//letter_found = True
//else:
//found = False
//
//print(len(new_input))