#include <iostream>
#include "day20.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;

void Day20::run()
{
    int input = 34000000;

    int part1 = get_first_house(input, 10);
    int part2 = get_first_house(input, 11, 50);

    cout << "** Day 20 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day20::get_first_house(int target, int mult, int maxHousesVisited)
{
    std::vector<int> houses(target / mult + 1, 0);

    for (int elf = 1; elf <= target / mult; elf++)
    {
        int housesVisited = 0;

        for (int house = elf; house <= target / mult; house += elf)
        {
            houses[house] += elf * mult;

            if (++housesVisited == maxHousesVisited)
                break;
        }
    }

    for (int house = 1; house < houses.size(); house++)
        if (houses[house] >= target)
            return house;

    return -1;
}