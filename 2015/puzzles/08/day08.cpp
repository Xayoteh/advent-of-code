#include <fstream>
#include <iostream>
#include "day08.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day08::FILENAME = "puzzles/08/input.txt";

void Day08::run()
{
    vector<string> strings = read_list();

    auto [part1, part2] = get_total_differences(strings);

    cout << "** Day 8 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

std::pair<size_t, size_t> Day08::get_total_differences(const vector<string> &sv)
{
    size_t in_memory_diff = 0, encoded_diff = 0;

    for (const string &s : sv)
    {
        in_memory_diff += s.size() - get_in_memory_size(s);
        encoded_diff += get_encoded_size(s) - s.size();
    }

    return {in_memory_diff, encoded_diff};
}

size_t Day08::get_in_memory_size(const string &s)
{
    size_t count = s.size() - 2;

    for (size_t i = 1; i < s.size() - 1; i++)
    {
        if (s[i] == '\\')
        {
            if (s[i + 1] == '"' || s[i + 1] == s[i])
            {
                count--;
                i++;
            }
            else if (s[i + 1] == 'x')
            {
                count -= 3;
                i += 3;
            }
        }
    }

    return count;
}

size_t Day08::get_encoded_size(const string &s)
{
    size_t count = 2;

    for (char c : s)
        if (c == '\\' || c == '"')
            count += 2;
        else
            count++;

    return count;
}

vector<string> Day08::read_list()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<string> items;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        items.push_back(line);
    }

    return items;
}