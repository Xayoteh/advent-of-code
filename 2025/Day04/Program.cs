/*
Solution to Advent of Code 2025 - Day 4
https://adventofcode.com/2025/day/4
*/

const string INPUT_PATH = "../input.txt";

char[][] diagram = ReadInput(INPUT_PATH);
List<(int, int)> accessibleRolls = GetAccessibleRolls(diagram);
List<(int, int)> totalAccessibleRolls = GetTotalAccessibleRolls(diagram);

Console.WriteLine("** Day 4 **");
Console.WriteLine($"Part 1: {accessibleRolls.Count}");
Console.WriteLine($"Part 2: {totalAccessibleRolls.Count}");

List<(int, int)> GetTotalAccessibleRolls(char[][] diagram)
{
    List<(int, int)> totalAccessibleRolls = [];

    while(GetAccessibleRolls(diagram) is List<(int, int)> accessibleRolls && accessibleRolls.Count > 0)
    {
        foreach((int r, int c) in accessibleRolls)
            diagram[r][c] = '.';
        
        totalAccessibleRolls.AddRange(accessibleRolls);
    }

    return totalAccessibleRolls;
}

bool IsInBounds(char[][] diagram, int r, int c) =>
    r >= 0 && r < diagram.Length && c >= 0 && c < diagram[r].Length;

bool IsAccessible(char[][] diagram, int r, int c)
{
    int count = 0;
    for(int dr = -1; dr <= 1; dr++)
        for(int dc = -1; dc <= 1; dc++)
            if(IsInBounds(diagram, r + dr, c + dc) && diagram[r + dr][c + dc] == '@')
                count++;

    return count <= 4;
}

List<(int, int)> GetAccessibleRolls(char[][] diagram)
{
    List<(int, int)> accessibleRolls = [];
    for(int r = 0; r < diagram.Length; r++)
        for(int c = 0; c < diagram[r].Length; c++)
            if(diagram[r][c] == '@' && IsAccessible(diagram, r, c))
                accessibleRolls.Add((r, c));

    return accessibleRolls;
}

char[][] ReadInput(string path) =>
    File.ReadLines(path).Select(x => x.ToCharArray()).ToArray();