#include "point.hpp"

using namespace AoC2015::Tools;

Point::Point() : x(0), y(0) {}

Point::Point(int _x, int _y) : x(_x), y(_y) {}

Point Point::operator+(const Point &p) const
{
    return Point(x + p.x, y + p.y);
}

Point &Point::operator+=(const Point &p)
{
    x += p.x;
    y += p.y;
    return *this;
}

Point Point::operator-(const Point &p) const
{
    return Point{x - p.x, y - p.y};
}

bool Point::operator==(const Point &p) const
{
    return x == p.x && y == p.y;
}

bool Point::operator!=(const Point &p) const
{
    return !(*this == p);
}

size_t std::hash<Point>::operator()(const Point &p) const
{
    return hash<size_t>()(p.x & p.y) ^ hash<size_t>()(p.x | p.y);
}
