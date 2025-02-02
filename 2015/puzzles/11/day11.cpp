#include <algorithm>
#include <iostream>
#include "day11.hpp"

using namespace ::AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;

void Day11::run()
{
    string input = "vzbxkghb";

    string part1 = get_next_valid_password(input);
    string part2 = get_next_valid_password(part1);

    cout << "** Day 11 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

string Day11::get_next_valid_password(string pass)
{
    do
    {
        increment(pass);
    } while (!is_valid_password(pass));

    return pass;
}

void Day11::increment(string &s)
{
    size_t skipIdx = s.find_first_of("iol");

    // Skip all strings containing i, o or l
    if (skipIdx != string::npos)
    {
        s[skipIdx]++;
        std::for_each(s.begin() + skipIdx + 1, s.end(), [](char &c)
                      { c = 'a'; });
        return;
    }

    for (size_t i = s.size() - 1; i >= 0; i--)
    {
        s[i]++;

        if (s[i] <= 'z')
            break;

        s[i] = 'a';
    }
}

bool Day11::is_valid_password(const string &s)
{
    bool containsIncreasingSequence = false;

    for (size_t i = 2; i < s.size(); i++)
        if (s[i - 1] == s[i] - 1 && s[i - 2] == s[i] - 2)
        {
            containsIncreasingSequence = true;
            break;
        }

    if (!containsIncreasingSequence)
        return false;

    auto firstPairIdx = std::adjacent_find(s.begin(), s.end());

    if (firstPairIdx == s.end())
        return false;

    return std::adjacent_find(firstPairIdx + 2, s.end()) != s.end();
}