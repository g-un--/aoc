namespace _2021;

using static Utils;

using Point = System.ValueTuple<int, int, int>;
using ScannerInput = Dictionary<int, HashSet<(int, int, int)>>;
using Distances = Dictionary<(int, int), (int, int, int)>;

public class Day19
{
    [Fact]
    public async Task Part1And2()
    {
        var scannersInput = await GetInput();
        
        var (beaconsFound, distances, origin) = Part1(scannersInput);
        Assert.Equal(318, beaconsFound.Count);

        var maxDistance = Part2(distances, scannersInput, origin);
        Assert.Equal(12166, maxDistance);
    }

    public static (HashSet<Point>, Distances, int) Part1(ScannerInput scannersInput) 
    {
        var distances = new Distances();
        var beaconsFound = new HashSet<Point>();

       
        var origin = scannersInput.Keys.First();
        var queue = new Queue<int>();
        foreach (var scanner in scannersInput.Keys.Where(key => key != origin))
            queue.Enqueue(scanner);
        foreach (var beacon in scannersInput[origin])
            beaconsFound.Add(beacon);

        while (queue.Count > 0)
        {
            var scanner = queue.Dequeue();
            var pairFound = false;
            foreach (var pairOrientation in Orientations)
            {
                var scannerPoints = scannersInput[scanner];
                foreach (var pairPoint in scannerPoints)
                {
                    if (pairFound) break;
                    var pairPointOriented = Map(pairPoint, pairOrientation);
                    foreach (var originPoint in beaconsFound)
                    {
                        if (pairFound) break;
                        var diff = Diff(originPoint, pairPointOriented);
                        var translated = scannerPoints
                            .Select(point => Sum(diff, Map(point, pairOrientation)))
                            .ToList();
                        var commonPoints = translated.Count(x => beaconsFound.Contains(x));
                        if (commonPoints >= 12)
                        {
                            distances[(origin, scanner)] = diff;
                            foreach (var translatedPoint in translated)
                            {
                                beaconsFound.Add(translatedPoint);
                            }
                            pairFound = true;
                            break;
                        }
                    }
                }
            }

            if (!pairFound)
            {
                queue.Enqueue(scanner);
            }
        }

        return (beaconsFound, distances, origin);
    }

    public static int Part2(Distances distances, ScannerInput scannersInput, int origin)
    {
        var maxDistance = int.MinValue;
        foreach(var key1 in scannersInput.Keys)
        {
            foreach(var key2 in scannersInput.Keys) 
            {
                if (key1 == key2) continue;
                var point1 = key1 == origin ? (0, 0, 0) : distances[(origin, key1)];
                var point2 = key2 == origin ? (0, 0, 0) : distances[(origin, key2)];
                var distance = Manhattan(point1, point2);
                if (distance > maxDistance) 
                {
                    maxDistance = distance;
                }
            }
        }

        return maxDistance;
    }

    static (int, int, int) Sum(Point point1, Point point2) =>
        (point1.Item1 + point2.Item1, point1.Item2 + point2.Item2, point1.Item3 + point2.Item3);

    static (int, int, int) Diff(Point point1, Point point2) =>
       (point1.Item1 - point2.Item1, point1.Item2 - point2.Item2, point1.Item3 - point2.Item3);

    static int Manhattan(Point point1, Point point2) =>
        Math.Abs(point2.Item1 - point1.Item1) +
        Math.Abs(point2.Item2 - point1.Item2) +
        Math.Abs(point2.Item3 - point1.Item3);

    static Point Map(Point point, (int, int, int) orientation)
    {
        var (xp, yp, zp) = point;
        var (xo, yo, zo) = orientation;
        var map = new[] { xp, yp, zp };
        var xKey = Math.Abs(xo) - 1;
        var xValue = xo > 0 ? 1 : -1;

        var yKey = Math.Abs(yo) - 1;
        var yValue = yo > 0 ? 1 : -1;

        var zKey = Math.Abs(zo) - 1;
        var zValue = zo > 0 ? 1 : -1;

        return (map[xKey] * xValue, map[yKey] * yValue, map[zKey] * zValue);
    }

    static (int, int, int)[] Orientations = new [] {
        (1,  2,  3),
        (1, -3,  2),
        (1, -2, -3),
        (1,  3, -2),
        //
        (2,  1, -3),
        (2,  3,  1),
        (2, -1,  3),
        (2, -3, -1),
        //
        (3,  1,  2),
        (3, -2,  1),
        (3, -1, -2),
        (3,  2, -1),
        //
        (-1,  3,  2),
        (-1, -2,  3),
        (-1, -3, -2),
        (-1,  2, -3),
        //
        (-2,  1,  3),
        (-2, -3,  1),
        (-2, -1, -3),
        (-2,  3, -1),
        //
        (-3,  1, -2),
        (-3,  2,  1),
        (-3, -1,  2),
        (-3, -2, -1)
    };  

    static async Task<ScannerInput> GetInput()
    {
        var input = await ReadInputLines(nameof(Day19));
        var scannersInput = new ScannerInput();

        var currentScanner = -1;
        var currentSet = new HashSet<Point>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            bool isHeader = false;
            line.MatchGroup("--- scanner (\\d+) ---", (scannerNumber) =>
            {
                if (currentScanner >= 0)
                {
                    scannersInput.Add(currentScanner, currentSet);
                }
                currentScanner = int.Parse(scannerNumber);
                currentSet = new HashSet<(int, int, int)>();
                isHeader = true;
            });
            if (!isHeader)
            {
                var coordinates = line.SplitBy(",").Select(int.Parse).ToArray();
                currentSet.Add((coordinates[0], coordinates[1], coordinates[2]));
            }
        }
        if (currentScanner >= 0)
        {
            scannersInput.Add(currentScanner, currentSet);
        }

        return scannersInput;
    }
}