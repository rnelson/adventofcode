package com.github.rnelson.adventofcode.days

import com.github.rnelson.adventofcode.Day
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.time.temporal.ChronoUnit

class Day04: Day() {
    init {
        super.setup("04")
    }

    override fun solveA(): String {
        val entries = mutableListOf<LogEntry>()
        val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm")

        val ts1 = LocalDateTime.parse("1518-11-01 00:05", formatter)
        val ts2 = LocalDateTime.parse("1518-11-01 00:25", formatter).minusMinutes(1)
        val span = ChronoUnit.MINUTES.between(ts1, ts2)
        return span.toString()

        // Get everything into objects that are a bit easier to work with
        input.forEach {
            entries.add(LogEntry.parse(it, formatter))
        }

        // Sort everything
        val sortedEntries = entries.sortedBy { it.timestamp }

        // Fill in missing guard numbers
        var currentGuard = sortedEntries[0].guard
        sortedEntries.forEach {
            if (it.guard > -1) { currentGuard = it.guard }
            else { it.guard = currentGuard }
        }

        // Sum awake/asleep time for each shift
        val guards = hashMapOf<Int, Guard>()
        currentGuard = sortedEntries[0].guard
        var currentWake = 0
        var currentSleep = 0
        sortedEntries.forEach {
            when (it.action) {
                LogAction.BEGIN -> {


                    currentGuard = it.guard
                }
                LogAction.SLEEP -> {
                    //
                }
                LogAction.WAKE -> {
                    //
                }
            }
        }

        // Group the entries by guard
        val groupedEntries = entries.groupBy { it.guard }

        groupedEntries.forEach {
            println(it)
        }

        return ""
    }

    override fun solveB(): String {
        return ""
    }

    enum class LogAction {
        UNKNOWN, BEGIN, SLEEP, WAKE
    }

    class LogEntry {
        var timestamp: LocalDateTime? = null
        var action: LogAction = LogAction.UNKNOWN
        var guard: Int = -1

        companion object {
            fun parse(line: String, formatter: DateTimeFormatter): LogEntry {
                val entry = LogEntry()
                entry.timestamp = LocalDateTime.parse(line.substring(1, 17), formatter)

                val action = line.substring(19)
                entry.action = when {
                    action.startsWith("Guard") -> LogAction.BEGIN
                    action.startsWith("falls") -> LogAction.SLEEP
                    action.startsWith("wakes") -> LogAction.WAKE
                    else -> LogAction.UNKNOWN
                }

                if (entry.action == LogAction.UNKNOWN) {
                    println("ERROR: Unknown action: $action")
                }

                if (entry.action == LogAction.BEGIN) {
                    val idStart = action.indexOf('#') + 1
                    val idEnd = action.indexOf(' ', idStart)

                    entry.guard = action.substring(idStart, idEnd).toInt()
                }

                return entry
            }
        }

        override fun toString(): String {
            return "LogEntry(timestamp=$timestamp, action=$action, guard=$guard)"
        }
    }

    class Guard {
        var number: Int = -1
        val entries = mutableListOf<LogEntry>()
        var sleep: Int = 0
        var awake: Int = 0
    }
}