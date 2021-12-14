namespace _2021;

using System.Numerics;
using static Utils;

public class Day14
{
    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day14));
        var pairs = GetStartPairs(input[0]);
        var insertionRules = GetInsertionRules(input.Skip(2));

        for (var step = 1; step <= 10; step++)
        {
            pairs = Evolve(pairs, insertionRules);
        }

        var result = GetDiffBetweenMostAndLeastCommonChars(pairs);
        Assert.Equal(2703, result);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day14));
        var pairs = GetStartPairs(input[0]);
        var insertionRules = GetInsertionRules(input.Skip(2));

        for (var step = 1; step <= 40; step++)
        {
            pairs = Evolve(pairs, insertionRules);
        }

        var result = GetDiffBetweenMostAndLeastCommonChars(pairs);
        Assert.Equal(2984946368465, result);
    }

    static BigInteger GetDiffBetweenMostAndLeastCommonChars(Dictionary<(char, char), BigInteger> pairs)
    {
        var charGroups = pairs.Select(p => (p.Key.Item1, p.Value))
            .Concat(pairs.Select(p => (p.Key.Item2, p.Value)))
            .GroupBy(p => p.Item1)
            .ToDictionary(
                g => g.Key,
                g => g.Aggregate(BigInteger.Zero, (sum, c) => sum + c.Value));

        var max = charGroups.Values.Max();
        var min = charGroups.Values.Min();
        var diff = max - min;
        return BigInteger.Divide(diff, 2) + (diff.IsEven ? 0 : 1);
    }

    static Dictionary<(char, char), BigInteger> Evolve(
        Dictionary<(char, char), BigInteger> pairs,
        Dictionary<(char, char), char> insertionRules)
    {
        var result = new Dictionary<(char, char), BigInteger>(pairs);
        var list = pairs.Keys.ToList();
        foreach (var pair in list)
        {
            if (insertionRules.TryGetValue(pair, out var m))
            {
                var (a, b) = pair;
                var abCount = pairs[pair];
                result[pair] -= abCount;
                if (result[pair].IsZero)
                {
                    result.Remove(pair);
                }
                result.TryGetValue((a, m), out var amCount);
                result.TryGetValue((m, b), out var mbCount);
                result[(a, m)] = abCount + amCount;
                result[(m, b)] = abCount + mbCount;
            }
        }
        return result;
    }

    static Dictionary<(char, char), BigInteger> GetStartPairs(string start)
    {
        var pairs = new Dictionary<(char, char), BigInteger>();
        for (var index = 0; index < start.Length; index++)
        {
            if (index + 1 < start.Length)
            {
                if (!pairs.TryGetValue((start[index], start[index + 1]), out var count))
                {
                    pairs.Add((start[index], start[index + 1]), BigInteger.One);
                }
                else
                {
                    pairs[(start[index], start[index + 1])] = count + 1;
                }
            }
        }
        return pairs;
    }

    static Dictionary<(char, char), char> GetInsertionRules(IEnumerable<string> rules)
    {
        var insertionRules = new Dictionary<(char, char), char>();
        foreach (var line in rules)
        {
            var parts = line.SplitBy("->");
            var pair = parts[0].Trim();
            var toInsert = parts[1].Trim();
            insertionRules.Add((pair[0], pair[1]), toInsert[0]);
        }
        return insertionRules;
    }
}