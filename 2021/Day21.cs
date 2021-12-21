namespace _2021;

using System.Diagnostics;
using System.Numerics;
using static Utils;


public class Day21
{
    public class Dice
    {
        public int Times { get; private set; } = 0;

        public int Roll()
        {
            Times += 1;
            var remainder = Times % 100;
            return remainder == 0 ? 100 : remainder;
        }
    }

    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day21));
        var player1Position = int.Parse(input[0].SplitBy(" ")[^1]);
        var player2Position = int.Parse(input[1].SplitBy(" ")[^1]);

        var player1Points = 0;
        var player2Points = 0;
        var dice = new Dice();
        var turn = 0;
        while (player1Points < 1000 && player2Points < 1000)
        {
            var points = Enumerable.Range(1, 3).Select(_ => dice.Roll()).Sum();

            if (turn == 0)
            {
                player1Position = Move(player1Position, points);
                player1Points += player1Position;
            }
            else
            {
                player2Position = Move(player2Position, points);
                player2Points += player2Position;
            }

            turn = (turn + 1) % 2;
        }

        var loserPoints = turn == 0 ? player1Points : player2Points;
        var result = loserPoints * dice.Times;
        Assert.Equal(428736, result);
    }

    [Fact]
    public static async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day21));
        var player1Position = int.Parse(input[0].SplitBy(" ")[^1]);
        var player2Position = int.Parse(input[1].SplitBy(" ")[^1]);

        var playerPointsInPosition = new Dictionary<(int, int, int, int, bool), BigInteger>();
        playerPointsInPosition[(0, 0, player1Position, player2Position, true)] = BigInteger.One;

        var shouldContinue = true;
        while (shouldContinue)
        {
            var toProcess = playerPointsInPosition.Keys.Where(key =>
            {
                var (player1Points, player2Points, _, _, __) = key;
                return player1Points < 21 && player2Points < 21;
            }).ToList();

            if (toProcess.Count == 0)
                shouldContinue = false;

            foreach (var state in toProcess)
            {
                var (points1, points2, position1, position2, turn) = state;
                var targetPoints = turn ? points1 : points2;
                var targetPosition = turn ? position1 : position2;
                foreach (var (d1, d2, d3) in Roll())
                {
                    var steps = d1 + d2 + d3;

                    var newPosition = Move(targetPosition, steps);
                    var newPoints = targetPoints + newPosition;

                    var newState = turn ? 
                        (newPoints, points2, newPosition, position2, !turn):
                        (points1, newPoints, position1, newPosition, !turn);

                    if (!playerPointsInPosition.ContainsKey(newState))
                    {
                        playerPointsInPosition[newState] = BigInteger.Zero;
                    }
                    playerPointsInPosition[newState] += playerPointsInPosition[state];
                }

                playerPointsInPosition.Remove(state);
            }
        }

        var player1Total = playerPointsInPosition.Aggregate(BigInteger.Zero, (acc, x) =>
        {
            var (key, value) = x;
            var (points1, _, __, ___, ____) = key;
            return points1 >= 21 ? acc + value : acc;
        });
        var player2Total = playerPointsInPosition.Aggregate(BigInteger.Zero, (acc, x) =>
        {
            var (key, value) = x;
            var (_, points2, __, ___, ____) = key;
            return points2 >= 21 ? acc + value : acc;
        });
        Assert.Equal(57328067654557, BigInteger.Max(player1Total, player2Total));
    }

    static int Move(int position, int dice)
    {
        var result = position + dice;
        var isTen = result % 10 == 0;
        return isTen ? 10 : result % 10;
    }

    static IEnumerable<(int, int, int)> Roll()
    {
        for (var d1 = 1; d1 <= 3; d1++)
            for (var d2 = 1; d2 <= 3; d2++)
                for (var d3 = 1; d3 <= 3; d3++)
                    yield return (d1, d2, d3);
    }
}