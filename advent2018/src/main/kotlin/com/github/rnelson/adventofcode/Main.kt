package com.github.rnelson.adventofcode

import kotlin.reflect.full.createInstance

fun main(args: Array<String>) {
    if (args.count() != 1) {
        println("usage: advent2018 <day number>")
        System.exit(1)
    }

    val className = "com.github.rnelson.adventofcode.days.Day${args[0]}"
    val day = Class.forName(className).kotlin.createInstance() as Day

    day.setup(args[0])
    println(day.solveA())
    println(day.solveB())
}