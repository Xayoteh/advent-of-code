#ifndef DAY02_H
#define DAY02_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Box;

    class Day02
    {
    public:
        Day02() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int get_paper_needed(Box);
        static int get_ribbon_needed(Box);
        static int get_total_paper_needed(const std::vector<Box> &);
        static int get_total_ribbon_needed(const std::vector<Box> &);

        static std::vector<Box> read_boxes();
    };

    class Box
    {
    public:
        int length, width, height;

        static Box parse(const std::vector<std::string> &);
    };
}

#endif