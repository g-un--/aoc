namespace _2021;
using static Utils;

public class Day3
{
    [Fact]
    public async Task Part1()
    {
        List<(int, int)> getStats(string[] lines)
        {
            var columnStats = new List<(int, int)>();

            foreach (var line in lines)
            {
                var column = 0;
                foreach (var digit in line)
                {
                    if (columnStats.Count <= column)
                    {
                        columnStats.Add((0, 0));
                    }
                    var (ones, zeros) = columnStats[column];
                    if (digit == '1')
                    {
                        columnStats[column] = (ones + 1, zeros);
                    }
                    else
                    {
                        columnStats[column] = (ones, zeros + 1);
                    }
                    column += 1;
                }
            }

            return columnStats;
        }

        var content = await ReadInputLines(nameof(Day3));
        var columnStats = getStats(content);

        var gamma = 0;
        var epsilon = 0;
        for (var index = 0; index < columnStats.Count; index++)
        {
            var (ones, zeros) = columnStats[index];
            gamma += (ones > zeros ? 1 : 0) * (1 << (columnStats.Count - index - 1));
            epsilon += (ones < zeros ? 1 : 0) * (1 << (columnStats.Count - index - 1));
        }

        Assert.Equal(2003336, gamma * epsilon);
    }

    [Fact]
    public async Task Part2()
    {
        string filterLines(string[] lines, int position, bool mostCommon)
        {
            if (lines.Length == 1)
            {
                return lines[0];
            }

            var groupsAtPosition = lines.GroupBy(line => line[position]).ToDictionary(group => group.Key);
            var ones = groupsAtPosition['1'].ToArray();
            var zeros = groupsAtPosition['0'].ToArray();

            if (mostCommon)
            {
                return filterLines(ones.Length >= zeros.Length ? ones : zeros, position + 1, mostCommon);
            }
            else
            {
                return filterLines(zeros.Length <= ones.Length ? zeros : ones, position + 1, mostCommon);
            }
        }

        var content = await ReadInputLines(nameof(Day3));
        var o2Rating = Convert.ToInt32(filterLines(content, 0, true), 2);
        var co2Rating = Convert.ToInt32(filterLines(content, 0, false), 2);
        Assert.Equal(1877139, o2Rating * co2Rating);
    }
}