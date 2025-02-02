#include <cmath>
#include <fstream>
#include <iostream>
#include "day21.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::vector;
using std::pair;

const string Day21::FILENAME = "puzzles/21/input.txt";
const vector<Item> Day21::WEAPONS{{8, {4, 0}}, {10, {5, 0}}, {25, {6, 0}}, {40, {7, 0}}, {74, {8, 0}}};
const vector<Item> Day21::ARMORS{{13, {0, 1}}, {31, {0, 2}}, {53, {0, 3}}, {75, {0, 4}}, {102, {0, 5}}};
const vector<Item> Day21::RINGS{{25, {1, 0}}, {50, {2, 0}}, {100, {3, 0}}, {20, {0, 1}}, {40, {0, 2}}, {80, {0, 3}}};
const vector<vector<Item>> Day21::SHOP{WEAPONS, ARMORS, RINGS, RINGS};

void Day21::run()
{
    Entity boss = read_boss_info();
    Entity player{100};

    vector<pair<int, bool>> results = get_possible_results(player, boss);

    int part1 = get_minimum_gold(results, true);
    int part2 = get_maximum_gold(results, false);
    ;

    cout << "** Day 21 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

Entity Day21::read_boss_info()
{
    std::ifstream file(FILENAME);

    if (!file.is_open())
        throw std::invalid_argument("File not found");

    vector<int> stats;
    string line;

    while (!file.eof())
    {
        getline(file, line);

        if (line.empty())
            continue;

        stats.push_back(stoi(Utilities::split(line, ": ")[1]));
    }

    return {stats[0], {stats[1], stats[2]}};
}

vector<pair<int, bool>> Day21::get_possible_results(Entity &player, const Entity &boss)
{
    vector<pair<int, bool>> results;
    vector<Item> aux;

    get_possible_results(results, player, boss, aux);

    return results;
}

void Day21::get_possible_results(vector<pair<int, bool>> &results, Entity &player, const Entity &boss, vector<Item>& currItems, size_t idx, int itemsPrice)
{
    if (idx == SHOP.size())
    {
        results.push_back({itemsPrice, can_win(player, boss)});
        return;
    }

    if (idx != 0)
        get_possible_results(results, player, boss, currItems, idx + 1, itemsPrice);

    for (const Item &item : SHOP[idx])
    {
        if (idx > 1 && currItems.back() == item)
            continue;

        currItems.push_back(item);
        player.stats += item.stats;

        get_possible_results(results, player, boss, currItems, idx + 1, itemsPrice + item.price);

        player.stats -= item.stats;
        currItems.pop_back();
    }
}

int Day21::get_maximum_gold(const vector<pair<int, bool>>& results, bool canWin)
{
    int maxGold = 0;

    for(const auto& [goldSpent, result] : results)
        if(result == canWin)
            maxGold = std::max(maxGold, goldSpent);

    return maxGold;
}

int Day21::get_minimum_gold(const vector<pair<int, bool>>& results, bool canWin)
{
    int minGold = INT_MAX;

    for(const auto& [goldSpent, result] : results)
        if(result == canWin)
            minGold = std::min(minGold, goldSpent);

    return minGold;
}

bool Day21::can_win(const Entity &player, const Entity &boss)
{
    int playerHits = ceilf((float)boss.hp / (std::max(player.stats.damage - boss.stats.armor, 1)));
    int bossHits = ceilf((float)player.hp / (std::max(boss.stats.damage - player.stats.armor, 1)));

    // since player hits firts, if both need same amount of hits then player will win
    return playerHits <= bossHits;
}

Stats &Stats::operator+=(const Stats &s)
{
    armor += s.armor;
    damage += s.damage;
    return *this;
}

Stats &Stats::operator-=(const Stats &s)
{
    armor -= s.armor;
    damage -= s.damage;
    return *this;
}

bool Stats::operator==(const Stats &s) const
{
    return armor == s.armor && damage == s.damage;
}

bool Item::operator==(const Item &i) const
{
    return price == i.price && stats == i.stats;
}
