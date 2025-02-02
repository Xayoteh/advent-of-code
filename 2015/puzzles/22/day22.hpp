#ifndef DAY22_H
#define DAY22_H

#include <string>
#include <unordered_map>
#include <vector>

namespace AoC2015::Puzzles
{
    class Boss;
    class Effect;
    class Player;
    class Skill;

    class Day22
    {
    public:
        Day22() = delete;
        static void run();

    private:
        static const std::string FILENAME;
        static const std::vector<Skill> SKILLS;
        static std::unordered_map<std::string, Effect> effects;

        static void apply_effects(Player &, Boss &);
        static void reset_effects();

        static size_t get_next_skill(Player &, int, int);
        static int simulate(Player, Boss, int, bool = false);

        static Boss read_boss_info();
    };

    class Boss
    {
    public:
        int hp, damage;
    };

    class Player
    {
    public:
        int hp, mana, armor;

        bool can_cast(const Skill &, const std::unordered_map<std::string, Effect> &) const;
        void cast(const Skill &, Boss &, std::unordered_map<std::string, Effect> &);
    };

    class Effect
    {
    public:
        int TIMER, damageOnTime, armorInc, manaRegen, currTimer;

        void apply(Player &, Boss &, const std::string &);
        bool is_active() const;
        void activate();
        void reset();
    };

    class Skill
    {
    public:
        std::string name;
        bool hasEffect;
        int manaCost, damage, healing;
    };
}

#endif