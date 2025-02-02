#include <fstream>
#include <iostream>
#include <vector>
#include "day25.hpp"
#include "../../tools/directions.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;

const string Day25::FILENAME = "puzzles/25/input.txt";

void Day25::run()
{
    Point coord = read_machines_message();

    ullong part1 = calculate_code(coord);

    cout << "** Day 25 **" << endl;
    cout << "Part 1: " << part1 << endl;
}

ullong Day25::calculate_code(const Point &coord)
{
    ullong code = 20151125;
    Point curr(1, 1);

    while (curr != coord)
    {
        code *= 252533;
        code %= 33554393;

        curr += Directions::TOP_RIGHT;

        if (curr.y == 0)
            curr = {1, curr.x};
    }

    return code;
}

Point Day25::read_machines_message()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    string line;

    getline(file, line);

    std::vector<string> data = Utilities::split(line, ' ');

    string x = data[data.size() - 1];
    string y = data[data.size() - 3];
    Point coords;

    coords.x = stoi(x.substr(0, x.size() - 1));
    coords.y = stoi(y.substr(0, y.size() - 1));

    return coords;
}
