/*
 * Advent of Code 2016
 * Day 11 (part 1)
 *
 * Command: clang++ --std=c++14 day11a.cpp
 *
 */

#include <fstream>
#include <iostream>
#include <map>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

const string INPUT_FILE = "input11.txt";
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

string trim(string input) {
	/*
	auto result = input;
	auto lastChar = input.at(input.end() - 1);

	if (lastChar == '.' || lastChar == ',') {
		result = input.substr(input.length() - 1);
	}
	*/

	auto result = input;
	auto endPos = result.find_last_not_of(" \t.,");
	
	if (endPos != string::npos) {
		result = result.substr(0, endPos + 1);
	}
	
	return result;
}

int main(void) {
	// Open the input file
	ifstream fin(INPUT_FILE);
	if (!fin) {
		cerr << "Error reading input file " << INPUT_FILE << endl;
		return -1;
	}

	// Read the input
	vector<string> input;
	char cInput[MAX_LINE_LENGTH];
	while (fin.getline(cInput, MAX_LINE_LENGTH)) {
		input.push_back(string(cInput));
	}
	fin.close();

	// Solve the problem
	map<int, vector<string>> floors;

	for (auto line : input) {
		auto bits = split(line, ' ');

		// Figure out what floor we're on
		auto floorWord = bits[1];
		auto floor = -1;

		if (floorWord == "first") { floor = 1; }
		else if (floorWord == "second") { floor = 2; }
		else if (floorWord == "third") { floor = 3; }
		else if (floorWord == "fourth") { floor = 4; }
		else {
			cerr << "Unexpected floor: " << floorWord << endl;
			return -1;
		}

		floors[floor].empty();

		for (int i = 4; i < bits.size() - 1; i++) {
			auto one = trim(bits.at(i));
			auto two = trim(bits.at(i + 1));
			string name;

			if (two == "generator") {
				auto isotope = one;
				char firstCharacter = ::toupper(isotope.at(0));
				name = firstCharacter;
				name += "G";
			} else if (two == "microchip") {
				auto isotope = one;
				char firstCharacter = ::toupper(isotope.at(0));
				name = firstCharacter;
				name += "M";
			}

			if (name.length()) {
				floors[floor].push_back(name);
			}
		}
	}

	for (auto &pair : floors) {
		auto floor = pair.first;
		auto contents = pair.second;

		cout << "F" << floor << ": ";
		for (auto thing : contents) {
			cout << thing << " ";
		}
		cout << endl;
	}

	return 0;
}

