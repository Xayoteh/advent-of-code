#ifndef DAY05_H
#define DAY05_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day05
    {
    public:
        Day05() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static bool contains_any(const std::string &, const std::vector<std::string> &);
        static bool contains_repeated_letter_gap(const std::string &);
        static bool contains_repeated_pair(const std::string &);
        static bool is_nice_string(const std::string &);
        static bool is_nice_string_new(const std::string &);
        static int count_vowels(const std::string &);

        static std::vector<std::string> get_nice_strings(const std::vector<std::string> &, bool = false);
        static std::vector<std::string> read_file();
    };

}

#endif