using System.Text.RegularExpressions;

namespace _2021;

static class Utils
{
    public static Task<string[]> ReadInputLines(string day)
    {
        var fileName = $"{day}.txt";
        var input = Path.Combine(Environment.CurrentDirectory, "input", fileName);
        return File.ReadAllLinesAsync(input);
    }

    public static string[] SplitBy(this string input, string separator) =>
        input.Split(separator, StringSplitOptions.RemoveEmptyEntries);

    public static List<int> ToIntList(this string input, string separator) =>
        input.SplitBy(separator).Select(int.Parse).ToList();

    public static IEnumerable<T> RepeatMany<T>(this IEnumerable<T> input)
    {
        var shouldContinue = true;
        while (shouldContinue)
        {
            shouldContinue = false;
            foreach (var item in input)
            {
                shouldContinue = true;
                yield return item;
            }
        }
    }

    public static IEnumerable<IList<T>> SlidingWindow<T>(this IEnumerable<T> input, int windowSize)
    {
        var window = new Queue<T>();
        foreach (var item in input)
        {
            if (window.Count == windowSize)
            {
                yield return new List<T>(window);
                window.Dequeue();
            }
            window.Enqueue(item);
        }
        if (window.Count > 0)
        {
            yield return new List<T>(window);
        }
    }

    public static void MatchGroup(this string input, string pattern, Action<string> action)
    {
        var match = Regex.Match(input, pattern);
        if (match.Success)
        {
            action(match.Groups[1].Value);
        }
    }

    public static HashSet<T> Clone<T>(this HashSet<T> target) => new HashSet<T>(target);

    public static HashSet<T> IntersectClone<T>(this HashSet<T> chars, IEnumerable<T> target)
    {
        var clone = chars.Clone();
        clone.IntersectWith(target);
        return clone;
    }

    public static HashSet<T> UnionClone<T>(this HashSet<T> chars, IEnumerable<T> target)
    {
        var clone = chars.Clone();
        clone.UnionWith(target);
        return clone;
    }

    public static HashSet<T> DiffClone<T>(this HashSet<T> chars, IEnumerable<T> target)
    {
        var clone = chars.Clone();
        clone.ExceptWith(target);
        return clone;
    }
}
