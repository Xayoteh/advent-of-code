using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day16
{
    const string Filename = "Puzzles/16/input.txt";
    const int ForwardScoreIncrement = 1;
    const int RotateScoreIncrement = 1001;

    public static void Run()
    {
        List<List<char>> maze = ReadMaze();
        Dictionary<Point, List<int>> minimumScores = maze.GetMinimumPathScores();

        int part1 = maze.GetMinimumScoreToEnd(minimumScores);
        int part2 = maze.GetTilesInMinimumPath(part1, minimumScores).Count;

        Console.WriteLine("** Day 16 **");
        Console.WriteLine($"Part 1v2: {part1}");
        Console.WriteLine($"Part 2v2: {part2}");
    }

    static List<List<char>> ReadMaze()
    {
        List<List<char>> maze = [];

        foreach (string line in File.ReadLines(Filename))
        {
            maze.Add([.. line]);
        }

        return maze;
    }

    static Point GetStartTile(this List<List<char>> maze)
    {
        for (var y = 0; y < maze.Count; y++)
        {
            for (var x = 0; x < maze[0].Count; x++)
            {
                if (maze[y][x] is 'S') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static Point GetEndTile(this List<List<char>> maze)
    {
        for (var y = 0; y < maze.Count; y++)
        {
            for (var x = 0; x < maze[0].Count; x++)
            {
                if (maze[y][x] is 'E') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static Dictionary<Point, List<int>> GetMinimumPathScores(this List<List<char>> maze)
    {
        PriorityQueue<(Point position, Point direction, int score), int> pq = new();
        Dictionary<Point, List<int>> minimumScores = [];
        HashSet<(Point, Point)> seen = [];

        pq.Enqueue((maze.GetStartTile(), Directions.Right, 0), 0);

        while (pq.Count > 0)
        {
            (Point current, Point direction, int score) = pq.Dequeue();

            if (!minimumScores.TryAdd(current, []) && !seen.Add((current, direction))) continue;

            minimumScores[current].Add(score);
            maze.GetReachableTiles(current, direction, score).ForEach(tile => pq.Enqueue(tile, tile.score));
        }

        return minimumScores;
    }

    static List<(Point position, Point direction, int score)> GetReachableTiles(this List<List<char>> maze, Point tile, Point direction, int score)
    {
        List<(Point, Point, int)> reachableTiles = [];

        foreach (Point newDirection in GetValidDirections(direction))
        {
            Point newTile = tile + newDirection;
            int neighborScore = score + (newDirection == direction ? ForwardScoreIncrement : RotateScoreIncrement);

            if (maze.IsEmptyOrEnd(newTile)) reachableTiles.Add((newTile, newDirection, neighborScore));
        }

        return reachableTiles;
    }

    static HashSet<Point> GetTilesInMinimumPath(this List<List<char>> maze, int minimumPathScore, Dictionary<Point, List<int>> minimumScores)
    {
        Queue<(Point position, int score)> queue = [];
        HashSet<(Point tile, int)> seen = [];
        HashSet<Point> tiles = [];        

        queue.Enqueue((maze.GetEndTile(), minimumPathScore));

        while (queue.Count > 0)
        {
            (Point current, int minimumScore) = queue.Dequeue();

            if (!seen.Add((current, minimumScore))) continue;

            tiles.Add(current);
            maze.GetPreviousTilesInPath(current, minimumScore, minimumScores).ForEach(queue.Enqueue);
        }

        return tiles;
    }

    static List<(Point position, int score)> GetPreviousTilesInPath(this List<List<char>> maze, Point tile, int minimumScore, Dictionary<Point, List<int>> minimumScores)
    {
        List<(Point, int)> previousTiles = [];

        foreach (Point direction in Directions.WithoutDiagonals)
        {
            Point neighbor = tile + direction;

            if (!maze.IsEmptyOrStart(neighbor)) continue;

            foreach (int score in minimumScores[neighbor])
            {
                int scoreDifference = minimumScore - score;

                if (scoreDifference is ForwardScoreIncrement or RotateScoreIncrement)
                {
                    previousTiles.Add((neighbor, score));
                }
            }
        }

        return previousTiles;
    }

    static int GetMinimumScoreToEnd(this List<List<char>> maze, Dictionary<Point, List<int>> minimumScores) =>
        minimumScores
        .First(kvp => maze.IsEnd(kvp.Key))
        .Value.Min();

    static bool IsEmptyOrEnd(this List<List<char>> maze, Point position) =>
        maze.EqualsAt(position, ['E', '.']);

    static bool IsEmptyOrStart(this List<List<char>> maze, Point position) =>
        maze.EqualsAt(position, ['S', '.']);

    static bool IsEnd(this List<List<char>> maze, Point position) =>
        maze.EqualsAt(position, 'E');

    static IEnumerable<Point> GetValidDirections(Point direction) =>
        [Directions.Previous(direction), direction, Directions.Next(direction)];
}
