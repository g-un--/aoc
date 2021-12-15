namespace _2021;
using static Utils;

public class Day15
{
    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day15));
        var rows = input.Length;
        var columns = input[0].Length;
        var array = GetInputArray(input, rows, columns);
        var costs = Dijkstra(array, (0, 0));
        var bottomRightCost = costs[rows - 1,columns - 1];
        Assert.Equal(553, bottomRightCost);

    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day15));
        var rows = input.Length;
        var columns = input[0].Length;
        var array = GetInputArray(input, rows, columns);
        var resizedArray = DuplicateArray(array, 5);
        var costs = Dijkstra(resizedArray, (0, 0));
        var bottomRightCost = costs[5 * rows - 1, 5 * columns - 1];
        Assert.Equal(2858, bottomRightCost);
    }

    static IEnumerable<(int, int)> GetNeighbors((int, int) point)
    {
        var (row, column) = point;
        yield return (row + 1, column);
        yield return (row, column + 1);
        yield return (row - 1, column);
        yield return (row, column - 1);
    }

    static bool IsValid((int, int) point, int rows, int columns)
    {
        var (row, column) = point;
        return row >= 0 && row < rows && column >= 0 && column < columns;
    }

    static void EnqueueNeighbors(Queue<(int, int)> queue, int[,] costs, (int, int) position) 
    {
        foreach (var neighbor in GetNeighbors(position))
        {
            var (row, column) = neighbor;
            if (IsValid(neighbor, costs.GetLength(0), costs.GetLength(1))) 
            {
                queue.Enqueue(neighbor);
            }
        }
    }

    static int[,] Dijkstra(int[,] array, (int, int) start)
    {
        var rows = array.GetLength(0);
        var columns = array.GetLength(1);
        int[,] costs = new int[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                costs[row, column] = int.MaxValue;
            }
        }
        var (startRow, startColumn) = start;
        costs[startRow, startColumn] = 0;

        var queue = new Queue<(int, int)>();
        EnqueueNeighbors(queue, costs, start);

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            var (row, column) = next;
            if (IsValid(next, rows, columns))
            {
                var min = int.MaxValue;
                foreach (var neighbor in GetNeighbors(next))
                {
                    if (!IsValid(neighbor, rows, columns))
                    {
                        continue;
                    }

                    var (neighborRow, neighborColumn) = neighbor;
                    var neighborCost = costs[neighborRow, neighborColumn];
                    if (neighborCost < int.MaxValue)
                    {
                        var cost = neighborCost + array[row, column];
                        min = cost < min ? cost : min;
                    }
                }
                if (costs[row, column] > min) {
                    costs[row, column] = min;
                    EnqueueNeighbors(queue, costs, next);
                }
            }
        }

        return costs;
    }

    static int[,] DuplicateArray(int[,] array, int times) 
    {
        var rows = array.GetLength(0);
        var columns = array.GetLength(1);
        var result = new int[times*rows, times*columns];

        for(var rowIndex = 0; rowIndex < times; rowIndex++)
        {
            for(var columnIndex = 0; columnIndex < times; columnIndex++) 
            {
                for(var row = 0; row < rows; row++) 
                {
                    for (var column = 0; column < columns; column++) 
                    {
                        var value = array[row, column] + rowIndex + columnIndex;
                        while (value >= 10) 
                            value = value - 9;
                        result[rowIndex * rows + row, columnIndex * columns + column] = value;
                    }
                }
            }
        }

        return result;
    }
}