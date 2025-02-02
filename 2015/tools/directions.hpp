#ifndef DIRECTIONS_H
#define DIRECTIONS_H

#include <vector>
#include "point.hpp"

namespace AoC2015::Tools::Directions
{
    extern const std::vector<Point> ALL;

    extern const Point UP;
    extern const Point DOWN;
    extern const Point LEFT;
    extern const Point RIGHT;

    extern const Point TOP_LEFT;
    extern const Point TOP_RIGHT;
    extern const Point BOTTOM_LEFT;
    extern const Point BOTTOM_RIGHT;
}

#endif