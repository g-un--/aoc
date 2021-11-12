namespace _2018;

static class Utils
{
    public static Task<string[]> ReadInputLines(string day) =>
         File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "input", $"{day}.txt"));

    public static IEnumerable<T> RepeatMany<T>(this T[] input)
    {
        var current = 0;
        while (true)
        {
            yield return input[current];
            current += 1;
            if (current == input.Length)
            {
                current = 0;
            }
        }
    }
}
