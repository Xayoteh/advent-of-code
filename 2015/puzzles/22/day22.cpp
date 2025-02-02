#include <algorithm>
#include <chrono>
#include <fstream>
#include <iostream>
#include <queue>
#include <unordered_set>
#include "day22.hpp"
#include "../../tools/utilities.hpp"

using namespace AoC2015::Puzzles;
using namespace AoC2015::Tools;

using std::cout, std::endl;
using std::string;
using std::unordered_map;
using std::vector;

const string Day22::FILENAME = "puzzles/22/input.txt";

const vector<Skill> Day22::SKILLS{
    {"magic missile", false, 53, 4},
    {"drain", false, 73, 2, 2},
    {"shield", true, 113},
    {"poison", true, 173},
    {"recharge", true, 229}};

unordered_map<string, Effect> Day22::effects{
    {"poison", Effect{6, 3}},
    {"shield", Effect{6, 0, 7}},
    {"recharge", Effect{5, 0, 0, 101}}};

void Day22::run()
{
    Boss boss = read_boss_info();
    Player player{50, 500};

    int part1 = INT_MAX;
    int part2 = INT_MAX;

    for (int i = 0; i < 10000; i++)
    {
        part1 = simulate(player, boss, part1);
        part2 = simulate(player, boss, part2, true);
    }

    cout << "** Day 22 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

Boss Day22::read_boss_info()
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

    return {stats[0], stats[1]};
}

size_t Day22::get_next_skill(Player &player, int manaSpent, int currMin)
{
    vector<size_t> indexes;

    for (size_t i = 0; i < SKILLS.size(); i++)
    {
        Skill skill = SKILLS[i];

        if ((manaSpent + skill.manaCost) < currMin && player.can_cast(skill, effects))
            indexes.push_back(i);
    }

    if (indexes.empty())
        return -1;

    size_t randIdx = rand() % indexes.size();

    return indexes[randIdx];
}

int Day22::simulate(Player player, Boss boss, int currMin, bool isHardMode)
{
    int manaSpent = 0;
    bool isPlayerTurn = true;

    reset_effects();

    while (player.hp > 0 && boss.hp > 0)
    {
        if (isHardMode && isPlayerTurn && player.hp == 1)
            break;

        apply_effects(player, boss);

        if (boss.hp <= 0)
            break;

        if (isPlayerTurn)
        {
            if (isHardMode)
                player.hp--;

            size_t skill = get_next_skill(player, manaSpent, currMin);

            if (skill == SIZE_MAX)
                break;

            player.cast(SKILLS[skill], boss, effects);
            manaSpent += SKILLS[skill].manaCost;
        }
        else
            player.hp -= std::max(1, boss.damage - player.armor);

        isPlayerTurn = !isPlayerTurn;
    }

    return boss.hp <= 0 ? manaSpent : currMin;
}

void Day22::reset_effects()
{
    for (auto &[name, eff] : effects)
        eff.reset();
}

void Day22::apply_effects(Player &p, Boss &b)
{
    for (auto &[name, eff] : effects)
        eff.apply(p, b, name);
}

void Player::cast(const Skill &s, Boss &b, unordered_map<string, Effect> &effects)
{
    mana -= s.manaCost;
    hp += s.healing;

    b.hp -= s.damage;

    if (s.hasEffect)
        effects[s.name].activate();
}

bool Player::can_cast(const Skill &s, const unordered_map<string, Effect> &effects) const
{
    return mana >= s.manaCost && (!s.hasEffect || !effects.at(s.name).is_active());
}

void Effect::apply(Player &p, Boss &b, const string &name)
{
    if (is_active())
    {
        p.mana += manaRegen;

        if (armorInc)
            p.armor = armorInc;

        b.hp -= damageOnTime;

        currTimer--;
    }
    else if (armorInc != 0)
        p.armor = 0;
}

bool Effect::is_active() const
{
    return currTimer != 0;
}

void Effect::activate()
{
    currTimer = TIMER;
}

void Effect::reset()
{
    currTimer = 0;
}
