#ifndef DAY01_H
#define DAY01_H

#include <string>
#include <unordered_map>

namespace AoC2015::Puzzles
{
    class Day01
    {
    public:
        Day01() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int get_final_floor(const std::unordered_map<char, int> &);
        static size_t get_first_instruction_to_enter_basement(const std::string &);

        static std::string read_instructions();
        static std::unordered_map<char, int> count_instructions(const std::string &);
    };
}

#endif