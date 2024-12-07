/*
 * Advent of Code 2016
 * Day 9 (part 2)
 *
 * Command: clang++ --std=c++14 day09b.cpp
 * Sources:
 *		https://github.com/westappdev/aoc2016/blob/master/c%23/kmurrayDay9.cs (algorithm)
 *
 */

#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

const string INPUT_FILE = "../../aoc-inputs/2016/input09.txt";
const int MAX_LINE_LENGTH = 2000;

void split(const std::string &s, char delim, std::vector<std::string> &elems) {
	std::stringstream ss;
	ss.str(s);
	std::string item;
	while (std::getline(ss, item, delim)) {
		elems.push_back(item);
	}
}

std::vector<std::string> split(const std::string &s, char delim) {
	std::vector<std::string> elems;
	split(s, delim, elems);
	return elems;
}

unsigned long long decompress(string input) {
	unsigned long long length = 0L;

	for (size_t start = 0; start < input.length(); start++) {
		if (input.at(start) != '(') {
			length++;
		} else {
			// Find the "(AxB)"
			auto open = input.find("(", start);
			auto close = input.find(")", start);

			// Parse the "(AxB)"
			auto instruction = input.substr(open + 1, close - open - 1);
			auto instrBits = split(instruction, 'x');
			auto characters = stoi(instrBits.at(0));
			auto count = stoi(instrBits.at(1));

			// Grab the next `A` characters and see if there's an ( in it
			auto nextSegment = input.substr(close + 1, characters);
			auto internalOpen = nextSegment.find("(");

			if (internalOpen != string::npos) {
				auto substring = input.substr(close + 1, characters);
				length += count * decompress(substring);
			} else {
				length += count * characters;
			}

			start = close + characters;
		}
	}

	return length;
}

int main(void) {
	// Open the input file
	ifstream fin(INPUT_FILE);
	if (!fin) {
		cerr << "Error reading input file " << INPUT_FILE << endl;
		return -1;
	}

	// Read the input
	string input;
	fin >> input;
	fin.close();

	// Solve the problem
	auto length = decompress(input);
	cout << "Length: " << length << endl;

	return 0;
}

