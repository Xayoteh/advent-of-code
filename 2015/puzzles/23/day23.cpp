#include <fstream>
#include <iostream>
#include "day23.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::pair;
using std::string;
using std::unordered_map;
using std::vector;

const string Day23::FILENAME = "puzzles/23/input.txt";

void Day23::run()
{
    vector<pair<string, vector<string>>> instructions = read_instructions();

    unordered_map<string, int> registersWithA0 = execute(instructions);
    unordered_map<string, int> registersWithA1 = execute(instructions, 1);

    int part1 = registersWithA0["b"];
    int part2 = registersWithA1["b"];

    cout << "** Day 23 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

void Day23::hlf(unordered_map<string, int> &registers, const string &param)
{
    registers[param] >>= 1;
}

void Day23::tpl(unordered_map<string, int> &registers, const string &param)
{
    registers[param] *= 3;
}

void Day23::inc(unordered_map<string, int> &registers, const string &param)
{
    registers[param]++;
}

void Day23::jmp(const string &param, size_t &idx)
{
    idx += (stoi(param) - 1);
}

void Day23::jie(const unordered_map<string, int> &registers, const vector<string> &params, size_t &idx)
{
    if (!(registers.at(params[0]) & 1))
        idx += (stoi(params[1]) - 1);
}

void Day23::jio(const unordered_map<string, int> &registers, const vector<string> &params, size_t &idx)
{
    if (registers.at(params[0]) == 1)
        idx += (stoi(params[1]) - 1);
}

unordered_map<string, int> Day23::execute(const vector<pair<string, vector<string>>> &instructions, int aValue)
{
    unordered_map<string, int> registers{{"a", aValue}, {"b", 0}};

    for (size_t i = 0; i < instructions.size(); i++)
    {
        string ins = instructions[i].first;
        vector<string> params = instructions[i].second;

        if (ins == "hlf")
            hlf(registers, params[0]);
        else if (ins == "tpl")
            tpl(registers, params[0]);
        else if (ins == "inc")
            inc(registers, params[0]);
        else if (ins == "jmp")
            jmp(params[0], i);
        else if (ins == "jie")
            jie(registers, params, i);
        else
            jio(registers, params, i);
    }

    return registers;
}

vector<pair<string, vector<string>>> Day23::read_instructions()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<pair<string, vector<string>>> instructions;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        size_t split_idx = line.find(' ');
        string ins = line.substr(0, split_idx);
        string params = line.substr(split_idx + 1);

        instructions.push_back({ins, Utilities::split(params, ", ")});
    }

    return instructions;
}
