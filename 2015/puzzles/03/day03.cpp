#include <fstream>
#include <iostream>
#include "day03.hpp"
#include "../../tools/directions.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::unordered_set;

const string Day03::FILENAME = "puzzles/03/input.txt";

void Day03::run()
{
    string moves = read_moves();

    unordered_set<Point> locations = get_locations(moves);
    unordered_set<Point> nextYearLocations = get_next_year_locations(moves);

    size_t part1 = locations.size();
    size_t part2 = nextYearLocations.size();

    cout << "** Day 3 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

string Day03::read_moves()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    string moves, line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        moves += line;
    }

    return moves;
}

unordered_set<Point> Day03::get_locations(const string &moves)
{
    unordered_set<Point> houses;
    Point position;

    houses.insert(position);

    for (char move : moves)
    {
        position += get_direction(move);
        houses.insert(position);
    }

    return houses;
}

unordered_set<Point> Day03::get_next_year_locations(const string &moves)
{
    unordered_set<Point> houses;
    Point santaPosition, roboSantaPosition;

    houses.insert(santaPosition);

    for (size_t i = 0; i < moves.size(); i++)
    {
        if (i & 1)
        {
            roboSantaPosition += get_direction(moves[i]);
            houses.insert(roboSantaPosition);
        }
        else
        {
            santaPosition += get_direction(moves[i]);
            houses.insert(santaPosition);
        }
    }

    return houses;
}

Point Day03::get_direction(char c)
{
    switch (c)
    {
    case '>':
        return Directions::RIGHT;
    case '<':
        return Directions::LEFT;
    case 'v':
        return Directions::DOWN;
    case '^':
        return Directions::UP;
    default:
        throw std::invalid_argument("Unknown direction");
    }
}