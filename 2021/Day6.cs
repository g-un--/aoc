namespace _2021;

using System.Numerics;
using static Utils;

public class Day6
{
    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day6));
        var fishes = input[0].Split(",").Select(int.Parse).ToList();

        for (var day = 1; day <= 80; day++)
        {
            var newFishes = new List<int>();
            for (var fishIndex = 0; fishIndex < fishes.Count; fishIndex++)
            {
                if (fishes[fishIndex] == 0)
                {
                    fishes[fishIndex] = 6;
                    newFishes.Add(8);
                }
                else
                {
                    fishes[fishIndex] -= 1;
                }
            }
            fishes.AddRange(newFishes);
        }

        Assert.Equal(386536, fishes.Count);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day6));
        var fishes = input[0].Split(",").Select(int.Parse).ToList();

        var countByDay = new BigInteger[9];
        foreach (var group in fishes.GroupBy(day => day))
        {
            countByDay[group.Key] = group.Count();
        }

        for (var day = 1; day <= 256; day++)
        {
            var day0Count = countByDay[0];
            for (var index = 0; index < 8; index++)
            {
                countByDay[index] = countByDay[index + 1];
            }
            countByDay[8] = day0Count;
            countByDay[6] += day0Count;
        }
        
        var sum = countByDay.Aggregate(BigInteger.Zero, (sum, day) => sum + day);
        Assert.Equal(1732821262171, sum);
    }
}