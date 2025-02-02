#include <fstream>
#include <iostream>
#include "day15.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;

const int Day15::TEASPOONS = 100;
const string Day15::FILENAME = "puzzles/15/input.txt";

void Day15::run()
{
    vector<Properties> ingredients = read_ingredients();
    vector<Properties> cookies = get_possible_cookies(ingredients);

    int part1 = get_max_score(cookies);
    int part2 = get_max_score(cookies, 500);

    cout << "** Day 15 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day15::get_score(const Properties &p)
{
    int score = 1;

    score *= std::max(p.capacity, 0);
    score *= std::max(p.flavor, 0);
    score *= std::max(p.durability, 0);
    score *= std::max(p.texture, 0);

    return score;
}

int Day15::get_max_score(const vector<Properties> &cookies, int expectedCalories)
{
    int maxScore = 0;

    for (const Properties &cookie : cookies)
    {
        int score = get_score(cookie);

        if (expectedCalories == -1 || cookie.calories == expectedCalories)
            maxScore = std::max(maxScore, score);
    }

    return maxScore;
}

vector<Properties> Day15::get_possible_cookies(const vector<Properties> &ingredients)
{
    vector<Properties> cookies;
    Properties aux{};

    get_possible_cookies(cookies, ingredients, aux);

    return cookies;
}

void Day15::get_possible_cookies(vector<Properties> &cookies, const vector<Properties> &ingredients, Properties &cookie, int currTeaspoons, size_t idx)
{
    if (idx == ingredients.size())
    {
        cookies.push_back(cookie);
        return;
    }

    for (int i = 0; i <= TEASPOONS - currTeaspoons; i++)
    {
        cookie += ingredients[idx] * i;
        get_possible_cookies(cookies, ingredients, cookie, currTeaspoons + i, idx + 1);
        cookie -= ingredients[idx] * i;
    }
}

vector<Properties> Day15::read_ingredients()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<Properties> ingredients;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        ingredients.push_back(Properties::parse(line));
    }

    return ingredients;
}

Properties Properties::operator*(int n) const
{
    return {capacity * n, durability * n, flavor * n, texture * n, calories * n};
}

Properties Properties::operator+(const Properties &p) const
{
    return {capacity + p.capacity, durability + p.durability, flavor + p.flavor, texture + p.texture, calories + p.calories};
}

Properties &Properties::operator+=(const Properties &p)
{
    capacity += p.capacity;
    durability += p.durability;
    flavor += p.flavor;
    texture += p.texture;
    calories += p.calories;

    return *this;
}

Properties &Properties::operator-=(const Properties &p)
{
    capacity -= p.capacity;
    durability -= p.durability;
    flavor -= p.flavor;
    texture -= p.texture;
    calories -= p.calories;

    return *this;
}

Properties Properties::parse(const string &s)
{
    size_t splitPos = s.find(':');
    vector<string> data = Utilities::split(s.substr(splitPos + 2), ", ");

    int capacity = stoi(Utilities::split(data[0], ' ')[1]);
    int durability = stoi(Utilities::split(data[1], ' ')[1]);
    int flavor = stoi(Utilities::split(data[2], ' ')[1]);
    int texture = stoi(Utilities::split(data[3], ' ')[1]);
    int calories = stoi(Utilities::split(data[4], ' ')[1]);

    return {capacity, durability, flavor, texture, calories};
}
