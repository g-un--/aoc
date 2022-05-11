namespace _2018;

using System.Text.RegularExpressions;
using static Utils;

public class Day3
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day3));

        var result = new int[1000, 1000];

        foreach (var item in content)
        {
            var groups = Regex.Matches(item, @"\d+").Select(x => x.Captures[0].Value).Select(int.Parse).ToArray();
            var (left, top, width, height) = (groups[1], groups[2], groups[3], groups[4]);
            for (var i = left + 1; i <= left + width; i++)
            {
                for (var j = top + 1; j <= top + height; j++)
                {
                    result[i, j] += 1;
                }
            }
        }

        var count = 0;
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 1000; j++)
            {
                if (result[i, j] > 1)
                {
                    count += 1;
                }
            }
        }

        Assert.Equal(108961, count);
    }

    public record Claim(int id, int left, int top, int width, int height);

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day3));

        Claim[] claims = content.Select(item =>
        {
            var groups = Regex.Matches(item, @"\d+").Select(x => x.Captures[0].Value).Select(int.Parse).ToArray();
            return new Claim(groups[0], groups[1], groups[2], groups[3], groups[4]);
        }).ToArray();

        var claim = claims.Single(x => !claims.Any(y =>
        {
            if (x == y) return false;
            var xIntersects = x.left <= (y.left + y.width) && y.left <= (x.left + x.width);
            var yIntersects = x.top <= (y.top + y.height) && y.top <= (x.top + x.height);
            return xIntersects && yIntersects;
        }));

        Assert.Equal(681, claim.id);
    }
}