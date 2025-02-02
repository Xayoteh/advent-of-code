#ifndef DAY20_H
#define DAY20_H

#include <vector>

namespace AoC2015::Puzzles
{
    class Day20
    {
    public:
        Day20() = delete;
        static void run();

    public:
        static int get_first_house(int, int, int = INT_MAX);
    };
}

#endif