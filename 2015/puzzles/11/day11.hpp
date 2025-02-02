#ifndef DAY11_H
#define DAY11_H

#include <string>

namespace AoC2015::Puzzles
{
    class Day11
    {
    public:
        Day11() = delete;
        static void run();

    private:
        static void increment(std::string &);

        static bool is_valid_password(const std::string &);

        static std::string get_next_valid_password(std::string);
    };
}

#endif