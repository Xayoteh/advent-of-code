#include <iostream>
#include "directions.hpp"

using namespace AoC2015::Tools;

const Point Directions::UP{0, -1};
const Point Directions::DOWN{0, 1};
const Point Directions::LEFT{-1, 0};
const Point Directions::RIGHT{1, 0};

const Point Directions::TOP_LEFT{-1, -1};
const Point Directions::TOP_RIGHT{1, -1};
const Point Directions::BOTTOM_LEFT{-1, 1};
const Point Directions::BOTTOM_RIGHT{1, 1};

const std::vector<Point> Directions::ALL{UP, DOWN, LEFT, RIGHT, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT};