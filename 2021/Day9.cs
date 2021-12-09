namespace _2021;
using static Utils;

public class Day9
{
    static bool IsLowest(int[,] matrix, int line, int column)
    {
        var top = line - 1;
        if (top >= 0 && matrix[top, column] <= matrix[line, column])
        {
            return false;
        }
        var left = column - 1;
        if (left >= 0 && matrix[line, left] <= matrix[line, column])
        {
            return false;
        }
        var right = column + 1;
        if (right < 100 && matrix[line, right] <= matrix[line, column])
        {
            return false;
        }
        var bottom = line + 1;
        if (bottom < 100 && matrix[bottom, column] <= matrix[line, column])
        {
            return false;
        }
        return true;
    }

    void ForEachLowestPoint(int[,] matrix, Action<int, int> pointHandler)
    {
        for (var lineIndex = 0; lineIndex < 100; lineIndex++)
        {
            for (var columnIndex = 0; columnIndex < 100; columnIndex++)
            {
                if (IsLowest(matrix, lineIndex, columnIndex))
                {
                    pointHandler(lineIndex, columnIndex);
                }
            }
        }
    }

    static int FindBasinSize(int[,] matrix, Queue<(int, int)> toCheck, HashSet<(int, int)> basin)
    {
        if (toCheck.Count == 0)
        {
            return basin.Count;
        }

        var (line, column) = toCheck.Dequeue();

        var top = line - 1;
        if (top >= 0 && matrix[top, column] < 9 && !basin.Contains((top, column)))
        {
            toCheck.Enqueue((top, column));
            basin.Add((top, column));
        }

        var left = column - 1;
        if (left >= 0 && matrix[line, left] < 9 && !basin.Contains((line, left)))
        {
            toCheck.Enqueue((line, left));
            basin.Add((line, left));
        }

        var right = column + 1;
        if (right < 100 && matrix[line, right] < 9 && !basin.Contains((line, right)))
        {
            toCheck.Enqueue((line, right));
            basin.Add((line, right));
        }

        var bottom = line + 1;
        if (bottom < 100 && matrix[bottom, column] < 9 && !basin.Contains((bottom, column)))
        {
            toCheck.Enqueue((bottom, column));
            basin.Add((bottom, column));
        }

        return FindBasinSize(matrix, toCheck, basin);
    }

    public static int[,] GetMatrix(string[] input)
    {
        var matrix = new int[100, 100];
        for (var lineIndex = 0; lineIndex < 100; lineIndex++)
        {
            for (var columnIndex = 0; columnIndex < 100; columnIndex++)
            {
                matrix[lineIndex, columnIndex] = (int)Char.GetNumericValue(input[lineIndex][columnIndex]);
            }
        }
        return matrix;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day9));
        var matrix = GetMatrix(input);
        var sum = 0;
        ForEachLowestPoint(matrix, (lineIndex, columnIndex) =>
        {
            sum += matrix[lineIndex, columnIndex] + 1;
        });
        Assert.Equal(585, sum);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day9));
        var matrix = GetMatrix(input);
        var basinSizes = new List<int>();
        ForEachLowestPoint(matrix, (lineIndex, columnIndex) =>
        {
            var queue = new Queue<(int, int)>();
            var basin = new HashSet<(int, int)>();
            queue.Enqueue((lineIndex, columnIndex));
            basin.Add((lineIndex, columnIndex));
            var size = FindBasinSize(matrix, queue, basin);
            basinSizes.Add(size);

        });
        var orderedSizes = basinSizes.OrderByDescending(x => x).ToArray();
        var result = orderedSizes[0] * orderedSizes[1] * orderedSizes[2];
        Assert.Equal(827904, result);
    }
}