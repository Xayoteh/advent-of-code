#include <fstream>
#include <iostream>
#include "day06.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const size_t Day06::GRID_SIZE = 1000;
const string Day06::FILENAME = "puzzles/06/input.txt";

void Day06::run()
{
    vector<Instruction> instructions = read_instructions();

    vector<vector<int>> setup = setup_lights(instructions);
    vector<vector<int>> setup2 = setup_lights(instructions, true);

    int part1 = get_total_brightness(setup);
    int part2 = get_total_brightness(setup2);

    cout << "** Day 6 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

vector<vector<int>> Day06::setup_lights(const vector<Instruction> &instructions, bool trueInstruction)
{
    vector<vector<int>> lights(GRID_SIZE, vector<int>(GRID_SIZE, 0));

    for (const Instruction &instruction : instructions)
    {
        if (instruction.op == 0)
            turn_off(lights, instruction, trueInstruction);
        else if (instruction.op == 1)
            turn_on(lights, instruction, trueInstruction);
        else
            toggle(lights, instruction, trueInstruction);
    }

    return lights;
}

void Day06::turn_on(vector<vector<int>> &lights, const Instruction &ins, bool trueInstruction)
{
    for (size_t y = ins.y0; y <= ins.y1; y++)
        for (size_t x = ins.x0; x <= ins.x1; x++)
        {
            if (trueInstruction)
                lights[y][x] += 1;
            else
                lights[y][x] = 1;
        }
}

void Day06::turn_off(vector<vector<int>> &lights, const Instruction &ins, bool trueInstruction)
{
    for (size_t y = ins.y0; y <= ins.y1; y++)
    {
        for (size_t x = ins.x0; x <= ins.x1; x++)
        {
            if (trueInstruction)
                lights[y][x] = std::max(lights[y][x] - 1, 0);
            else
                lights[y][x] = 0;
        }
    }
}

void Day06::toggle(vector<vector<int>> &lights, const Instruction &ins, bool trueInstruction)
{
    for (size_t y = ins.y0; y <= ins.y1; y++)
    {
        for (size_t x = ins.x0; x <= ins.x1; x++)
        {
            if (trueInstruction)
                lights[y][x] += 2;
            else
                lights[y][x] = lights[y][x] ? 0 : 1;
        }
    }
}

int Day06::get_total_brightness(const vector<vector<int>> &lights)
{
    int brightness = 0;

    for (size_t y = 0; y < GRID_SIZE; y++)
        for (size_t x = 0; x < GRID_SIZE; x++)
            brightness += lights[x][y];

    return brightness;
}

vector<Instruction> Day06::read_instructions()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Instruction> instructions;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        instructions.push_back(Instruction::parse(line));
    }

    return instructions;
}

Instruction Instruction::parse(const string &s)
{
    vector<string> data = Utilities::split(s, ' ');

    vector<string> xy1 = Utilities::split(data[data.size() - 3], ',');
    vector<string> xy2 = Utilities::split(data[data.size() - 1], ',');
    int op = data[1] == "off" ? 0 : data[1] == "on" ? 1
                                                    : 2;

    return {stoull(xy1[0]), stoull(xy1[1]), stoull(xy2[0]), stoull(xy2[1]), op};
}
