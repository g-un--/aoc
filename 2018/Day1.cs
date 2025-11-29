namespace _2018;
using static Utils;

public class Day1
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x));
        var result = numbers.Sum();
        Assert.Equal(406, result);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day1));
        var numbers = content.Select(x => int.Parse(x)).ToArray();
        var frequencies = new HashSet<int>();
        var current = 0;

        foreach (var value in numbers.RepeatMany())
        {
            current += value;
            if (frequencies.Contains(current))
            {
                break;
            }
            frequencies.Add(current);
        }
        Assert.Equal(312, current);
    }
}