#include <fstream>
#include <iostream>
#include "day18.hpp"
#include "../../tools/directions.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const string Day18::FILENAME = "puzzles/18/input.txt";

void Day18::run()
{
    vector<vector<bool>> initialConfig = read_initial_config();

    vector<vector<bool>> stateAfter100 = get_next_state(initialConfig, 100);
    vector<vector<bool>> realStateAfter100 = get_next_state(initialConfig, 100, true);

    size_t part1 = count_lights_on(stateAfter100);
    size_t part2 = count_lights_on(realStateAfter100);

    cout << "** Day 18 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

void Day18::turn_corners_on(vector<vector<bool>> &grid)
{
    grid[0][0] = true;
    grid[0][grid[0].size() - 1] = true;
    grid[grid.size() - 1][0] = true;
    grid[grid.size() - 1][grid[0].size() - 1] = true;
}

size_t Day18::count_lights_on(const vector<vector<bool>> &grid)
{
    size_t lights_on = 0;

    for (size_t y = 0; y < grid.size(); y++)
        for (size_t x = 0; x < grid[0].size(); x++)
            lights_on += grid[y][x];

    return lights_on;
}

vector<vector<bool>> Day18::get_next_state(vector<vector<bool>> grid, int steps, bool isCornerStuck)
{
    for (int i = 0; i < steps; i++)
    {
        if (isCornerStuck)
            turn_corners_on(grid);
        get_next_state(grid);
    }

    if (isCornerStuck)
        turn_corners_on(grid);

    return grid;
}

void Day18::get_next_state(vector<vector<bool>> &grid)
{
    vector<vector<bool>> currState = grid;

    for (size_t y = 0; y < grid.size(); y++)
    {
        for (size_t x = 0; x < grid[0].size(); x++)
        {
            int neighborsOn = 0;
            Point currPos(x, y);

            for (const Point &dir : Directions::ALL)
                neighborsOn += equals_at(currState, currPos + dir, true);

            if (currState[y][x])
                grid[y][x] = neighborsOn == 2 || neighborsOn == 3;
            else
                grid[y][x] = neighborsOn == 3;
        }
    }
}

vector<vector<bool>> Day18::read_initial_config()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<vector<bool>> grid;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        vector<bool> row;

        for (char c : line)
            row.push_back(c == '#');

        grid.push_back(row);
    }

    return grid;
}

bool Day18::at(const vector<vector<bool>> &v, const Point &p)
{
    return v[p.y][p.x];
}

bool Day18::equals_at(const vector<vector<bool>> &v, const Point &p, bool value)
{
    return is_in_bounds(v, p) && at(v, p) == value;
}

bool Day18::is_in_bounds(const vector<vector<bool>> &v, const Point &p)
{
    return p.y >= 0 && p.y < v.size() && p.x >= 0 && p.x < v[0].size();
}