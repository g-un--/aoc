namespace _2021;

using System.Numerics;
using static Utils;

using Range = System.ValueTuple<int, int>;

public class Day22
{
    public record Command(string onOff, (Range, Range, Range) cube);

    [Fact]
    public async Task Part1()
    {
        var commands  = await GetInputCommands();
        var target = ((-50, 50), (-50, 50), (-50, 50));
        var targetCommands = new List<Command>();
        foreach(var command in commands) 
        {
             var targetIntersection = GetCubeIntersection(command.cube, target);
             if(targetIntersection != null)
             {
                 targetCommands.Add(new Command(command.onOff, targetIntersection.Value));
             }
        }
        var total = GetOnCount(targetCommands.ToArray());
        Assert.Equal(580012, total);
    }

    [Fact]
    public async Task Part2()
    {
        var commands  = await GetInputCommands();
        var total = GetOnCount(commands.ToArray());
        Assert.Equal(1334238660555542, total);
    }

    public BigInteger GetOnCount(Command[] commands) 
    {
        var processed = new List<Command>();
        foreach(var command in commands) 
        {
            var toAdd = new List<Command>();
            foreach(var processedCommand in processed) 
            {
                var processedIntersection = GetCubeIntersection(processedCommand.cube, command.cube);
                if (processedIntersection == null) continue;
                toAdd.Add(new Command(
                    processedCommand.onOff == "on" ? "off" : "on", 
                    processedIntersection.Value));
            }
            processed.AddRange(toAdd);

            if(command.onOff == "on")
            {
                processed.Add(command);
            }
        }
        var total = processed.Aggregate(BigInteger.Zero, (acc, value) => {
            var count = Count(value.cube);
            return value.onOff == "on" ? acc + count : acc - count; 
        });

        return total;
    }

    public static async Task<Command[]> GetInputCommands()
    {
        var input = await ReadInputLines(nameof(Day22));

        return input.Select(line =>
        {
            var command = line.SplitBy(" ");
            var onOff = command[0];
            var cube = command[1].SplitBy(",").Select(range => {
                int start =0, end = 0;
                range.MatchGroups(@".=(-?\d+)..(-?\d+)", (groups) =>
                {
                    start = int.Parse(groups[0]);
                    end = int.Parse(groups[1]);
                });
                return (start, end);
            }).ToArray();
            return new Command(onOff, (cube[0], cube[1], cube[2]));
        }).ToArray();
    }

    (Range, Range, Range)? GetCubeIntersection((Range, Range, Range) cube1, (Range, Range, Range) cube2)
    {
        var (x1range, y1range, z1range) = cube1;
        var (x2range, y2range, z2range) = cube2;

        var (isXCommon, xStart, xEnd) = GetRangeIntersection(x1range, x2range);
        var (isYCommon, yStart, yEnd) = GetRangeIntersection(y1range, y2range);
        var (isZCommon, zStart, zEnd) = GetRangeIntersection(z1range, z2range);

        if (isXCommon && isYCommon && isZCommon) 
        {
            return ((xStart, xEnd), (yStart, yEnd), (zStart, zEnd));
        }

        return null;
    }

    public static BigInteger Count((Range, Range, Range) cube) 
    {
        var ((xstart, xend), (ystart, yend), (zstart, zend)) = cube;
        return BigInteger.Multiply(BigInteger.Multiply(xend-xstart+1, yend-ystart+1), (zend-zstart+1));
    }

    (bool, int, int) GetRangeIntersection(Range range1, Range range2)
    {
        if (range1.Item2 >= range2.Item1 && range1.Item1 <= range2.Item2)
        {
            var start = Math.Max(range1.Item1, range2.Item1);
            var end = Math.Min(range1.Item2, range2.Item2);
            return (true, start, end);
        }

        return (false, 0, 0);
    }
}