#include <fstream>
#include <iostream>
#include "day01.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::unordered_map;

const string Day01::FILENAME = "puzzles/01/input.txt";

void Day01::run()
{
    string instructions = read_instructions();

    unordered_map<char, int> instructionCount = count_instructions(instructions);

    int part1 = get_final_floor(instructionCount);
    size_t part2 = get_first_instruction_to_enter_basement(instructions);

    cout << "** Day 1 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

string Day01::read_instructions()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    string instructions, line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        instructions += line;
    }

    return instructions;
}

unordered_map<char, int> Day01::count_instructions(const string &instructions)
{
    unordered_map<char, int> count;

    for (char instruction : instructions)
        count[instruction]++;

    return count;
}

int Day01::get_final_floor(const unordered_map<char, int> &instructionCount)
{
    return instructionCount.at('(') - instructionCount.at(')');
}

size_t Day01::get_first_instruction_to_enter_basement(const string &instructions)
{
    int currentFloor = 0;
    size_t i;

    for (i = 0; i < instructions.length() && currentFloor != -1; i++)
    {
        if (instructions[i] == '(')
            currentFloor++;
        else
            currentFloor--;
    }

    return i;
}