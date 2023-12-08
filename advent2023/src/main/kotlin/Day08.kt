class Day08 : Day(8) {
    override fun partA(input: Array<String>): Any {
        var result = 0
        time { result = solveA(input) }
        return result
    }

    override fun partB(input: Array<String>): Any {
        return 0
    }

    private fun solveA(input: Array<String>): Int {
        val data = parse(input)
        val steps = data.first.toCharArray()

        var currentNode = data.second[0]
        var currentStep = -1
        var count = 0

        while (currentNode.label != data.second[data.second.size - 1].label) {
            // Make sure we don't go past the end of the instructions
            currentStep++;
            if (currentStep >= steps.size) {
                currentStep = 0
            }

            // Increment
            val step = steps[currentStep]
            count += 1

            //print("Instructed to go $step from ${current.label}, headed to ")

            currentNode = when (step) {
                'L' -> findNode(currentNode.left, data)
                'R' -> findNode(currentNode.right, data)
                else -> throw Error("unexpected step '$step'")
            }

            //println("${current.label}.")
        }

        return count
    }

    private fun findNode(label: String, data: Pair<String, Array<Node>>): Node {
        return data.second.first { it.label == label }
    }

    private fun parse(input: Array<String>): Pair<String, Array<Node>> {
        val nodes = mutableListOf<Node>()
        val steps = input[0]
        val regex = """(\w+) = \((\w+), (\w+)\)""".toRegex()

        for (i in input.indices) {
            if (i < 2) continue

            val (n, l, r) = regex.find(input[i])!!.destructured
            nodes.add(Node(n, l, r))
        }

        return Pair(steps, nodes.toTypedArray())
    }

    data class Node(val label: String, val left: String, val right: String)
}
