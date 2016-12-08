/*
 * Advent of Code 2016
 * Day 7 (part 2)
 *
 * Command: clang++ --std=c++14 day07b.cpp
 *
 */

#include <algorithm>
#include <fstream>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

const string INPUT_FILE = "input07.txt";
const int MAX_LINE_LENGTH = 2000;

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
	int count = 0;

	for (auto line : input) {
		cout << "LINE:" << line << endl;

		vector<string> aba;
		vector<string> bab;

		int end = line.length() - 2;
		bool inside = false;

		for (int i = 0; i < end; i++) {
			// Check to see if we've entered/ended a hypernet sequence
			if (line.at(i) == '[') {
				inside = true;
				continue;
			} else if (line.at(i) == ']') {
				inside = false;
				continue;
			}

			// Look for ABAs/BABs
			if (line.at(i) == line.at(i + 2)) {
				if (line.at(i) != line.at(i + 1)) {
					if (line.at(i) != '[' && line.at(i) != ']' && line.at(i + 1) != '[' && line.at(i + 1) != ']') {
						if (!inside) {
							string candidate = line.substr(i, 3);

							cout << "ABA:" << candidate << endl;

							aba.push_back(candidate);
						} else {
							string candidate;
							candidate += line.at(i + 1);
							candidate += line.at(i);
							candidate += line.at(i + 1);

							cout << "BAB:" << candidate << endl;

							bab.push_back(candidate);
						}
					}
				}
			}
		}

		// Check for a match
		for (auto a : aba) {
			bool add = false;

			for (auto b : bab) {
				if (a == b) {
					cout << "MATCH:" << a << "/" << b << endl;

					add = true;
					break;
				}
			}

			if (add) {
				count++;
			}
		}
		
		cout << endl;
	}

	cout << "Count: " << count << endl;

	return 0;
}

