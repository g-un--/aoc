namespace _2021;

using System.Numerics;
using static Utils;

public class Day6
{
    public static BigInteger GetPopulationAfter(int days, List<int> fishesAge) 
    {
        var countByDay = new BigInteger[9];
        foreach (var group in fishesAge.GroupBy(day => day))
        {
            countByDay[group.Key] = group.Count();
        }

        for (var day = 1; day <= days; day++)
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
        return sum;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day6));
        var fishesAge = input[0].ToIntList(",");
        var populationAfter80Days = GetPopulationAfter(80, fishesAge);
        Assert.Equal(386536, populationAfter80Days);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day6));
        var fishesAge = input[0].ToIntList(",");
        var populationAfter256Days = GetPopulationAfter(256, fishesAge);
        Assert.Equal(1732821262171, populationAfter256Days);
    }
}