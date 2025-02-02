#include <iostream>
#include <openssl/md5.h>
#include "day04.hpp"

using namespace AoC2015::Puzzles;

using std::cout, std::endl;
using std::string;

void Day04::run()
{
    string input = "iwrupvqb";

    int part1 = get_min_sufix_for_hash(input, 5);
    int part2 = get_min_sufix_for_hash(input, 6);

    cout << "** Day 4 **" << endl;
    cout << "Part 1: " << part1 << endl;
    cout << "Part 2: " << part2 << endl;
}

int Day04::get_min_sufix_for_hash(const string &s, int zeroes)
{
    int min = 0;
    unsigned char result[MD5_DIGEST_LENGTH];

    do
    {
        string curr = s + std::to_string(++min);
        // TODO: Make own implementation
        MD5((unsigned char *)curr.c_str(), curr.size(), result);
    } while (!starts_with_n_zeroes(result, zeroes));

    return min;
}

bool Day04::starts_with_n_zeroes(unsigned char *md, int zeroes)
{
    bool isEven = !(zeroes & 1);
    int firstNonZero = isEven ? (zeroes / 2 - 1) : (zeroes / 2);

    for (int i = 0; i < firstNonZero; i++)
        if (md[i] != 0)
            return false;

    if (!isEven)
        return md[firstNonZero] < 16;

    return md[firstNonZero] == 0 && md[firstNonZero + 1] > 15;
}