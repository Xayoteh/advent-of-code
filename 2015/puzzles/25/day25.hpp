#ifndef DAY25_H
#define DAY25_H

#include <string>
#include "../../tools/point.hpp"

using ullong = unsigned long long;

namespace AoC2015::Puzzles
{
    class Day25
    {
    public:
        Day25() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static ullong calculate_code(const Tools::Point &);

        static Tools::Point read_machines_message();
    };
}

#endif