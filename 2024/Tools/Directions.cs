namespace AdventOfCode2024.Tools;

static class Directions 
{
    public static readonly Point Up = new(0, -1);
    public static readonly Point Down = new(0, 1);
    public static readonly Point Left = new(-1, 0);
    public static readonly Point Right = new(1, 0);
    public static readonly Point TopRight = new(1, -1);
    public static readonly Point BottomRight = new(1, 1);
    public static readonly Point BottomLeft = new(-1, 1);
    public static readonly Point TopLeft = new(-1, -1);

    public static readonly Point[] WithoutDiagonals = [
        Up, Right, Down, Left
    ];

    public static readonly Point[] DiagonalsOnly = [
        TopRight, BottomRight, BottomLeft, TopLeft
    ];

    public static readonly Point[] All = [
        ..WithoutDiagonals, ..DiagonalsOnly
    ];

    // Clockwise
    public static Point Next(Point direction) => direction switch
    {
        _ when direction == Up => Right,
        _ when direction == Right => Down,
        _ when direction == Down => Left,
        _ when direction == Left => Up,
        _ when direction == TopRight => BottomRight,
        _ when direction == BottomRight => BottomLeft,
        _ when direction == BottomLeft => TopLeft,
        _ when direction == TopLeft => TopRight,
        _ => throw new NotImplementedException()
    };

    // Clockwise
    public static Point Previous(Point direction) => direction switch
    {
        _ when direction == Up => Left,
        _ when direction == Left => Down,
        _ when direction == Down => Right,
        _ when direction == Right => Up,
        _ when direction == TopRight => TopLeft,
        _ when direction == TopLeft => BottomLeft,
        _ when direction == BottomLeft => BottomRight,
        _ when direction == BottomRight => TopRight,
        _ => throw new NotImplementedException()
    };

    public static Point Parse(char c) => c switch
    {
        '>' => Right,
        'v' => Down,
        '<' => Left,
        '^' => Up,
        _ => throw new NotImplementedException()
    };
}