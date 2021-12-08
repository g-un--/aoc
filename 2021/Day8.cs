namespace _2021;
using static Utils;

public class Day8
{
    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day8));
        var sum = 0;
        foreach (var line in input)
        {
            var output = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1];
            var digits = output.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var digits1478Lenghts = new int[] { 2, 4, 3, 7 };
            sum += digits.Count(digit => digits1478Lenghts.Contains(digit.Length));
        }
        Assert.Equal(390, sum);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day8));

        var sum = 0;
        foreach (var line in input)
        {
            var output = line.SplitBy("|");
            var signals = output[0].SplitBy(" ");
            var digits = output[1].SplitBy(" ");

            var map = new Dictionary<int, string>();
            map[1] = signals.First(digit => digit.Length == 2);
            map[7] = signals.First(digit => digit.Length == 3);
            map[4] = signals.First(digit => digit.Length == 4);
            map[8] = signals.First(digit => digit.Length == 7);

            var rightVerticalChars = new HashSet<char>(map[1]);
            var fiveCharsDigits = signals.Where(digit => digit.Length == 5).ToArray();
            var horizontalChars = new HashSet<char>(fiveCharsDigits[0])
                .IntersectClone(fiveCharsDigits[1])
                .IntersectClone(fiveCharsDigits[2]);

            var topHorizonalChar = map[7].First(digitChar => horizontalChars.Contains(digitChar));
            var middleHorizontalChar = map[4].First(digitChar => horizontalChars.Contains(digitChar));
            var bottomHorizontalChar = horizontalChars.DiffClone(new char[] {
                    topHorizonalChar,
                    middleHorizontalChar
                }).First();

            var eightChars = new HashSet<char>(map[8]);
            var leftVerticalChars = eightChars.DiffClone(rightVerticalChars).DiffClone(horizontalChars);
            var threeChars = horizontalChars.UnionClone(rightVerticalChars);

            map[3] = signals.First(digit => 
                digit.Length == 5 &&
                digit.All(threeChar => threeChars.Contains(threeChar)));

            var nineChars = new HashSet<char>(map[3]).UnionClone(map[4]);

            map[9] = signals.First(digit => 
                digit.Length == 6 &&
                digit.All(nineChar => nineChars.Contains(nineChar)));

            var topLeftVerticalChar = nineChars.DiffClone(map[3]).First();
            var bottomLeftVerticalChar = leftVerticalChars.DiffClone(new [] { topLeftVerticalChar }).First();

            map[6] = signals.First(digit => 
                digit.Length == 6 &&
                leftVerticalChars.All(verticalChar => digit.Contains(verticalChar)) &&
                digit.Contains(middleHorizontalChar));

            var bottomRightVerticalChar = rightVerticalChars.IntersectClone(map[6]).First();
            var topRightVerticalChar = rightVerticalChars.DiffClone(new [] { bottomRightVerticalChar }).First();

            map[0] = signals.First(digit => 
                digit.Length == 6 &&
                !digit.Contains(middleHorizontalChar));

            map[2] = signals.First(digit => 
                digit.Length == 5 &&
                digit.Contains(topRightVerticalChar) &&
                digit.Contains(bottomLeftVerticalChar));

            map[5] = signals.First(digit => 
                digit.Length == 5 &&
                digit.Contains(topLeftVerticalChar) &&
                digit.Contains(bottomRightVerticalChar));

            var solution = map.ToDictionary(kvp =>
            {
                var sortedChars = kvp.Value.ToArray();
                Array.Sort(sortedChars);
                return new String(sortedChars);
            }, kvp => kvp.Key);

            var decoded = digits.Select(digit =>
            {
                var digitChars = digit.ToArray();
                Array.Sort(digitChars);
                return solution[new String(digitChars)];
            }).ToArray();

            sum += (decoded[0] * 1000 + decoded[1] * 100 + decoded[2] * 10 + decoded[3]);
        }

        Assert.Equal(1011785, sum);
    }
}