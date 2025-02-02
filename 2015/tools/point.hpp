#ifndef POINT_H
#define POINT_H

#include <iostream>
#include <string>

namespace AoC2015::Tools
{
    class Point
    {
    public:
        int x, y;

        Point();
        Point(int, int);

        bool operator==(const Point &) const;
        bool operator!=(const Point &) const;

        Point operator+(const Point &) const;
        Point operator-(const Point &) const;
        Point &operator+=(const Point &);

        friend std::ostream &operator<<(std::ostream &os, const Point &p)
        {
            os << p.x << ", " << p.y;
            return os;
        }
    };
}

namespace std
{
    template <>
    struct hash<AoC2015::Tools::Point>
    {
        size_t operator()(const AoC2015::Tools::Point &p) const;
    };
}

#endif