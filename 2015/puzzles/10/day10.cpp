#include <iostream>
#include "day10.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;

void Day10::run()
{
    string input = "1113222113";

    string sequenceAfter40 = get_final_sequence(input, 40);
    // No need to compute 40 iterations again
    string sequenceAfter50 = get_final_sequence(sequenceAfter40, 10);

    size_t part1 = sequenceAfter40.size();
    size_t part2 = sequenceAfter50.size();

    cout << "** Day 10 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

string Day10::get_final_sequence(string sequence, int iterations)
{
    for (int i = 0; i < iterations; i++)
        sequence = get_next_sequence(sequence);

    return sequence;
}

string Day10::get_next_sequence(const string &sequence)
{
    string nextSequence;
    char curr = sequence[0];
    size_t count = 0;

    for (char c : sequence + '.')
    {
        if (c == curr)
            count++;
        else
        {
            nextSequence += std::to_string(count) + curr;
            curr = c;
            count = 1;
        }
    }

    return nextSequence;
}