namespace _2021;

using static Utils;

public class Day18
{
    class Pair
    {
        public int? Value { get; set; }

        public Pair? Left { get; set; }

        public Pair? Right { get; set; }

        public Pair? Parent { get; set; }
    }

    static Pair ParsePair(string input)
    {
        Pair current = new Pair();
        foreach (var value in input)
        {
            if (Char.IsDigit(value))
            {
                current.Value = (int)Char.GetNumericValue(value);
            }
            else if (value == '[')
            {
                current.Left = new Pair();
                current.Left.Parent = current;
                current = current.Left;
            }
            else if (value == ']')
            {
                current = current.Parent ?? throw new ArgumentException(input);
            }
            else if (value == ',')
            {
                var parent = current.Parent;
                if (parent == null) throw new ArgumentException(input);
                parent.Right = new Pair();
                parent.Right.Parent = current.Parent;
                current = parent.Right;
            }
            else
            {
                throw new ArgumentException(input);
            }
        }
        return current;
    }

    static IEnumerable<Pair> IteratePair(Pair pair)
    {
        if (pair.Value.HasValue)
        {
            return new[] { pair };
        }
        else if (pair.Left != null && pair.Right != null)
        {
            return new[] { pair }.Concat(IteratePair(pair.Left)).Concat(IteratePair(pair.Right));
        }

        return Enumerable.Empty<Pair>();
    }

    static IEnumerable<Pair> Parents(Pair pair)
    {
        var current = pair;
        while (current.Parent != null)
        {
            yield return current.Parent;
            current = current.Parent;
        }
    }

    static Pair Root(Pair pair) =>
        pair.Parent != null ? Root(pair.Parent) : pair;

    static int Level(Pair pair, int soFar = 0) =>
        pair.Parent != null ? Level(pair.Parent, soFar + 1) : soFar;

    static Pair Explode(Pair target)
    {
        if (target.Left == null || target.Right == null || target.Parent == null)
        {
            throw new ArgumentException(nameof(target));
        }
        var root = Root(target);

        var leftPairValueToUpdate =
            IteratePair(root)
                .TakeWhile(pair => pair != target)
                .Aggregate<Pair, Pair?>(null, (toUpdate, current) =>
                    current.Value.HasValue ? current : toUpdate);

        var rightPairValueToUpdate =
            IteratePair(root)
                .SkipWhile(pair => pair != target)
                .FirstOrDefault(pair => pair.Value.HasValue && !Parents(pair).Contains(target));

        if (leftPairValueToUpdate != null)
        {
            leftPairValueToUpdate.Value += target.Left.Value;
        }

        if (rightPairValueToUpdate != null)
        {
            rightPairValueToUpdate.Value += target.Right.Value;
        }

        if (target.Parent.Left == target)
        {
            target.Parent.Left = new Pair { Value = 0, Parent = target.Parent };
        }
        else
        {
            target.Parent.Right = new Pair { Value = 0, Parent = target.Parent };
        }

        return target.Parent;
    }

    static void Split(Pair pair)
    {
        if (pair.Value == null || pair.Parent == null)
        {
            throw new ArgumentException(nameof(pair));
        }
        var value = (int)pair.Value;
        var leftValue = (int)Math.Floor((double)pair.Value / 2);
        var rightValue = (int)Math.Ceiling((double)pair.Value / 2);
        var newPair = new Pair
        {
            Left = new Pair { Value = leftValue },
            Right = new Pair { Value = rightValue }
        };
        newPair.Left.Parent = newPair;
        newPair.Right.Parent = newPair;
        newPair.Parent = pair.Parent;

        if (pair.Parent.Left == pair)
        {
            pair.Parent.Left = newPair;
        }
        else
        {
            pair.Parent.Right = newPair;
        }

    }

    static string Print(Pair pair)
    {
        if (pair.Value.HasValue)
        {
            return pair.Value?.ToString() ?? string.Empty;
        }
        else if (pair.Left != null && pair.Right != null)
        {
            return $"[{Print(pair.Left)},{Print(pair.Right)}]";
        }
        throw new ArgumentException(nameof(pair));
    }

    static bool SearchAndExplode(Pair root)
    {
        var firstToExplode = IteratePair(root).FirstOrDefault(x =>
            (x.Left != null) && (x.Right != null) && Level(x) == 4);

        if (firstToExplode != null)
        {
            Explode(firstToExplode);
            return true;
        }

        return false;
    }

    static bool SearchAndSplit(Pair root)
    {
        var firstToSplit = IteratePair(root).FirstOrDefault(x =>
            x.Value.HasValue && x.Value >= 10);

        if (firstToSplit != null)
        {
            Split(firstToSplit);
            return true;
        }

        return false;
    }

    static void Reduce(Pair target)
    {
        var shouldContinue = true;
        while (shouldContinue)
        {
            var exploded = SearchAndExplode(target);
            if (!exploded)
            {
                var splitted = SearchAndSplit(target);
                shouldContinue = splitted;
            }
        }
    }

    static Pair Add(Pair left, Pair right)
    {
        var result = new Pair
        {
            Left = left,
            Right = right
        };
        left.Parent = result;
        right.Parent = result;
        return result;
    }

    static int Magnitude(Pair pair)
    {
        if (pair.Value.HasValue)
        {
            return (int)pair.Value;
        }
        else if (pair.Left != null && pair.Right != null)
        {
            return 3 * Magnitude(pair.Left) + 2 * Magnitude(pair.Right);
        }
        else
        {
            throw new ArgumentException(nameof(pair));
        }
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day18));
        var pairs = input.Select(ParsePair).ToList();
        var current = pairs[0];
        Reduce(current);
        foreach (var pair in pairs.Skip(1))
        {
            Reduce(pair);
            current = Add(current, pair);
            Reduce(current);
        }
        var printed = Print(current);
        var result = Magnitude(current);
        Assert.Equal(4116, result);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day18));
        var max = int.MinValue;
        for (var index1 = 0; index1 < input.Length; index1++)
        {
            for (var index2 = 0; index2 < input.Length; index2++)
            {
                if (index1 == index2)
                {
                    continue;
                }
                var pairs = input.Select(ParsePair).ToList();
                var result = Add(pairs[index1], pairs[index2]);
                Reduce(result);
                var magnitude = Magnitude(result);
                if (magnitude > max)
                {
                    max = magnitude;
                }
            }
        }
        Assert.Equal(4638, max);
    }
}