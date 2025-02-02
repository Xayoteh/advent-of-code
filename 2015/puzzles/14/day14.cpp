#include <fstream>
#include <iostream>
#include <unordered_map>
#include "day14.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day14::FILENAME = "puzzles/14/input.txt";

void Day14::run()
{
    vector<Reindeer> reindeers = read_reindeers_data();

    int part1 = get_max_distance(reindeers, 2503);
    int part2 = get_max_score(reindeers, 2503);

    cout << "** Day 14 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day14::get_max_distance(const vector<Reindeer> &reindeers, int time)
{
    int distance = 0;

    for (const Reindeer &reindeer : reindeers)
        distance = std::max(distance, get_distance(reindeer, time));

    return distance;
}

int Day14::get_max_score(const vector<Reindeer> &reindeers, int time)
{
    std::unordered_map<string, int> score;
    vector<string> leads;
    int leadDistance = 0;

    for (int t = 1; t <= time; t++)
    {
        leads.clear();

        for (const Reindeer &reindeer : reindeers)
        {
            int distance = get_distance(reindeer, t);

            // if current distances is greater than lead distance
            // then its the new lead distance and leads should reset
            if (distance > leadDistance)
            {
                leads.clear();
                leadDistance = distance;
            }

            if (distance == leadDistance)
                leads.push_back(reindeer.name);
        }

        for (const string &lead : leads)
            score[lead]++;
    }

    int maxScore = 0;

    for (const auto &[_, score] : score)
        maxScore = std::max(maxScore, score);

    return maxScore;
}

vector<Reindeer> Day14::read_reindeers_data()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Reindeer> reindeers;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<string> data = Utilities::split(line, ' ');

        string name = data[0];
        int flySpeed = stoi(data[3]);
        int flyTime = stoi(data[6]);
        int restTime = stoi(data[13]);

        reindeers.push_back({flySpeed, flyTime, restTime, name});
    }

    return reindeers;
}

int Day14::get_distance(const Reindeer &r, int time)
{
    int cycleTime = r.flyTime + r.restTime;
    int fullCycles = time / cycleTime;
    int distance = r.flySpeed * fullCycles * r.flyTime;

    time -= fullCycles * cycleTime;
    distance += std::min(time * r.flySpeed, r.flyTime * r.flySpeed);

    return distance;
}