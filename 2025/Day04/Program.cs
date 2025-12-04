/*
Solution to Advent of Code 2025 - Day 4
https://adventofcode.com/2025/day/4
*/

const string INPUT_PATH = "../input.txt";

string[] diagram = ReadInput(INPUT_PATH);
int accessibleRolls = GetAccessibleRolls(diagram);

Console.WriteLine("** Day 4 **");
Console.WriteLine($"Part 1: {accessibleRolls}");

bool IsInBounds(string[] diagram, int r, int c) =>
    r >= 0 && r < diagram.Length && c >= 0 && c < diagram[r].Length;

bool IsAccessible(string[] diagram, int r, int c)
{
    int count = 0;
    for(int dr = -1; dr <= 1; dr++)
        for(int dc = -1; dc <= 1; dc++)
            if(IsInBounds(diagram, r + dr, c + dc) && diagram[r + dr][c + dc] == '@')
                count++;

    return count <= 4;
}

int GetAccessibleRolls(string[] diagram)
{
    int count = 0;
    for(int r = 0; r < diagram.Length; r++)
        for(int c = 0; c < diagram[r].Length; c++)
            if(diagram[r][c] == '@' && IsAccessible(diagram, r, c))
                count++;

    return count;
}

string[] ReadInput(string path) =>
    File.ReadLines(path).ToArray();