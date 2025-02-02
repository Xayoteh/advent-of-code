#include <algorithm>
#include <fstream>
#include <iostream>
#include <queue>
#include <unordered_map>
#include <vector>
#include "day13.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::unordered_set;
using std::vector;

const string Day13::FILENAME = "puzzles/13/input.txt";

void Day13::run()
{
    unordered_set<Edge> happinessChanges = read_happiness_changes();

    int part1 = get_max_total_happiness_change(happinessChanges);
    int part2 = get_max_total_happiness_change(happinessChanges, true);

    cout << "** Day 13 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day13::get_max_total_happiness_change(const unordered_set<Edge> &changes, bool includeSelf)
{
    std::priority_queue<Edge> pq(changes.begin(), changes.end());
    std::unordered_map<string, int> neighborCount;
    int lastChange, totalChange = 0;

    while (!pq.empty())
    {
        Edge edge = pq.top();
        pq.pop();

        if (neighborCount[edge.vertexA] == 2 || neighborCount[edge.vertexB] == 2)
            continue;

        neighborCount[edge.vertexA]++;
        neighborCount[edge.vertexB]++;
        totalChange += edge.weigth;

        // keep track of last edge's weight
        lastChange = edge.weigth;
    }

    // if self is included subtract the last conection's weight
    return totalChange - (includeSelf ? lastChange : 0);
}

unordered_set<Edge> Day13::read_happiness_changes()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Edge> temp;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<string> data = Utilities::split(line.substr(0, line.size() - 1), ' ');

        string a = data.front(), b = data.back();
        int sign = data[2] == "lose" ? -1 : 1;
        int change = stoi(data[3]);

        auto it = std::find(temp.begin(), temp.end(), Edge{a, b});

        if (it == temp.end())
            temp.push_back({a, b, sign * change});
        else
            it->weigth += sign * change;
    }

    return unordered_set<Edge>(temp.begin(), temp.end());
}

bool Edge::operator<(const Edge &e) const
{
    return weigth < e.weigth;
}

bool Edge::operator==(const Edge &e) const
{
    return (vertexA == e.vertexA && vertexB == e.vertexB) || (vertexA == e.vertexB && vertexB == e.vertexA);
}

size_t std::hash<Edge>::operator()(const Edge &e) const
{
    return hash<string>()(e.vertexA) ^ hash<string>()(e.vertexB);
}
