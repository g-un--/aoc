namespace _2021;
using static Utils;

public class Day2
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day2));
        var forward = 0;
        var depth = 0;

        foreach (var line in content)
        {
            line.MatchGroup("up (\\d+)", value => depth -= int.Parse(value));
            line.MatchGroup("down (\\d+)", value => depth += int.Parse(value));
            line.MatchGroup("forward (\\d+)", value => forward += int.Parse(value));
        }

        Assert.Equal(1727835, forward * depth);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day2));
        var forward = 0;
        var depth = 0;
        var aim = 0;

        foreach (var line in content)
        {
            line.MatchGroup("up (\\d+)", value => aim -= int.Parse(value));
            line.MatchGroup("down (\\d+)", value => aim += int.Parse(value));
            line.MatchGroup("forward (\\d+)", value =>
            {
                var x = int.Parse(value);
                forward += x;
                depth += x * aim;
            });
        }

        Assert.Equal(1544000595, forward * depth);
    }
}