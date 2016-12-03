cmake_minimum_required (VERSION 2.6)
project (AdventOfCode)

set(CMAKE_CXX_FLAGS "--std=c++14")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

add_executable(day01a day01a.cpp)
add_executable(day01b day01b.cpp)
add_executable(day02a day02a.cpp)
add_executable(day02b day02b.cpp)

#set(Boost_USE_STATIC_LIBS ON)
#find_package(Boost)
#if(Boost_FOUND)
#	include_directories(${Boost_INCLUDE_DIRS})
#	add_executable(day02a day02a.cpp)
#	target_link_libraries(day02a ${Boost_LIBRARIES})
#endif()
