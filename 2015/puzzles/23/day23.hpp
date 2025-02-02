#ifndef DAY23_H
#define DAY23_H

#include <string>
#include <unordered_map>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day23
    {
    public:
        Day23() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static void hlf(std::unordered_map<std::string, int> &, const std::string &);
        static void inc(std::unordered_map<std::string, int> &, const std::string &);
        static void jie(const std::unordered_map<std::string, int> &, const std::vector<std::string> &, size_t &);
        static void jio(const std::unordered_map<std::string, int> &, const std::vector<std::string> &, size_t &);
        static void jmp(const std::string &, size_t &);
        static void tpl(std::unordered_map<std::string, int> &, const std::string &);

        static std::unordered_map<std::string, int> execute(const std::vector<std::pair<std::string, std::vector<std::string>>> &, int = 0);
        static std::vector<std::pair<std::string, std::vector<std::string>>> read_instructions();
    };
}

#endif