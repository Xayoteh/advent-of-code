#include <algorithm>
#include <fstream>
#include <iostream>
#include "day07.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::unordered_map;
using std::vector;

const string Day07::FILENAME = "puzzles/07/input.txt";

void Day07::run()
{
    auto [originalWires, gates] = read_input();
    unordered_map<string, ushort> wires = originalWires;

    ushort part1 = get_wire_value("a", wires, gates);

    wires = originalWires;
    wires["b"] = part1;

    ushort part2 = get_wire_value("a", wires, gates);

    cout << "** Day 7 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

std::pair<unordered_map<string, ushort>, unordered_map<string, Gate>> Day07::read_input()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    unordered_map<string, ushort> wires;
    unordered_map<string, Gate> gates;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<string> data = Utilities::split(line, ' ');
        string out = data[data.size() - 1];

        if (data.size() == 3 && is_number(data[0]))
            wires[out] = stoi(data[0]);
        else
            gates[out] = Gate::parse(data);
    }

    return {wires, gates};
}

ushort Day07::get_wire_value(const string &wire, unordered_map<string, ushort> &wires, const unordered_map<string, Gate> &gates)
{
    if (!wires.count(wire))
        wires[wire] = get_gate_output(gates.at(wire), wires, gates);

    return wires[wire];
}

Gate Gate::parse(const vector<string> &data)
{
    Gate gate{};

    if (data.size() == 3)
        gate.input1 = data[0];
    else if (data[0] == "NOT")
    {
        gate.input1 = data[1];
        gate.op = data[0];
    }
    else
    {
        gate.input1 = data[0];
        gate.input2 = data[2];
        gate.op = data[1];
    }

    return gate;
}

ushort Day07::get_gate_output(const Gate &gate, unordered_map<string, ushort> &wires, const unordered_map<string, Gate> &gates)
{
    ushort input1 = is_number(gate.input1) ? stoi(gate.input1) : get_wire_value(gate.input1, wires, gates);

    if (gate.op.empty())
        return input1;
    else if (gate.op == "NOT")
        return ~input1;

    ushort input2 = is_number(gate.input2) ? stoi(gate.input2) : get_wire_value(gate.input2, wires, gates);

    if (gate.op == "AND")
        return input1 & input2;
    if (gate.op == "OR")
        return input1 | input2;
    if (gate.op == "LSHIFT")
        return input1 << input2;

    return input1 >> input2;
}

bool Day07::is_number(const string &s)
{
    return (s[0] == '+' || s[0] == '-' || isdigit(s[0])) && std::all_of(s.begin(), s.end(), [](char c)
                                                                        { return isdigit(c); });
}