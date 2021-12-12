namespace _2021;

using System.Collections.Immutable;
using static Utils;

public class Day12
{
    [Fact]
    public async Task Part1()
    {
        var caves = await GetMap();
        var allPaths = new HashSet<string>();
        var visited = ImmutableDictionary<string, int>.Empty.Add("start", 1);
        var isValid = (ImmutableDictionary<string, int> visited) => visited.All(v => v.Value <= 1);
        var currentPath = ImmutableList<string>.Empty;

        Visit(caves, allPaths, isValid, visited, currentPath, "start");

        Assert.Equal(3510, allPaths.Count);
    }

    [Fact]
    public async Task Part2()
    {
        var caves = await GetMap();
        var allPaths = new HashSet<string>();
        var isValid = (ImmutableDictionary<string, int> visited) => 
            visited.All(v => v.Value <= 2) && 
            visited.Count(v => v.Value == 2) <= 1;
        var visited = ImmutableDictionary<string, int>.Empty.Add("start", 1);
        var currentPath = ImmutableList<string>.Empty;

        Visit(caves, allPaths, isValid, visited, currentPath, "start");

        Assert.Equal(122880, allPaths.Count);
    }

    static void AddToMap(Dictionary<string, List<string>> map, string start, string end)
    {
        if (!map.TryGetValue(start, out var neighbors))
        {
            neighbors = new List<string>();
            neighbors.Add(end);
            map.Add(start, neighbors);
        }
        else
        {
            neighbors.Add(end);
        }
    }

    static async Task<Dictionary<string, List<string>>> GetMap()
    {
        var input = await ReadInputLines(nameof(Day12));
        var map = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var caves = line.SplitBy("-");
            AddToMap(map, caves[0], caves[1]);
            AddToMap(map, caves[1], caves[0]);
        }

        return map;
    }

    static void Visit(
        Dictionary<string, List<string>> caves,
        HashSet<string> allPaths,
        Func<ImmutableDictionary<string, int>, bool> isValid,
        ImmutableDictionary<string, int> visited,
        ImmutableList<string> currentPath,
        string start)
    {
        var pathSoFar = currentPath.Add(start);

        if (start == "end")
        {
            allPaths.Add(string.Join(',', pathSoFar));
            return;
        }

        if (!caves.TryGetValue(start, out var neighbors))
        {
            return;
        }

        foreach (var neighbor in neighbors)
        {
            if (Char.IsLower(neighbor[0]) && neighbor != "start")
            {
                visited.TryGetValue(neighbor, out var neighborVisitCount);
                neighborVisitCount += 1;
                var newVisitCount = visited.SetItem(neighbor, neighborVisitCount);
                if(isValid(newVisitCount)) {
                    Visit(caves, allPaths, isValid, newVisitCount, pathSoFar, neighbor);
                }
            }
            else if (Char.IsUpper(neighbor[0]))
            {
                Visit(caves, allPaths, isValid, visited, pathSoFar, neighbor);
            }
        }
    }
}