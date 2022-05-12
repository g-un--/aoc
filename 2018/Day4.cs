namespace _2018;

using System.Text.RegularExpressions;
using static Utils;

public record struct GuardRecord(int? guardId, DateTime date, bool isAwake);
public record GuardAction(int guardId, DateTime date, bool isAwake);

public class Day4
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day4));
        var records = GetRecords(content);
        var guardActions = GetActions(records);
        var sleepTime = GetSleepTime(guardActions);

        var sleepyGuard = sleepTime.MaxBy(kvp => kvp.Value.Sum());
        var minute = sleepyGuard.Value.IndexOf(sleepyGuard.Value.Max());

        Assert.Equal(84636, sleepyGuard.Key * minute);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day4));
        var records = GetRecords(content);
        var guardActions = GetActions(records);
        var sleepTime = GetSleepTime(guardActions);

        var sleepyGuard = sleepTime.MaxBy(kvp => kvp.Value.Max());
        var minute = sleepyGuard.Value.IndexOf(sleepyGuard.Value.Max());

        Assert.Equal(91679, sleepyGuard.Key * minute);
    }

    Dictionary<int, List<int>> GetSleepTime(List<GuardAction> guardActions)
    {
        var sleepTime = new Dictionary<int, List<int>>();
        for (var index = 0; index < guardActions.Count; index++)
        {
            var guardAction = guardActions[index];
            var guardId = (int)guardAction.guardId;
            if (!sleepTime.ContainsKey(guardId))
            {
                sleepTime.Add(guardId, Enumerable.Repeat(0, 60).ToList());
            }
            if (!guardActions[index].isAwake)
            {
                var nextIndex = index + 1;
                var limit = nextIndex < guardActions.Count &&
                    guardActions[nextIndex].guardId == guardId &&
                    guardActions[nextIndex].isAwake ?
                    guardActions[nextIndex].date.Minute : 60;

                var sleepMinutes = sleepTime[guardId];
                var start = guardActions[index].date.Hour == 23 ? 0 : guardActions[index].date.Minute;
                for (; start < limit; start++)
                {
                    sleepMinutes[start] += 1;
                }
            }
        }
        return sleepTime;
    }

    List<GuardRecord> GetRecords(string[] lines) =>
        lines.Select(x =>
        {
            var parts = Regex.Match(x, @"\[(.*)\] (.*)");
            var date = DateTime.Parse(parts.Groups[1].Value);
            var action = parts.Groups[2].Value;
            var guard = Regex.Match(action, @"Guard #(\d+)");
            if (guard.Success)
            {
                var guardId = int.Parse(guard.Groups[1].Value);
                return new GuardRecord(guardId, date, true);
            }
            else
            {
                var wakingUp = action == "wakes up";
                return new GuardRecord(null, date, wakingUp);
            }
        }).OrderBy(x => x.date).ToList();

    List<GuardAction> GetActions(List<GuardRecord> records) =>
        records.Select((x, index) =>
        {
            if (x.guardId.HasValue)
            {
                return new GuardAction(x.guardId.Value, x.date, x.isAwake);
            }
            else
            {
                var lastGuard = records.Take(index).Last(x => x.guardId.HasValue);
                if (!lastGuard.guardId.HasValue)
                {
                    throw new ArgumentException("last guard not found");
                }
                return new GuardAction(lastGuard.guardId.Value, x.date, x.isAwake);
            }
        }).ToList();
}