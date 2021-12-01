namespace _2021;

static class Utils
{
    public static Task<string[]> ReadInputLines(string day) 
    {
        var fileName = $"{day}.txt";
        var input = Path.Combine(Environment.CurrentDirectory, "input", fileName);
        return File.ReadAllLinesAsync(input);
    }
         

    public static IEnumerable<T> RepeatMany<T>(this IEnumerable<T> input)
    {
        var shouldContinue = true;
        while (shouldContinue) 
        {
            shouldContinue = false;
            foreach(var item in input) 
            {
                shouldContinue = true;
                yield return item;
            }
        }
    }

    public static IEnumerable<IList<T>> SlidingWindow<T>(this IEnumerable<T> input, int windowSize) 
    {
        var window = new Queue<T>();
        foreach(var item in input) {
            if (window.Count == windowSize) {
                yield return new List<T>(window);
                window.Dequeue();
            }
            window.Enqueue(item);
        }
        if (window.Count > 0) {
            yield return new List<T>(window);
        }
    }
}