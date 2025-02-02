#ifndef DAY09_H
#define DAY09_H

#include <string>
#include <unordered_map>
#include <unordered_set>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day09
    {
    public:
        Day09() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static int find_longest_route(const std::unordered_map<std::string, std::vector<std::pair<std::string, int>>> &);
        static int find_longest_route(const std::unordered_map<std::string, std::vector<std::pair<std::string, int>>> &, std::unordered_set<std::string> &, const std::string &, int = 0);
        static int find_shortest_route(const std::unordered_map<std::string, std::vector<std::pair<std::string, int>>> &);
        static int find_shortest_route(const std::unordered_map<std::string, std::vector<std::pair<std::string, int>>> &, std::unordered_set<std::string> &, const std::string &, int = 0);

        static std::unordered_map<std::string, std::vector<std::pair<std::string, int>>> read_distances();
    };
}

#endif