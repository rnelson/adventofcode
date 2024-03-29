# cmake -D CMAKE_C_COMPILER=clang -D CMAKE_CXX_COMPILER=clang++ .

cmake_minimum_required (VERSION 3.28)
project (AdventOfCode)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_FLAGS "-O3")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} "${CMAKE_SOURCE_DIR}/contrib/cmake")

add_executable(day01a day01a.cpp)
add_executable(day01b day01b.cpp)
add_executable(day02a day02a.cpp)
add_executable(day02b day02b.cpp)
add_executable(day04a day04a.cpp)
add_executable(day04b day04b.cpp)
add_executable(day06a day06a.cpp)
add_executable(day06b day06b.cpp)
add_executable(day07a day07a.cpp)
add_executable(day07b day07b.cpp)
add_executable(day08a day08a.cpp)
add_executable(day08b day08b.cpp)
add_executable(day09a day09a.cpp)
add_executable(day09b day09b.cpp)
add_executable(day10a day10a.cpp)
add_executable(day10b day10b.cpp)
#add_executable(day11a day11a.cpp)
#add_executable(day11b day11b.cpp)
add_executable(day12a day12a.cpp)
add_executable(day12b day12b.cpp)
#add_executable(day13a day13a.cpp)
#add_executable(day13b day13b.cpp)
add_executable(day15a day15a.cpp)
add_executable(day15b day15b.cpp)
add_executable(day16a day16a.cpp)
add_executable(day16b day16b.cpp)
add_executable(day18a day18a.cpp)
add_executable(day18b day18b.cpp)
add_executable(day19a day19a.cpp)
add_executable(day19b day19b.cpp)
add_executable(day20a day20a.cpp)
add_executable(day20b day20b.cpp)
add_executable(day21a day21a.cpp)
add_executable(day21b day21b.cpp)
add_executable(day22a day22a.cpp)
#add_executable(day22b day22b.cpp)

set(Boost_USE_STATIC_LIBS ON)
find_package(Boost)
if(Boost_FOUND)
	include_directories(${Boost_INCLUDE_DIRS})

	add_executable(day03a day03a.cpp)
	add_executable(day03b day03b.cpp)

	target_link_libraries(day03a ${Boost_LIBRARIES})
	target_link_libraries(day03b ${Boost_LIBRARIES})
endif()

find_package(OpenSSL)
if(OPENSSL_FOUND)
	include_directories(${OPENSSL_INCLUDE_DIRS})

	add_executable(day05a day05a.cpp)
	add_executable(day05b day05b.cpp)
	add_executable(day14a day14a.cpp)
	add_executable(day14b day14b.cpp)

	link_libraries(crypto)
	target_link_libraries(day05a ${OPENSSL_LIBRARIES})
	target_link_libraries(day05b ${OPENSSL_LIBRARIES})
	target_link_libraries(day14a ${OPENSSL_LIBRARIES})
	target_link_libraries(day14b ${OPENSSL_LIBRARIES})
endif()
