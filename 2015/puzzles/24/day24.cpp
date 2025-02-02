#include <fstream>
#include <iostream>
#include <numeric>
#include "day24.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day24::FILENAME = "puzzles/24/input.txt";

void Day24::run()
{
    vector<int> weights = read_weights();

    vector<vector<int>> shortestGroupsOf3 = get_shortest_groups(weights, 3);
    vector<vector<int>> shortestGroupsOf4 = get_shortest_groups(weights, 4);

    ullong part1 = get_minimum_quantum_entanglement(shortestGroupsOf3);
    ullong part2 = get_minimum_quantum_entanglement(shortestGroupsOf4);

    cout << "** Day 24 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

ullong Day24::get_quantum_entanglement(const vector<int> &v)
{
    return std::accumulate(v.begin(), v.end(), (ullong)1, std::multiplies<ullong>());
}

ullong Day24::get_minimum_quantum_entanglement(const vector<vector<int>> &groups)
{
    ullong minQE = ULONG_LONG_MAX;

    for (const auto &group : groups)
        minQE = std::min(minQE, get_quantum_entanglement(group));

    return minQE;
}

vector<int> Day24::read_weights()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<int> weights;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        weights.push_back(stoi(line));
    }

    return weights;
}

vector<vector<int>> Day24::get_shortest_groups(const vector<int> &weights, int parts)
{
    int totalWeight = std::accumulate(weights.begin(), weights.end(), 0);
    int groupWeight = totalWeight / parts;
    vector<vector<int>> groups;
    vector<int> aux;

    find_possible_groups(groups, weights, groupWeight, aux);

    return groups;
}

void Day24::find_possible_groups(vector<vector<int>> &groups, const vector<int> &weights, int groupWeight, vector<int> &currGroup, size_t idx, int currGroupWeight)
{
    // candidate group
    if (currGroupWeight == groupWeight)
    {
        // first group found
        if (groups.empty())
        {
            groups.push_back(currGroup);
            return;
        }

        // group too large
        if (currGroup.size() > groups[0].size())
            return;

        // new shortest group
        if (currGroup.size() < groups[0].size())
            groups.clear();

        groups.push_back(currGroup);

        return;
    }

    // end of vector or weight too big or group too large
    if (idx == weights.size() || currGroupWeight > groupWeight || (!groups.empty() && currGroup.size() >= groups[0].size()))
        return;

    find_possible_groups(groups, weights, groupWeight, currGroup, idx + 1, currGroupWeight);

    currGroup.push_back(weights[idx]);
    find_possible_groups(groups, weights, groupWeight, currGroup, idx + 1, currGroupWeight + weights[idx]);
    currGroup.pop_back();
}
