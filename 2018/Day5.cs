namespace _2018;
using static Utils;

public class Day5
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day5));
        var polymer = new List<char>(content[0]);
        Reduce(polymer);
        Assert.Equal(9526, polymer.Count);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day5));
        var polymer = content[0];
        var min = int.MaxValue;

        for (char c = 'A'; c <= 'Z'; c++)
        {
            var lowerC = Char.ToLower(c);
            var newPolymer = new List<char>(polymer);
            newPolymer.RemoveAll(x => x == c || x == lowerC);
            Reduce(newPolymer);
            if (newPolymer.Count < min)
            {
                min = newPolymer.Count;
            }
        }

        Assert.Equal(6694, min);
    }

    static void Reduce(List<char> polymer)
    {
        var start = 0;
        while (start < polymer.Count)
        {
            if (start + 1 < polymer.Count &&
               ((Char.IsUpper(polymer[start]) && Char.IsLower(polymer[start + 1]) &&
                    Char.ToLower(polymer[start]) == polymer[start + 1]) ||
                (Char.IsLower(polymer[start]) && Char.IsUpper(polymer[start + 1]) &&
                    Char.ToUpper(polymer[start]) == polymer[start + 1])))
            {
                polymer.RemoveRange(start, 2);
                start = start - 1 > 0 ? start - 1 : 0;
            }
            else
            {
                start += 1;
            }
        }
    }
}