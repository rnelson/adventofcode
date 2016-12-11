/*
 * Advent of Code 2016
 * Day 10 (part 1)
 *
 * Command: clang++ --std=c++14 day10a.cpp
 *
 */

#include <fstream>
#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

const int CHIP_A = 61;
const int CHIP_B = 17;

const string INPUT_FILE = "input10.txt";
const int MAX_LINE_LENGTH = 2000;
enum magnitude_t { LOW, HIGH };
enum destination_t { OUTPUT, BOT };

struct Bot {
	int number;
	int chip1;
	int chip2;
};

struct Instruction {
	magnitude_t chipType;
	destination_t destinationType;
	int destinationNumber;
};

struct InstructionPair {
	int sourceBotNumber;
	vector<Instruction> instructions;
};

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

void processQueue(vector<InstructionPair> &q, map<int, Bot> &bots, map<int, int> &output) {
	int i = 0;

	for (; i < q.size(); i++) {
		auto pair = q.at(i);

		// See if this bot has both chips and can do work
		auto bot = bots[pair.sourceBotNumber];
		if (bot.chip1 < 0 || bot.chip2 < 0) {
			continue;
		}

		// Find the min/max chip
		auto minChip = (bot.chip1 < bot.chip2 ? bot.chip1 : bot.chip2);
		auto maxChip = (bot.chip1 > bot.chip2 ? bot.chip1 : bot.chip2);

		//if (minChip == CHIP_A || maxChip == CHIP_A || minChip == CHIP_B || maxChip == CHIP_B) {
		if (true) {
			cout << "Bot " << pair.sourceBotNumber << endl;
			cout << "  C1:  " << bot.chip1 << endl;
			cout << "  C2:  " << bot.chip2 << endl;
			cout << "  Min: " << minChip << endl;
			cout << "  Max: " << maxChip << endl;
			cout << endl;
		}

		// Part A answer
		if (minChip == CHIP_B && maxChip == CHIP_A) {
			cout << "Bot " << pair.sourceBotNumber << " compares microchips with values " << CHIP_A << " and " << CHIP_B << "." << endl;
		}

		// Process the instructions
		for (auto i : pair.instructions) {
			auto value = (i.chipType == LOW ? minChip : maxChip);
			auto key = i.destinationNumber;

			switch (i.destinationType) {
				case OUTPUT:
					output[key] = value;
					break;
				case BOT:
					auto destBot = bots[key];

					if (destBot.chip1 < 0) {
						destBot.chip1 = value;
					} else {
						destBot.chip2 = value;
					}

					bots[key] = destBot;
					break;
			}
		}

		// Reset this bot
		bot.chip1 = numeric_limits<int>::min();
		bot.chip2 = numeric_limits<int>::min();
		bots[pair.sourceBotNumber] = bot;

		// Remove this instruction from the queue and start over
		q.erase(q.begin() + i);
		i = 0;
	}
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
	map<int, Bot> bots;
	map<int, int> output;
	vector<InstructionPair> q;

	for (auto line : input) {
		// Determine the instruction type
		if (line.substr(0, 5) == "value") {
			// If it's a value line, simply set the value
			auto bits = split(line, ' ');
			auto bot = stoi(bits[5]);
			auto value = stoi(bits[1]);

			Bot b;
			auto it = bots.find(bot);

			if (it != bots.end()) {
				b = bots[bot];
				bots.erase(bot);

				if (b.chip1 < 0) {
					b.chip1 = value;
				} else if (b.chip2 < 0) {
					b.chip2 = value;
				}

				bots[bot] = b;
			} else {
				b.number = bot;
				b.chip1 = value;
				b.chip2 = numeric_limits<int>::min();
			}
		} else {
			// Otherwise, sort it out and add it to our queue
			auto bits = split(line, ' ');

			auto givingBot = stoi(bits[1]);
			auto lowDestinationType = bits[5];
			auto lowDestinationNum = stoi(bits[6]);
			auto highDestinationType = bits[10];
			auto highDestinationNum = stoi(bits[11]);

			Instruction i1;
			i1.chipType = LOW;
			i1.destinationType = (lowDestinationType == "bot" ? BOT : OUTPUT);
			i1.destinationNumber = lowDestinationNum;

			Instruction i2;
			i2.chipType = HIGH;
			i2.destinationType = (highDestinationType == "bot" ? BOT : OUTPUT);
			i2.destinationNumber = highDestinationNum;

			InstructionPair p;
			p.sourceBotNumber = givingBot;
			p.instructions.push_back(i1);
			p.instructions.push_back(i2);

			q.push_back(p);
		}

		// Now that we've done a new instruction, see if we can do anything in our queue
		processQueue(q, bots, output);
	}

	// If we've made it through the file, keep going through the queue until it's complete
	while (q.size()) {
		processQueue(q, bots, output);
	}

	return 0;
}

