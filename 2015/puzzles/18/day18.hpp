#ifndef DAY18_H
#define DAY18_H

#include <string>
#include <vector>
#include "../../tools/point.hpp"

namespace AoC2015::Puzzles
{
    class Day18
    {
    public:
        Day18() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static void get_next_state(std::vector<std::vector<bool>> &);
        static void turn_corners_on(std::vector<std::vector<bool>> &);

        static bool at(const std::vector<std::vector<bool>> &, const Tools::Point &);
        static bool equals_at(const std::vector<std::vector<bool>> &, const Tools::Point &, bool);
        static bool is_in_bounds(const std::vector<std::vector<bool>> &, const Tools::Point &);
        static size_t count_lights_on(const std::vector<std::vector<bool>> &);

        static std::vector<std::vector<bool>> get_next_state(std::vector<std::vector<bool>>, int, bool = false);
        static std::vector<std::vector<bool>> read_initial_config();
    };
}

#endif