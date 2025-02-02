#ifndef DAY21_H
#define DAY21_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Entity;
    class Item;

    class Day21
    {
    public:
        Day21() = delete;
        static void run();

    private:
        static const std::string FILENAME;
        static const std::vector<std::vector<Item>> SHOP;
        static const std::vector<Item> ARMORS;
        static const std::vector<Item> RINGS;
        static const std::vector<Item> WEAPONS;

        static void get_possible_results(std::vector<std::pair<int, bool>> &, Entity &, const Entity &, std::vector<Item> &, size_t = 0, int = 0);

        static bool can_win(const Entity &, const Entity &);
        static int get_minimum_gold(const std::vector<std::pair<int, bool>> &, bool);
        static int get_maximum_gold(const std::vector<std::pair<int, bool>> &, bool);

        static std::vector<std::pair<int, bool>> get_possible_results(Entity &, const Entity &);

        static Entity read_boss_info();
    };

    class Stats
    {
    public:
        int damage, armor;

        bool operator==(const Stats &) const;

        Stats &operator+=(const Stats &);
        Stats &operator-=(const Stats &);
    };

    class Item
    {
    public:
        int price;
        Stats stats;

        bool operator==(const Item &) const;
    };

    class Entity
    {
    public:
        int hp;
        Stats stats;
    };
}

#endif