#ifndef DAY16_H
#define DAY16_H

#include <string>
#include <unordered_map>
#include <vector>

using Aunt = std::unordered_map<std::string, int>;

namespace AoC2015::Puzzles
{
    class Day16
    {
    public:
        Day16() = delete;
        static void run();

    private:
        static const std::string FILENAME;
        static const Aunt AUNT;

        static bool is_similar(const Aunt &);
        static bool is_similar_range(const Aunt &);
        static size_t get_aunt_number(const std::vector<Aunt> &);
        static size_t get_real_aunt_number(const std::vector<Aunt> &);

        static std::vector<Aunt> read_aunts();
    };
}

#endif