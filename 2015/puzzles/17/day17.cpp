#include <fstream>
#include <iostream>
#include <memory>
#include "day17.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;
using std::vector;

const int Day17::EGGNOG_LITERS = 150;
const string Day17::FILENAME = "puzzles/17/input.txt";

void Day17::run()
{
    vector<int> containers = read_containers();
    vector<vector<int>> combinations = get_valid_combinations(containers);

    size_t part1 = combinations.size();
    size_t part2 = count_shortest_combinations(combinations);

    cout << "** Day 17 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

size_t Day17::count_shortest_combinations(const vector<vector<int>>& combinations)
{
    size_t count = 0;
    size_t minSize = SIZE_MAX;

    for(const vector<int>& combination : combinations)
    {
        if(combination.size() < minSize)
        {
            count = 0;
            minSize = combination.size();
        }

        if(combination.size() == minSize)
            count++;
    } 

    return count;
}

void Day17::get_valid_combinations(vector<vector<int>>& combinations, const vector<int> &containers, vector<int> &currCombination, int currSum, size_t idx)
{
    if(currSum == EGGNOG_LITERS)
    {
        combinations.push_back(currCombination);
        return;
    }
    if(currSum >= EGGNOG_LITERS || idx == containers.size())
        return;
    
    get_valid_combinations(combinations, containers, currCombination, currSum, idx + 1);

    currCombination.push_back(containers[idx]);
    get_valid_combinations(combinations, containers, currCombination, currSum + containers[idx], idx + 1);  
    currCombination.pop_back();
}

vector<int> Day17::read_containers()
{
    std::ifstream file(FILENAME);

    if(!file.is_open())
        throw std::invalid_argument("File not found");

    vector<int> containers;
    string line;

    while(!file.eof())
    {
        getline(file, line);

        if(line.empty())
            continue;

        containers.push_back(stoi(line));
    }

    return containers;
}

vector<vector<int>> Day17::get_valid_combinations(const vector<int> &containers)
{
    vector<vector<int>> combinations;
    vector<int> aux;

    get_valid_combinations(combinations, containers, aux);

    return combinations;
}
