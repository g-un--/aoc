namespace _2018;

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
}
