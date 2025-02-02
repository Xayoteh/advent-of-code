#ifndef DAY03_H
#define DAY03_H

#include <string>
#include <unordered_set>
#include "../../tools/point.hpp"

namespace AoC2015::Puzzles
{
    class Day03
    {
    public:
        Day03() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static std::string read_moves();

        static std::unordered_set<Tools::Point> get_locations(const std::string &);
        static std::unordered_set<Tools::Point> get_next_year_locations(const std::string &);

        static Tools::Point get_direction(char c);
    };
}

#endif