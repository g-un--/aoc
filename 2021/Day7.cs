namespace _2021;
using static Utils;

public class Day7
{
    public static int GetMinCost(List<int> positions, Func<int, int, int> getCost)
    {
        positions.Sort();

        var minCost = int.MaxValue;
        for (var position = positions[0]; position < positions[positions.Count - 1]; position++)
        {
            var minAtIndex = 0;
            for (var toMoveIndex = 0; toMoveIndex < positions.Count; toMoveIndex++)
            {
                var cost = getCost(position, positions[toMoveIndex]);
                minAtIndex += cost;
                if (minAtIndex > minCost)
                {
                    break;
                }
            }
            if (minCost > minAtIndex)
            {
                minCost = minAtIndex;
            }
        }

        return minCost;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day7));
        var positions = input[0].ToIntList(",");
        var minCost = GetMinCost(positions, (start, stop) =>
        {
            return Math.Abs(start - stop);
        });
        Assert.Equal(345197, minCost);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day7));
        var positions = input[0].ToIntList(",");
        var minCost = GetMinCost(positions, (start, stop) =>
        {
            var dif = Math.Abs(start - stop);
            return (dif) * (dif + 1) / 2;
        });
        Assert.Equal(96361606, minCost);
    }
}