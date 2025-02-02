#ifndef d04
#define d04

#include <string>

namespace AoC2015::Puzzles
{
    class Day04
    {
    public:
        Day04() = delete;
        static void run();

    private:
        static bool starts_with_n_zeroes(unsigned char *, int);
        static int get_min_sufix_for_hash(const std::string &, int);
    };
}

#endif