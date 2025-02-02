#ifndef DAY24_H
#define DAY24_H

#include <string>
#include <vector>

using ullong = unsigned long long;

namespace AoC2015::Puzzles
{
    class Day24
    {
    public:
        Day24() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static void find_possible_groups(std::vector<std::vector<int>> &, const std::vector<int> &, int, std::vector<int> &, size_t = 0, int = 0);

        static ullong get_minimum_quantum_entanglement(const std::vector<std::vector<int>> &);
        static ullong get_quantum_entanglement(const std::vector<int> &);

        static std::vector<int> read_weights();
        static std::vector<std::vector<int>> get_shortest_groups(const std::vector<int> &, int);
    };
}

#endif