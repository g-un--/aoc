namespace _2021;

using System.Text;
using static Utils;

public class Day17
{
    public record Area(int minX, int maxX, int minY, int maxY);

    public static bool IsInArea(Area area, int x, int y) =>
        area.minX <= x && x <= area.maxX && area.minY <= y && y <= area.maxY;

    static async Task<Area> GetInputArea() 
    {
        var input = await ReadInputLines(nameof(Day17));
        var parts = input[0].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var getCoordinates = (string start) => {
            var array = parts.First(x => x.StartsWith(start)).Remove(0, 2).SplitBy("..").Select(int.Parse).ToArray();
            return (array[0], array[1]);
        };
        var (minX, maxX) = getCoordinates("x=");
        var (minY, maxY) = getCoordinates("y=");
        return new Area(minX, maxX, minY, maxY);
    }

    [Fact]
    public async Task Part1()
    {
        var area = await GetInputArea();
        var maxYVelocity = Math.Abs(area.minY) - 1;
        var maxHeight = (maxYVelocity * (maxYVelocity + 1)) / 2;
        Assert.Equal(8256, maxHeight);
    }

    [Fact]
    public async Task Part2()
    {
        var area = await GetInputArea();
        var minYVelocity = area.minY;
        var maxYVelocity = Math.Abs(area.minY) - 1;
        var minXVelocity = (int)Math.Sqrt(area.minX);
        var maxXVelocity = area.maxX;
        var maxSteps = 2 * Math.Abs(area.minY);

        var distinctVelocities = 0;
        for(var dx = minXVelocity; dx <= maxXVelocity; dx++) 
        {
            for (var dy = minYVelocity; dy <= maxYVelocity; dy++) 
            {
                var xVelocity = dx;
                var yVelocity = dy;
                var currentX = 0;
                var currentY = 0;
                
                for (var step = 1; step <= maxSteps; step++) 
                {
                    currentX += xVelocity;
                    currentY += yVelocity;
                    if (IsInArea(area, currentX, currentY)) {
                        distinctVelocities += 1;
                        break;
                    }
                    if (xVelocity > 0) xVelocity -= 1;
                    yVelocity -= 1;
                }
            }
        }
        Assert.Equal(2326, distinctVelocities);
    }
}