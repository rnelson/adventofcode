class Day05 : Day(5) {
    override fun partA(input: Array<String>): Any {
        return solveA(input)
    }

    override fun partB(input: Array<String>): Any {
        var result = -1L
        time { result = solveB(input) }
        return result
    }

    private fun solveA(input: Array<String>): Long {
        val data = parse(input)
        val map = mutableMapOf<Long, Long>()

        data.seeds.forEach { map[it] = findLocation(it, data) }

        return map.values.minOf { it }
    }

    private fun solveB(input: Array<String>): Long {
        /*
        val data = parse(input)

        for (i in 0..Long.MAX_VALUE) {
            val location = findSeed(i, data)

            for (i in 0..<data.seeds.size step 2) {
                if (location >= data.seeds[i] && location <= (data.seeds[i] + data.seeds[i+1]))
                    return location
            }
        }

        return -3
        // */

        //*
        val data = parse(input)
        var smallestLocation = Long.MAX_VALUE

        for (i in 0..<data.seeds.size step 2) {
            val start = data.seeds[i]
            val length = data.seeds[i+1]
            val end = start + length

            for (seed in start..(end+1)) {
                //println("checking $j")
                val location = findLocation(seed, data)

                if (location < smallestLocation) {
                    println("New smallest! Seed $seed at loc $location")
                    smallestLocation = location
                }
            }
        }

        return smallestLocation
        // */

        /*
        // -Xmx62g
        val data = parse(input)
        val seeds = mutableListOf<Long>()
        val map = mutableMapOf<Long, Long>()

        for (i in 0..<data.seeds.size step 2) {
            val start = data.seeds[i]
            val end = start + data.seeds[i + 1]

            for (j in start..end) seeds.add(j)
        }

        data.seeds.forEach { map[it] = findLocation(it, data) }

        return map.values.minOf { it }
        // */
    }

    private fun findLocation(seed: Long, input: Input): Long {
        return checkMaps(
            checkMaps(
                checkMaps(
                    checkMaps(
                        checkMaps(
                            checkMaps(
                                checkMaps(
                                    seed,
                                    input.seedToSoil),
                                input.soilToFertilizer),
                            input.fertilizerToWater),
                        input.waterToLight),
                    input.lightToTemperature),
                input.temperatureToHumidity),
            input.humidityToLocation)
    }

    private fun findSeed(location: Long, input: Input): Long {
        return checkMaps(
            checkMaps(
                checkMaps(
                    checkMaps(
                        checkMaps(
                            checkMaps(
                                checkMaps(
                                    location,
                                    input.humidityToLocation),
                                input.temperatureToHumidity),
                            input.lightToTemperature),
                        input.waterToLight),
                    input.fertilizerToWater),
                input.soilToFertilizer),
            input.seedToSoil)
    }

    private fun checkMaps(me: Long, section: Section): Long {
        section.data.forEach { map ->
            val start = map.sourceStart
            val end = map.sourceStart + map.length
            val offset = map.destinationStart - map.sourceStart

            if (me in start..end) {
                return me + offset
            }
        }

        return me
    }

    private fun parse(input: Array<String>): Input {
        val result = Input()

        val seedsLine = input[0].substring(7)
        val seeds = seedsLine.getLongs().toMutableList()
        result.seeds = seeds

        var currentSection = ""
        var currentEntries = mutableListOf<MapEntry>()

        for (i in input.indices) {
            if (i < 2) continue

            val line = input[i].trim()

            if (line.isEmpty() || "ECOMPUTERSAREDUMB" == line) {
                // We've hit the boundary between two sections
                when (currentSection) {
                    "seed-to-soil" -> {
                        result.seedToSoil.header = currentSection
                        result.seedToSoil.data = currentEntries
                    }
                    "soil-to-fertilizer" -> {
                        result.soilToFertilizer.header = currentSection
                        result.soilToFertilizer.data = currentEntries
                    }
                    "fertilizer-to-water" -> {
                        result.fertilizerToWater.header = currentSection
                        result.fertilizerToWater.data = currentEntries
                    }
                    "water-to-light" -> {
                        result.waterToLight.header = currentSection
                        result.waterToLight.data = currentEntries
                    }
                    "light-to-temperature" -> {
                        result.lightToTemperature.header = currentSection
                        result.lightToTemperature.data = currentEntries
                    }
                    "temperature-to-humidity" -> {
                        result.temperatureToHumidity.header = currentSection
                        result.temperatureToHumidity.data = currentEntries
                    }
                    "humidity-to-location" -> {
                        result.humidityToLocation.header = currentSection
                        result.humidityToLocation.data = currentEntries
                    }
                }

                currentSection = ""
                currentEntries = mutableListOf()
            } else if (!line[0].isDigit()) {
                // We're at a new header
                currentSection = line.substring(0, line.length - 5)
                currentEntries = mutableListOf()
            } else {
                // Data!
                val numbers = line.getLongs()
                currentEntries.add(MapEntry(numbers[0], numbers[1], numbers[2]))
            }
        }

        return result
    }

    data class Section(var header: String, var data: List<MapEntry>)
    data class MapEntry(val destinationStart: Long, val sourceStart: Long, val length: Long)

    class Input {
        var seeds = mutableListOf<Long>()
        val seedToSoil = Section("", mutableListOf())
        val soilToFertilizer = Section("", mutableListOf())
        val fertilizerToWater = Section("", mutableListOf())
        val waterToLight = Section("", mutableListOf())
        val lightToTemperature = Section("", mutableListOf())
        val temperatureToHumidity = Section("", mutableListOf())
        val humidityToLocation = Section("", mutableListOf())

        val sections = listOf(seedToSoil,
            soilToFertilizer,
            fertilizerToWater,
            waterToLight,
            lightToTemperature,
            temperatureToHumidity,
            humidityToLocation)
    }
}