package com.github.rnelson.adventofcode

import com.github.rnelson.adventofcode.days.Day01

fun main(args: Array<String>) {
    if (args.count() != 1) {
        println("usage: advent2018 <day number>")
        System.exit(1)
    }

    // Create the appropriate "Day" subclass
    // Pass args[0] to constructor
    // Call solveA(), solveB() and print the responses

    val day : Day01 = Day01()
    day.setup(args[0])
    day.solveA()
    day.solveB()
}