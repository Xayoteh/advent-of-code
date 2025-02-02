#ifndef DAY13_H
#define DAY13_H

#include <string>
#include <unordered_set>

namespace AoC2015::Puzzles
{
    class Edge;

    class Day13
    {
    public:
        Day13() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int get_max_total_happiness_change(const std::unordered_set<Edge> &, bool = false);

        static std::unordered_set<Edge> read_happiness_changes();
    };

    class Edge
    {
    public:
        std::string vertexA, vertexB;
        int weigth;

        bool operator<(const Edge &) const;
        bool operator==(const Edge &) const;
    };
}

namespace std
{
    template <>
    struct hash<AoC2015::Puzzles::Edge>
    {
        size_t operator()(const AoC2015::Puzzles::Edge &) const;
    };
}

#endif