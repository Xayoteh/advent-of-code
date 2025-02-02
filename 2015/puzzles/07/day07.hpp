#ifndef DAY07_H
#define DAY07_H

#include <string>
#include <unordered_map>
#include <vector>

using ushort = unsigned short;

namespace AoC2015::Puzzles
{
    class Gate;

    class Day07
    {
    public:
        Day07() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static bool is_number(const std::string &);
        static ushort get_gate_output(const Gate &, std::unordered_map<std::string, ushort> &, const std::unordered_map<std::string, Gate> &);
        static ushort get_wire_value(const std::string &, std::unordered_map<std::string, ushort> &, const std::unordered_map<std::string, Gate> &);

        static std::pair<std::unordered_map<std::string, ushort>, std::unordered_map<std::string, Gate>> read_input();
    };

    class Gate
    {
    public:
        std::string input1, input2, op;

        static Gate parse(const std::vector<std::string> &);
    };
}

#endif