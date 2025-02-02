#include <fstream>
#include <iostream>
#include "day16.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day16::FILENAME = "puzzles/16/input.txt";
const Aunt Day16::AUNT{{"children", 3},
                       {"cats", 7},
                       {"samoyeds", 2},
                       {"pomeranians", 3},
                       {"akitas", 3},
                       {"vizslas", 0},
                       {"goldfish", 5},
                       {"trees", 3},
                       {"cars", 2},
                       {"perfumes", 1}};

void Day16::run()
{
    vector<Aunt> aunts = read_aunts();

    size_t part1 = get_aunt_number(aunts);
    size_t part2 = get_real_aunt_number(aunts);

    cout << "** Day 16 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

size_t Day16::get_aunt_number(const vector<Aunt> &aunts)
{
    for (size_t i = 0; i < aunts.size(); i++)
        if (is_similar(aunts[i]))
            return i + 1;
    return -1;
}

size_t Day16::get_real_aunt_number(const vector<Aunt> &aunts)
{
    for (size_t i = 0; i < aunts.size(); i++)
        if (is_similar_range(aunts[i]))
            return i + 1;
    return -1;
}

vector<Aunt> Day16::read_aunts()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Aunt> aunts;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        size_t splitIdx = line.find(':');
        vector<string> compounds = Utilities::split(line.substr(splitIdx + 2), ", ");
        Aunt aunt;

        for (const string &compound : compounds)
        {
            vector<string> data = Utilities::split(compound, ": ");
            string label = data[0];
            int kinds = stoi(data[1]);

            aunt[label] = kinds;
        }

        aunts.push_back(aunt);
    }

    return aunts;
}

bool Day16::is_similar(const Aunt &aunt)
{
    for (const auto &[compound, kinds] : aunt)
        if (kinds != AUNT.at(compound))
            return false;

    return true;
}

bool Day16::is_similar_range(const Aunt &aunt)
{
    for (const auto &[compound, kinds] : aunt)
    {
        int expectedKinds = AUNT.at(compound);

        if (compound == "cats" || compound == "trees")
        {
            if (kinds <= expectedKinds)
                return false;
        }
        else if (compound == "pomeranians" || compound == "goldfish")
        {
            if (kinds >= expectedKinds)
                return false;
        }
        else if (kinds != expectedKinds)
            return false;
    }

    return true;
}
