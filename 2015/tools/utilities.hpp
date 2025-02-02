#ifndef UTILITIES_H
#define UTILITIES_H

#include <vector>
#include <string>
#include "point.hpp"

namespace AoC2015::Tools::Utilities
{
    std::vector<std::string> split(const std::string &, char);
    std::vector<std::string> split(const std::string &, const std::string &);
}

#endif