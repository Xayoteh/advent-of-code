#ifndef DAY06_H
#define DAY06_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Instruction;

    class Day06
    {
    public:
        Day06() = delete;
        static void run();

    private:
        static const std::size_t GRID_SIZE;
        static const std::string FILENAME;

        static void toggle(std::vector<std::vector<int>> &, const Instruction &, bool);
        static void turn_off(std::vector<std::vector<int>> &, const Instruction &, bool);
        static void turn_on(std::vector<std::vector<int>> &, const Instruction &, bool);

        static int get_total_brightness(const std::vector<std::vector<int>> &);

        static std::vector<std::vector<int>> setup_lights(const std::vector<Instruction> &, bool = false);
        static std::vector<Instruction> read_instructions();
    };

    class Instruction
    {
    public:
        size_t x0, y0, x1, y1;
        int op;

        static Instruction parse(const std::string &);
    };
}

#endif