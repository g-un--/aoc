namespace _2021;
using static Utils;

public class Day13
{
    HashSet<(int, int)> FoldLeft(IEnumerable<(int, int)> points, int column)
    {
        var result = new HashSet<(int, int)>();

        foreach (var point in points)
        {
            var (x, y) = point;
            if (x < column)
            {
                result.Add(point);
            }
            else if (x > column)
            {
                result.Add((Math.Abs(x - 2 * column), y));
            }
        }

        return result;
    }

    HashSet<(int, int)> FoldUp(IEnumerable<(int, int)> points, int row)
    {
        var result = new HashSet<(int, int)>();

        foreach (var point in points)
        {
            var (x, y) = point;
            if (y < row)
            {
                result.Add(point);
            }
            else if (y > row)
            {
                result.Add((x, Math.Abs(y - 2 * row)));
            }
        }

        return result;
    }

    static async Task<(List<(int, int)> points, List<(string, int)> folds)> GetPointsAndFolds()
    {
        var input = await ReadInputLines(nameof(Day13));
        var points = new List<(int, int)>();
        var folds = new List<(string, int)>();

        foreach (var line in input)
        {
            if (line.Contains(","))
            {
                var linePoints = line.SplitBy(",").Select(int.Parse).ToArray();
                points.Add((linePoints[0], linePoints[1]));
            }
            else if (line.Contains("fold"))
            {
                var instructionParts = line.SplitBy(" ");
                var foldLine = instructionParts[2].SplitBy("=");
                folds.Add((foldLine[0], int.Parse(foldLine[1])));
            }
        }

        return (points, folds);
    }

    [Fact]
    public async Task Part1()
    {
        var (points, folds) = await GetPointsAndFolds();

        var (direction, rowOrColumn) = folds[0];
        var pointsCount = 0;
        if (direction == "x")
        {
            var result = FoldLeft(points, rowOrColumn);
            pointsCount = result.Count;
        }
        else if (direction == "y")
        {
            var result = FoldUp(points, rowOrColumn);
            pointsCount = result.Count;
        }

        Assert.Equal(837, pointsCount);
    }

    [Fact]
    public async Task Part2()
    {
        var (points, folds) = await GetPointsAndFolds();

        HashSet<(int, int)> result = new HashSet<(int, int)>(points);
        foreach (var fold in folds)
        {
            var (direction, rowOrColumn) = fold;
            if (direction == "x")
            {
                result = FoldLeft(result, rowOrColumn);

            }
            else if (direction == "y")
            {
                result = FoldUp(result, rowOrColumn);
            }
        }
        var maxY = result.Select(point => point.Item2).Max();
        var maxX = result.Select(point => point.Item1).Max();
        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                if (result.Contains((x, y)))
                {
                    Console.Write("##");
                }
                else
                {
                    Console.Write("..");
                }
            }
            Console.WriteLine();
        }
    }
}