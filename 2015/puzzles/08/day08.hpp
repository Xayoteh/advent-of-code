#ifndef DAY08_H
#define DAY08_H

#include <string>
#include <vector>

namespace AoC2015::Puzzles
{
    class Day08
    {
    public:
        Day08() = delete;
        static void run();

    private:
        static const std::string FILENAME;

        static size_t get_encoded_size(const std::string &);
        static size_t get_in_memory_size(const std::string &);

        static std::pair<size_t, size_t> get_total_differences(const std::vector<std::string> &);
        static std::vector<std::string> read_list();
    };
}

#endif