namespace AdventOfCode2024.Tools;

record Point(int X, int Y) 
{    
    public static Point operator+(Point a, Point b) =>
        new(a.X + b.X, a.Y + b.Y);

    public static Point operator-(Point a, Point b) =>
        new(a.X - b.X, a.Y - b.Y);

    public static Point operator*(Point a, int mul) =>
        new(a.X * mul, a.Y * mul);

    public override string ToString() => $"{X},{Y}";
};