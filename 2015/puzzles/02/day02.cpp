#include <algorithm>
#include <fstream>
#include <iostream>
#include "day02.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day02::FILENAME = "puzzles/02/input.txt";

void Day02::run()
{
    vector<Box> boxes = read_boxes();

    int part1 = get_total_paper_needed(boxes);
    int part2 = get_total_ribbon_needed(boxes);

    cout << "** Day 2 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

vector<Box> Day02::read_boxes()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Box> boxes;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<string> strings = Utilities::split(line, 'x');

        boxes.push_back(Box::parse(strings));
    }

    return boxes;
}

int Day02::get_total_paper_needed(const vector<Box> &boxes)
{
    int total = 0;

    for (Box box : boxes)
        total += get_paper_needed(box);

    return total;
}

int Day02::get_paper_needed(Box box)
{
    int lw = box.length * box.width;
    int lh = box.length * box.height;
    int wh = box.width * box.height;

    return 2 * lw + 2 * lh + 2 * wh + std::min({lw, lh, wh});
}

int Day02::get_total_ribbon_needed(const vector<Box> &boxes)
{
    int total = 0;

    for (Box box : boxes)
        total += get_ribbon_needed(box);

    return total;
}

int Day02::get_ribbon_needed(Box box)
{
    int lw = 2 * box.length + 2 * box.width;
    int lh = 2 * box.length + 2 * box.height;
    int wh = 2 * box.width + 2 * box.height;

    return box.length * box.width * box.height + std::min({lw, lh, wh});
}

Box Box::parse(const vector<string> &strings)
{
    return {stoi(strings[0]), stoi(strings[1]), stoi(strings[2])};
}
