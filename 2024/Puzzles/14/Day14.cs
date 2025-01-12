using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day14
{
    const string FileName = "Puzzles/14/input.txt";
    const int Width = 101;
    const int Height = 103;

    public static void Run()
    {
        var elapsedSeconds = 100;

        List<Robot> robots = ReadRobots();
        List<Point> finalPositions = robots.GetPositions(elapsedSeconds);

        int[] robotsPerQuadrant = finalPositions.CountRobotsPerQuadrant();

        int part1 = robotsPerQuadrant.CalculateSecurityFactor();
        int part2 = robots.CalculateTimeForTree();

        Console.WriteLine("** Day 14 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<Robot> ReadRobots()
    {
        List<Robot> robots = [];

        foreach (var line in File.ReadLines(FileName))
        {
            robots.Add(Robot.Parse(line));
        }

        return robots;
    }

    static List<Point> GetPositions(this List<Robot> robots, int time)
    {
        List<Point> finalPositions = [];

        foreach (Robot robot in robots)
        {
            finalPositions.Add(robot.GetPosition(time));
        }

        return finalPositions;
    }

    static int[] CountRobotsPerQuadrant(this List<Point> positions)
    {
        Dictionary<Point, int> robotsPerTile = positions.Frequency();
        var count = new int[4];

        foreach ((Point tile, int robotsCount) in robotsPerTile)
        {
            int quadrant = tile.GetQuadrant();

            if (quadrant is not -1) count[quadrant] += robotsCount;
        }

        return count;
    }

    static int GetQuadrant(this Point position) => position switch
    {
        _ when position.X < Width / 2 && position.Y < Height / 2 => 0,
        _ when position.X > Width / 2 && position.Y < Height / 2 => 1,
        _ when position.X < Width / 2 && position.Y > Height / 2 => 2,
        _ when position.X > Width / 2 && position.Y > Height / 2 => 3,
        _ => -1
    };

    static int CalculateSecurityFactor(this int[] robotsPerQuadrant) =>
        robotsPerQuadrant.Aggregate(1, (a, b) => a * b, res => res);

    static int CalculateTimeForTree(this List<Robot> robots)
    {
        HashSet<Point> uniquePositions;
        List<Point> positions;
        var time = 0;

        // Idk why this works, but apparently when
        // there is at most 1 robot per tile
        // we can see the tree
        do
        {
            positions = robots.GetPositions(++time);
            uniquePositions = [.. positions];
        } while (positions.Count != uniquePositions.Count);

        // Print the tree :D
        // for(var i = 0; i < Height; i++)
        // {
        //     for(var j = 0; j < Width; j++)
        //     {
        //         if(uniquePositions.Contains(new(j, i))) Console.Write('*');
        //         else Console.Write(' ');
        //     }
        //     Console.WriteLine();
        // }        

        return time;
    }

    record Robot(Point Position, Point Velocity)
    {
        public static Robot Parse(string rawData)
        {
            string[] robotData = rawData.Split(' ');
            string[] positionData = robotData[0].Split(',');
            string[] velocityData = robotData[1].Split(',');

            Point position = new(int.Parse(positionData[0][2..]), int.Parse(positionData[1]));
            Point velocity = new(int.Parse(velocityData[0][2..]), int.Parse(velocityData[1]));

            return new(position, velocity);
        }

        public Point GetPosition(int time)
        {
            int x = (Position.X + Velocity.X * time) % Width;
            int y = (Position.Y + Velocity.Y * time) % Height;

            if (x < 0) x = Width + x;
            if (y < 0) y = Height + y;

            return new(x, y);
        }
    }

    public static Dictionary<Point, int> Frequency(this List<Point> points)
    {
        Dictionary<Point, int> count = [];

        foreach(Point point in points)
        {
            count.TryAdd(point, 0);
            count[point]++;
        }

        return count;
    }
}
