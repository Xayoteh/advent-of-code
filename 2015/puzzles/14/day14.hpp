#ifndef DAY14_H
#define DAY14_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Reindeer;

    class Day14
    {
    public:
        Day14() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int get_distance(const Reindeer &, int);
        static int get_max_distance(const std::vector<Reindeer> &, int);
        static int get_max_score(const std::vector<Reindeer> &, int);

        static std::vector<Reindeer> read_reindeers_data();
    };

    class Reindeer
    {
    public:
        int flySpeed, flyTime, restTime;
        std::string name;
    };
}

#endif