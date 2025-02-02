#ifndef DAY10_H
#define DAY10_H

#include <string>

namespace AoC2015::Puzzles
{
    class Day10
    {
    public:
        Day10() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static std::string get_final_sequence(std::string, int);
        static std::string get_next_sequence(const std::string &);
    };
}

#endif