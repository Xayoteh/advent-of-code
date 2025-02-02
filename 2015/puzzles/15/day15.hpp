#ifndef DAY15_H
#define DAY15_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Properties;

    class Day15
    {
    public:
        Day15() = delete;
        static void run();

    private:
        static const int TEASPOONS;
        static const std::string FILENAME;

        static void get_possible_cookies(std::vector<Properties> &, const std::vector<Properties> &, Properties &, int = 0, size_t = 0);

        static int get_max_score(const std::vector<Properties> &, int = -1);
        static int get_score(const Properties &);

        static std::vector<Properties> get_possible_cookies(const std::vector<Properties> &);
        static std::vector<Properties> read_ingredients();
    };

    class Properties
    {
    public:
        int capacity, durability, flavor, texture, calories;

        Properties operator*(int) const;
        Properties operator+(const Properties &) const;
        Properties &operator+=(const Properties &);
        Properties &operator-=(const Properties &);

        static Properties parse(const std::string &);
    };
}

#endif