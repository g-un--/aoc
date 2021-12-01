namespace _2021;
using static Utils;

public class Day1
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x)).ToArray();
        var result = 0;
        for (var index = 1; index < numbers.Length; index++)
        {
            if (numbers[index] > numbers[index - 1])
            {
                result += 1;
            }
        }
        Console.WriteLine($"Part1: {result}");
        Assert.Equal(1583, result);
    }

     [Fact]
    public async Task Part1Method2()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x));
        var result = 0;
        var previousSum = int.MaxValue;
        foreach(var window in numbers.SlidingWindow(1)) {
            var currentSum = window.Sum();
            if (previousSum < currentSum) {
                result += 1;
            }
            previousSum = currentSum;
        }
        Console.WriteLine($"Part1 Method2: {result}");
        Assert.Equal(1583, result);
    }


    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x)).ToArray();
        var result = 0;

        for (var index = 3; index < numbers.Length; index++)
        {
            if (numbers[index - 3] < numbers[index])
            {
                result += 1;
            }
        }
        Console.WriteLine($"Part2: {result}");
        Assert.Equal(1627, result);
    }

    [Fact]
    public async Task Part2Method2()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x));
        var result = 0;
        var previousSum = int.MaxValue;
        foreach(var window in numbers.SlidingWindow(3)) {
            var currentSum = window.Sum();
            if (previousSum < currentSum) {
                result += 1;
            }
            previousSum = currentSum;
        }
        Console.WriteLine($"Part2 Method2: {result}");
        Assert.Equal(1627, result);
    }
}