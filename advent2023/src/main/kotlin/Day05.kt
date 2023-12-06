class Day05 : Day(5) {
    override fun partA(input: Array<String>): Any {
        return solve(input)
    }

    override fun partB(input: Array<String>): Any {
        return 0
    }

    fun solve(input: Array<String>): Any {
        val data = parse(input)

        val map = mutableMapOf<Long, Long>()
        data.seeds.forEach { map[it] = findLocation(it, data) }

        return map.values.minOf { it }
    }

    fun findLocation(seed: Long, input: Input): Long {
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

    fun checkMaps(me: Long, section: Section): Long {
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

    fun parse(input: Array<String>): Input {
        val result = Input()

        val seedsLine = input[0].substring(7)
        val seeds = seedsLine.getLongs().toMutableList()
        result.seeds = seeds

        var currentSection = ""
        var currentEntries = mutableListOf<MapEntry>()

        for (i in input.indices) {
            if (i < 2) continue

            val line = input[i].trim()

            if (line.isEmpty()) {
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
        val seedToSoil = Section("", mutableListOf<MapEntry>())
        val soilToFertilizer = Section("", mutableListOf<MapEntry>())
        val fertilizerToWater = Section("", mutableListOf<MapEntry>())
        val waterToLight = Section("", mutableListOf<MapEntry>())
        val lightToTemperature = Section("", mutableListOf<MapEntry>())
        val temperatureToHumidity = Section("", mutableListOf<MapEntry>())
        val humidityToLocation = Section("", mutableListOf<MapEntry>())

        val sections = listOf(seedToSoil,
            soilToFertilizer,
            fertilizerToWater,
            waterToLight,
            lightToTemperature,
            temperatureToHumidity,
            humidityToLocation)
    }
}