namespace _2021;
using static Utils;

public class Day11
{
    static int[,] GetInputArray(string[] input)
    {
        var result = new int[10, 10];

        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                result[row, column] = (int)Char.GetNumericValue(input[row][column]);
            }
        }

        return result;
    }

    static void UpdateNearbyCells(int[,] array, bool[,] arrayCheck, int row, int column)
    {
        if(arrayCheck[row, column]) {
            return;
        }

        arrayCheck[row, column] = true;

        for (var deltaRow = -1; deltaRow <= 1; deltaRow++)
        {
            for (var deltaColumn = -1; deltaColumn <= 1; deltaColumn++)
            {
                if (!(deltaRow == 0 && deltaColumn == 0))
                {
                    var newRow = row + deltaRow;
                    var newColumn = column + deltaColumn;
                    if (newRow >= 0 && newRow < 10 && newColumn >= 0 && newColumn < 10)
                    {
                        array[newRow, newColumn] += 1;
                        if (array[newRow, newColumn] > 9 && !arrayCheck[newRow, newColumn])
                        {
                            UpdateNearbyCells(array, arrayCheck, newRow, newColumn);
                        }
                    }
                }
            }
        }
    }

    static void IncrementAll(int[,] array)
    {
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                array[row, column] += 1;
            }
        }
    }

    static void CheckAll(int[,] array)
    {
        var arrayCheck = new bool[10, 10];
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                if (array[row, column] > 9)
                {
                    UpdateNearbyCells(array, arrayCheck, row, column);
                }
            }
        }
    }

    static int FlashAll(int[,] array)
    {
        var result = 0;
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                if (array[row, column] > 9)
                {
                    result += 1;
                    array[row, column] = 0;
                }
            }
        }
        return result;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day11));
        var array = GetInputArray(input);
        var sum = 0;

        for (var step = 1; step <= 100; step++)
        {
            IncrementAll(array);
            CheckAll(array);
            sum += FlashAll(array);
        }

        Assert.Equal(1719, sum);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day11));
        var array = GetInputArray(input);
        var step = 0;
        var result = 0;

        while(result != 100)
        {
            step += 1;
            IncrementAll(array);
            CheckAll(array);
            result = FlashAll(array);
        }

        Assert.Equal(232, step);
    }
}