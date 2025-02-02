#ifndef DAY17_H
#define DAY17_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day17
    {
    public:
        Day17() = delete;
        static void run();

    private:
        static const int EGGNOG_LITERS;
        static const std::string FILENAME;

        static void get_valid_combinations(std::vector<std::vector<int>> &, const std::vector<int> &, std::vector<int> &, int = 0, size_t = 0);

        static size_t count_shortest_combinations(const std::vector<std::vector<int>> &);

        static std::vector<int> read_containers();
        static std::vector<std::vector<int>> get_valid_combinations(const std::vector<int> &);
    };
}

#endif