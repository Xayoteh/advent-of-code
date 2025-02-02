#include <algorithm>
#include <fstream>
#include <iostream>
#include "day05.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day05::FILENAME = "puzzles/05/input.txt";

void Day05::run()
{
    vector<string> stringsInFile = read_file();

    vector<string> niceStrings = get_nice_strings(stringsInFile);
    vector<string> niceStrings2 = get_nice_strings(stringsInFile, true);

    size_t part1 = niceStrings.size();
    size_t part2 = niceStrings2.size();

    cout << "** Day 5 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

vector<string> Day05::read_file()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<string> strings;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        strings.push_back(line);
    }

    return strings;
}

vector<string> Day05::get_nice_strings(const vector<string> &strings, bool useNewModel)
{
    vector<string> niceStrings;

    for (const string &s : strings)
    {
        if (!useNewModel && is_nice_string(s))
            niceStrings.push_back(s);
        else if (useNewModel && is_nice_string_new(s))
            niceStrings.push_back(s);
    }

    return niceStrings;
}

bool Day05::is_nice_string(const string &s)
{
    if (count_vowels(s) < 3)
        return false;

    if (std::adjacent_find(s.begin(), s.end()) == s.end())
        return false;

    return !contains_any(s, {"ab", "cd", "pq", "xy"});
}

int Day05::count_vowels(const string &s)
{
    string vowels = "aeiou";
    int count = 0;

    for (char c : s)
    {
        bool isVowel = std::any_of(vowels.begin(), vowels.end(), [c](char vowel)
                                   { return vowel == c; });
        if (isVowel)
            count++;
    }

    return count;
}

bool Day05::is_nice_string_new(const string &s)
{
    if (!contains_repeated_pair(s))
        return false;

    return contains_repeated_letter_gap(s);
}

bool Day05::contains_repeated_pair(const string &s)
{
    for (size_t i = 0; i < s.size() - 1; i++)
        for (size_t j = i + 2; j < s.size() - 1; j++)
            if (s.at(i) == s.at(j) && s.at(i + 1) == s.at(j + 1))
                return true;

    return false;
}

bool Day05::contains_repeated_letter_gap(const string &s)
{
    for (size_t i = 2; i < s.size(); i++)
        if (s.at(i) == s.at(i - 2))
            return true;

    return false;
}

bool Day05::contains_any(const string &s, const vector<string> &sv)
{
    return std::any_of(sv.begin(), sv.end(), [s](const string &sf)
                       { return s.find(sf) != string::npos; });
}
