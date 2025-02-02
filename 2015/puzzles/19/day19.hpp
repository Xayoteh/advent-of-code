#ifndef DAY19_H
#define DAY19_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day19
    {
    public:
        Day19() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static size_t count_possible_molecules(const std::string &, const std::vector<std::pair<std::string, std::string>> &);
        static size_t get_minimum_replacements_needed(const std::string &, std::vector<std::pair<std::string, std::string>> &);

        static std::pair<std::vector<std::pair<std::string, std::string>>, std::string> read_input();
    };
}

#endif