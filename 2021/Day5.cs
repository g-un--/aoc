namespace _2021;
using static Utils;

public class Day5
{
    static int GetIntersectionPoints(int[,] board)
    {
        var result = 0;
        for (var row = 0; row < 1000; row++)
        {
            for (var column = 0; column < 1000; column++)
            {
                if (board[row, column] >= 2)
                {
                    result += 1;
                }
            }
        }
        return result;
    }

    static void UpdateRow(int[,] board, int row, int column1, int column2)
    {
        var min = Math.Min(column1, column2);
        var max = Math.Max(column1, column2);
        for (var column = min; column <= max; column++)
        {
            board[row, column] += 1;
        }
    }

    static void UpdateColumn(int[,] board, int column, int row1, int row2)
    {
        var min = Math.Min(row1, row2);
        var max = Math.Max(row1, row2);
        for (var row = min; row <= max; row++)
        {
            board[row, column] += 1;
        }
    }

    static void UpdateDiagonal(int[,] board, int row1, int column1, int row2, int column2)
    {
        var rowIncrement = row1 - row2 > 0 ? -1 : 1;
        var columnIncrement = column1 - column2 > 0 ? -1 : 1;
        var current = (row1, column1);
        while (current != (row2, column2))
        {
            var (row, column) = current;
            board[row, column] += 1;
            current = (row + rowIncrement, column + columnIncrement);
        }
        board[row2, column2] += 1;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day5));
        var board = new int[1000, 1000];
        foreach (var line in input)
        {
            var coordinates = line.Split(new[] { " ", "->", "," }, StringSplitOptions.RemoveEmptyEntries);
            var linePoints = coordinates.Select(int.Parse).ToArray();
            if (linePoints[0] == linePoints[2])
            {
                UpdateRow(board, linePoints[0], linePoints[1], linePoints[3]);
            }
            else if (linePoints[1] == linePoints[3])
            {
                UpdateColumn(board, linePoints[1], linePoints[0], linePoints[2]);
            }
        }
        var result = GetIntersectionPoints(board);
        Assert.Equal(6710, result);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day5));
        var board = new int[1000, 1000];
        foreach (var line in input)
        {
            var coordinates = line.Split(new[] { " ", "->", "," }, StringSplitOptions.RemoveEmptyEntries);
            var linePoints = coordinates.Select(int.Parse).ToArray();
            if (linePoints[0] == linePoints[2])
            {
                UpdateRow(board, linePoints[0], linePoints[1], linePoints[3]);
            }
            else if (linePoints[1] == linePoints[3])
            {
                UpdateColumn(board, linePoints[1], linePoints[0], linePoints[2]);
            }
            else
            {
                UpdateDiagonal(board, linePoints[0], linePoints[1], linePoints[2], linePoints[3]);
            }
        }
        var result = GetIntersectionPoints(board);
        Assert.Equal(20121, result);
    }
}