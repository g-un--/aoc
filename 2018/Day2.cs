namespace _2018;
using static Utils;

public class Day2
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day2));
        var countTimes = (string[] input, int times) => input.Count(x => x.GroupBy(x => x).Any(g => g.Count() == times));
        var twoTimes = countTimes(content, 2);
        var threeTimes = countTimes(content, 3);

        Assert.Equal(6972, twoTimes * threeTimes);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day2));

        (bool result, string common) MatchItems(string item1, string item2)
        {
            if (item1.Length != item2.Length)
            {
                return (false, string.Empty);
            }

            var diffs = item1.Select((itemChar, index) => itemChar == item2[index]).ToArray();
            if (diffs.Count(x => x == false) == 1)
            {
                return (true, new string(item1.Where((_, index) => diffs[index]).ToArray()));
            }

            return (false, string.Empty);
        }

        var result = content
            .SelectMany(first => content.Select(second => MatchItems(first, second)))
            .First(x => x.result);

        Assert.Equal("aixwcbzrmdvpsjfgllthdyoqe", result.common);
    }
}