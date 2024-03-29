package com.github.rnelson.adventofcode

import kotlin.reflect.full.createInstance
import kotlin.system.exitProcess

fun main(args: Array<String>) {
    if (args.count() != 1) {
        println("usage: advent2018 <day number>")
        exitProcess(1)
    }

    val className = "com.github.rnelson.adventofcode.days.Day${args[0]}"
    val day = Class.forName(className).kotlin.createInstance() as Day

    day.setup(args[0])

    println("Day ${args[0]}")
    println("------")
    println("Part A: ${day.solveA()}")
    println("Part B: ${day.solveB()}")
}