using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day23
{
    const string Filename = "Puzzles/23/input.txt";

    public static void Run()
    {
        Dictionary<string, List<string>> graph = ReadConnections();

        List<List<string>> triplets = graph.FindTriplets();
        List<string> longestSet = graph.FindLongestSet();

        int part1 = triplets.Count(t => t.Any(s => s.StartsWith('t')));
        string part2 = longestSet.Order().Join(',');

        Console.WriteLine("** Day 23 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static Dictionary<string, List<string>> ReadConnections()
    {
        Dictionary<string, List<string>> graph = [];

        foreach (string line in File.ReadLines(Filename))
        {
            string[] nodes = line.Split('-');

            graph.TryAdd(nodes[0], []);
            graph.TryAdd(nodes[1], []);

            graph[nodes[0]].Add(nodes[1]);
            graph[nodes[1]].Add(nodes[0]);
        }

        return graph;
    }

    static List<string> FindLongestSet(this Dictionary<string, List<string>> graph)
    {
        List<string> longest = [];
        HashSet<string> seen = [];

        foreach((string nodeA, List<string> connections) in graph)
        {
            for(var i = 0; i < connections.Count - 1; i++)
            {
                string nodeB = connections[i];
                HashSet<string> current = [nodeA, nodeB];

                if(seen.Contains(nodeB)) continue;

                for(var j = i + 1; j < connections.Count; j++)
                {
                    string nodeC = connections[j];

                    if(seen.Contains(nodeC)) continue;
                    // All nodes in current set are connected to nodeC
                    if(current.IsSubsetOf(graph[nodeC])) current.Add(nodeC);
                }

                if(current.Count > longest.Count) longest = [..current];
            }

            seen.Add(nodeA);
        }

        return longest;
    }

    static List<List<string>> FindTriplets(this Dictionary<string, List<string>> graph)
    {
        List<List<string>> triplets = [];
        HashSet<string> seen = [];

        foreach((string nodeA, List<string> connections) in graph)
        {
            foreach((string nodeB, string nodeC) in connections.Pairs())
            {
                if(seen.Contains(nodeB) || seen.Contains(nodeC)) continue;
                if(graph[nodeB].Contains(nodeC)) triplets.Add([nodeA, nodeB, nodeC]);
            }
            
            seen.Add(nodeA);
        }

        return triplets;
    }
}
