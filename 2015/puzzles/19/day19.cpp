#include <algorithm>
#include <chrono>
#include <fstream>
#include <iostream>
#include <random>
#include <unordered_set>
#include "day19.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::pair;
using std::string;
using std::unordered_set;
using std::vector;

const string Day19::FILENAME = "puzzles/19/input.txt";

void Day19::run()
{
    auto input = read_input();
    vector<pair<string, string>> replacements = input.first;
    string medicineMolecule = input.second;

    size_t part1 = count_possible_molecules(medicineMolecule, replacements);
    size_t part2 = get_minimum_replacements_needed(medicineMolecule, replacements);

    cout << "** Day 19 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

// TODO: change to A* algorithm
size_t Day19::get_minimum_replacements_needed(const string &originalMolecule, vector<pair<string, string>> &replacements)
{
    string currentMolecule = originalMolecule;
    size_t minReplacements = 0;

    while (currentMolecule != "e")
    {
        string previousMolecule = currentMolecule;

        for (const auto &[replacement, toReplace] : replacements)
        {
            size_t replaceIdx = currentMolecule.find(toReplace);

            if (replaceIdx != string::npos)
            {
                currentMolecule.replace(replaceIdx, toReplace.size(), replacement);
                minReplacements++;
            }
        }

        if (currentMolecule == previousMolecule)
        {
            currentMolecule = originalMolecule;
            minReplacements = 0;

            auto seed = std::chrono::system_clock::now().time_since_epoch().count();
            std::shuffle(replacements.begin(), replacements.end(), std::default_random_engine(seed));
        }
    }

    return minReplacements;
}

size_t Day19::count_possible_molecules(const string &medicineMolecule, const vector<pair<string, string>> &replacements)
{
    unordered_set<string> molecules;

    for (const auto &[toReplace, replacement] : replacements)
    {
        size_t replaceIdx = 0;

        while ((replaceIdx = medicineMolecule.find(toReplace, replaceIdx)) != string::npos)
        {
            molecules.insert(string(medicineMolecule).replace(replaceIdx, toReplace.size(), replacement));
            replaceIdx += toReplace.size();
        }
    }

    return molecules.size();
}

pair<vector<pair<string, string>>, string> Day19::read_input()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<pair<string, string>> replacements;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            break;

        vector<string> data = Utilities::split(line, " => ");

        replacements.push_back({data[0], data[1]});
    }

    getline(file, line);

    return {replacements, line};
}
