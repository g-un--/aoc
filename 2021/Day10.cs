namespace _2021;

using System.Numerics;
using static Utils;

public class Day10
{
    public static HashSet<char> OpenChars { get; } = new HashSet<char>(new[] { '(', '[', '{', '<' });

    public static char GetPair(char c)
    {
        switch (c)
        {
            case ')': return '(';
            case '}': return '{';
            case ']': return '[';
            case '>': return '<';
            default: throw new ArgumentException(c.ToString());
        }
    }

    public static char GetClosingChar(char c)
    {
        switch (c)
        {
            case '(': return ')';
            case '[': return ']';
            case '{': return '}';
            case '<': return '>';
            default: throw new ArgumentException(c.ToString());
        }
    }

    public static int GetPoints(char c)
    {
        switch (c)
        {
            case ')': return 1;
            case ']': return 2;
            case '}': return 3;
            case '>': return 4;
            default: throw new ArgumentException(c.ToString());
        }
    }

    public static int GetError(char c)
    {
        switch (c)
        {
            case '}': return 1197;
            case ']': return 57;
            case ')': return 3;
            case '>': return 25137;
            default: throw new ArgumentException(c.ToString());
        }
    }

    public static int GetError(string line)
    {
        var stack = new Stack<char>();
        foreach (var c in line)
        {
            if (OpenChars.Contains(c))
            {
                stack.Push(c);
            }
            else if (stack.Count > 0 && GetPair(c) == stack.Peek())
            {
                stack.Pop();
            }
            else
            {
                return GetError(c);
            }
        }
        return 0;
    }

    public static BigInteger GetCompletionPoins(string line)
    {
        var stack = new Stack<char>();
        bool isInvalid = false;
        foreach (var c in line)
        {
            if (OpenChars.Contains(c))
            {
                stack.Push(c);
            }
            else if (stack.Count > 0 && GetPair(c) == stack.Peek())
            {
                stack.Pop();
            }
            else
            {
                isInvalid = true;
                return 0;
            }
        }
        if (!isInvalid && stack.Count > 0)
        {
            BigInteger score = BigInteger.Zero;
            while (stack.Count > 0)
            {
                var openChar = stack.Pop();
                var closingChar = GetClosingChar(openChar);
                score = score * 5 + GetPoints(closingChar);
            }
            return score;
        }
        return 0;
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day10));
        var sum = 0;
        foreach (var line in input)
        {
            var error = GetError(line);
            if (error > 0)
            {
                sum += error;
            }
        }
        Assert.Equal(388713, sum);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day10));
        var scores = new List<BigInteger>();
        foreach (var line in input)
        {
            var score = GetCompletionPoins(line);
            if (score > 0)
            {
                scores.Add(score);
            }
        }
        var sortedScores = scores.OrderBy(x => x).ToArray();
        Assert.Equal(3539961434, sortedScores[sortedScores.Length / 2]);
    }
}