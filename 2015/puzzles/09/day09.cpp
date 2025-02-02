#include <fstream>
#include <iostream>
#include "day09.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::pair;
using std::string;
using std::unordered_map;
using std::unordered_set;
using std::vector;

const string Day09::FILENAME = "puzzles/09/input.txt";

void Day09::run()
{
    unordered_map<string, vector<pair<string, int>>> distances = read_distances();

    int part1 = find_shortest_route(distances);
    int part2 = find_longest_route(distances);

    cout << "** Day 9 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

unordered_map<string, vector<pair<string, int>>> Day09::read_distances()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    unordered_map<string, vector<pair<string, int>>> distances;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<string> data = Utilities::split(line, ' ');

        string a = data[0], b = data[2];
        int distance = stoi(data[4]);

        distances[a].push_back({b, distance});
        distances[b].push_back({a, distance});
    }

    return distances;
}

int Day09::find_shortest_route(const unordered_map<string, vector<pair<string, int>>> &distances)
{
    int shortest = INT_MAX;
    unordered_set<string> seen;

    for (const auto &[node, _] : distances)
    {
        seen.clear();
        shortest = std::min(find_shortest_route(distances, seen, node), shortest);
    }

    return shortest;
}

int Day09::find_shortest_route(const unordered_map<string, vector<pair<string, int>>> &distances, unordered_set<string> &seen, const string &currNode, int currRouteDistance)
{
    seen.insert(currNode);

    int shortest = INT_MAX;

    for (const auto &[node, distance] : distances.at(currNode))
    {
        if (!seen.count(node))
            shortest = std::min(find_shortest_route(distances, seen, node, currRouteDistance + distance), shortest);
    }

    seen.erase(currNode);

    return shortest == INT_MAX ? currRouteDistance : shortest;
}

int Day09::find_longest_route(const unordered_map<string, vector<pair<string, int>>> &distances)
{
    int longest = 0;
    unordered_set<string> seen;

    for (const auto &[node, _] : distances)
    {
        seen.clear();
        longest = std::max(find_longest_route(distances, seen, node), longest);
    }

    return longest;
}

int Day09::find_longest_route(const unordered_map<string, vector<pair<string, int>>> &distances, unordered_set<string> &seen, const string &currNode, int currRouteDistance)
{
    seen.insert(currNode);

    int longest = 0;

    for (const auto &[node, distance] : distances.at(currNode))
    {
        if (!seen.count(node))
            longest = std::max(find_longest_route(distances, seen, node, currRouteDistance + distance), longest);
    }

    seen.erase(currNode);

    return longest == 0 ? currRouteDistance : longest;
}
