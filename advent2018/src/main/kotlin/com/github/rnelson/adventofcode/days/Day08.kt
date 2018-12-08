package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day
import java.util.Stack

class Day08: Day() {
    private val numbers = mutableListOf<Int>()

    init {
        super.setup("08")
        input[0].split(' ').forEach { numbers.add(it.toInt()) }
    }

    override fun solveA(): String {
        val root = parse(0).first

        val stack = Stack<Node>()
        stack.push(root)

        var metaSum = 0

        while (stack.isNotEmpty()) {
            val node = stack.pop()
            metaSum += node.metadata.sum()

            node.children.forEach { stack.push(it) }
        }

        return metaSum.toString()
    }

    override fun solveB(): String {
        val root = parse(0).first

        val stack = Stack<Node>()
        stack.push(root)

        var value = 0

        while (stack.isNotEmpty()) {
            val node = stack.pop()

            if (node.children.isEmpty()) {
                value += node.metadata.sum()
            }
            else {
                val indexes = node.metadata.map { it - 1 }
                val validIndexes = indexes.filter { it < node.children.size }
                validIndexes.forEach {
                    stack.push(node.children[it])
                }
            }
        }

        return value.toString()
    }

    class Node {
        var parent: Node? = null
        var children = mutableListOf<Node>()
        var metadata = mutableListOf<Int>()
    }

    private fun parse(startPosition: Int): Pair<Node, Int> {
        var position = startPosition

        val node = Node()
        var childCount = numbers[position++]
        var metaCount = numbers[position++]

        // Recursively parse children
        while (childCount-- > 0) {
            val pair = parse(position)

            pair.first.parent = node
            node.children.add(pair.first)

            position = pair.second
        }

        // Add metadata
        while (metaCount-- > 0) {
            node.metadata.add(numbers[position++])
        }

        return Pair(node, position)
    }
}