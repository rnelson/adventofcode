class Day08 : Day(8) {
    override fun partA(input: Array<String>): Any {
        return solveA(input)
    }

    override fun partB(input: Array<String>): Any {
        return solveB(input)
    }

    private fun solveA(input: Array<String>): Int {
        val data = parse(input)
        val steps = data.steps.toCharArray()

        var currentNode = "AAA"
        var currentStep = -1
        var count = 0

        while (currentNode != "ZZZ") {
            count += 1
            currentStep += 1
            if (currentStep >= steps.size) {
                currentStep = 0
            }

            val direction = steps[currentStep]
            currentNode = when (direction) {
                'L' -> data.nodes[currentNode]!!.first
                'R' -> data.nodes[currentNode]!!.second
                else -> throw Error("unexpected step '$direction'")
            }
        }

        return count
    }

    private fun solveB(input: Array<String>): Long {
        val data = parse(input)

        val cycles = mutableListOf<Pair<String, Long>>()
        val starts = data.nodes.keys.filter { it.endsWith("A") }
        val ends = data.nodes.keys.filter { it.endsWith("Z") }

        starts.forEach { startNode ->
            val steps = data.steps.toCharArray()

            var currentNode = startNode
            var currentStep = -1
            var count = 0L

            while (!ends.contains(currentNode)) {
                count += 1
                currentStep += 1
                if (currentStep >= steps.size) {
                    currentStep = 0
                }

                val direction = steps[currentStep]
                currentNode = when (direction) {
                    'L' -> data.nodes[currentNode]!!.first
                    'R' -> data.nodes[currentNode]!!.second
                    else -> throw Error("unexpected step '$direction'")
                }
            }

            cycles.add(Pair(startNode, count))
        }

        print("Starts:")
        starts.forEach { print(" $it")}
        println("")

        print("Ends:")
        ends.forEach { print(" $it")}
        println("")

        cycles.forEach { println("f: ${it.first}, s: ${it.second}") }

        /*var lcm = cycles[0].second
        for (i in cycles.drop(1).indices) { lcm = lcm.lcm(cycles[i].second) }

        return lcm*/

        val counts = mutableListOf<Long>()
        cycles.forEach { counts.add(it.second) }
        return counts.toTypedArray().lcm()
    }

    private fun parse(input: Array<String>): Input {
        val nodes = mutableMapOf<String, Pair<String, String>>()
        val steps = input[0]
        val regex = """(\w+) = \((\w+), (\w+)\)""".toRegex()
        var first = ""; var last = ""

        for (i in input.indices) {
            if (i < 2) continue

            val (n, l, r) = regex.find(input[i])!!.destructured
            nodes[n] = Pair(l, r)

            if (i == 2) first = n
            last = n
        }

        return Input(steps, nodes, first, last)
    }

    data class Input(val steps: String,
        val nodes: Map<String, Pair<String, String>>,
        val first: String,
        val last: String)
}
