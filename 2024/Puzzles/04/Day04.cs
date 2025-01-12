using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day04
{
    const string Filename = "Puzzles/04/input.txt";

    public static void Run()
    {
        List<List<char>> wordSearch = ReadWordSearch();
        
        int part1 = wordSearch.CountXmas();
        int part2 = wordSearch.CountCrossMas();

        Console.WriteLine("** Day 4 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static int CountXmas(this List<List<char>> wordSearch)
    {
        var count = 0;

        for (var y = 0; y < wordSearch.Count; y++)
        {
            for (var x = 0; x < wordSearch[y].Count; x++)
            {
                if (wordSearch[y][x] is 'X') count += wordSearch.CountXmas(new(x, y));
            }
        }

        return count;
    }

    static int CountCrossMas(this List<List<char>> wordSearch)
    {
        var count = 0;

        for (var y = 0; y < wordSearch.Count; y++)
        {
            for (var x = 0; x < wordSearch[y].Count; x++)
            {
                if (wordSearch[y][x] is 'A' && wordSearch.IsCross(new(x, y))) count++;
            }
        }

        return count;
    }

    static int CountXmas(this List<List<char>> wordSearch, Point xPosition)
    {
        var count = 0;

        foreach (Point direction in Directions.All)
        {
            Point position = xPosition;
            var isXmas = true;

            foreach (char c in "XMAS")
            {
                isXmas = wordSearch.EqualsAt(position, c);

                if(!isXmas) break;

                position += direction;
            }

            if (isXmas) count++;
        }

        return count;
    }

    private static bool IsCross(this List<List<char>> wordSearch, Point aPosition)
    {
        var mCount = 0;
        var sCount = 0;

        foreach(Point direction in Directions.DiagonalsOnly)
        {
            Point current = aPosition + direction;

            if(wordSearch.EqualsAt(current, 'M')) mCount++;
            else if(wordSearch.EqualsAt(current, 'S')) sCount++;
            else return false;
        }

        return mCount == sCount && 
            wordSearch.At(aPosition + Directions.TopLeft) != wordSearch.At(aPosition + Directions.BottomRight) &&
            wordSearch.At(aPosition + Directions.TopRight) != wordSearch.At(aPosition + Directions.BottomLeft);
    }

    static List<List<char>> ReadWordSearch()
    {
        List<List<char>> wordSearch = [];

        foreach(string line in File.ReadLines(Filename))
        {
            wordSearch.Add([..line]);
        }

        return wordSearch;
    }
        

}
