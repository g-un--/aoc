namespace _2021;

using static Utils;

public class Day23
{
    public record struct Board(
        string hallway,
        string room0,
        string room1,
        string room2,
        string room3);

    static string Room2((char, char) chars) => new String(new[] {
        chars.Item1, chars.Item2
    });

    static string Room4((char, char, char, char) chars) => new String(new[] {
        chars.Item1, chars.Item2, chars.Item3, chars.Item4
    });

    [Fact]
    public static async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day23));
        var hallway = new String(Enumerable.Repeat('.', 11).ToArray());
        var rooms = new List<(char, char)>();
        for (var index = 3; index <= 9; index += 2)
        {
            rooms.Add((input[2][index], input[3][index]));
        }
        var board = new Board(hallway, Room2(rooms[0]), Room2(rooms[1]), Room2(rooms[2]), Room2(rooms[3]));
        var min = Play(board);

        Assert.Equal(14350, min);
    }

    [Fact]
    public static async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day23));
        var hallway = new String(Enumerable.Repeat('.', 11).ToArray());
        var rooms = new List<(char, char, char, char)>();
        var extra = new Dictionary<int, char[]> {
            {0, new [] {'D', 'D'}},
            {1, new [] {'C', 'B'}},
            {2, new [] {'B', 'A'}},
            {3, new [] {'A', 'C'}},
        };
        for (var index = 0; index <= 3; index++)
        {
            var inputIndex = 3 + (2 * index);
            rooms.Add((input[2][inputIndex], extra[index][0], extra[index][1], input[3][inputIndex]));
        }
        var board = new Board(hallway, Room4(rooms[0]), Room4(rooms[1]), Room4(rooms[2]), Room4(rooms[3]));
        var min = Play(board);

        Assert.Equal(49742, min);
    }

    static int Play(Board start)
    {
        var toCheck = new HashSet<Board>();
        var costs = new Dictionary<Board, int>();
        var boardsChecked = new HashSet<Board>();
        costs[start] = 0;
        toCheck.Add(start);
        int min = int.MaxValue;

        while (toCheck.Count > 0)
        {
            var board = toCheck.First();
            toCheck.Remove(board);
            if (costs[board] > min) continue;
            if (IsValid(board, board.room0.Length))
            {
                min = costs[board];
                continue;
            }

            var pieceToMoveFromHallway = GetValidPieceToMoveFromHallway(board).ToList();
            foreach (var (index, piece) in pieceToMoveFromHallway)
            {
                var fromHallway = true;
                var (newBoard, shouldRecheck) = MovePiece(fromHallway, piece, index, board, costs);
                if (boardsChecked.Contains(board) && !shouldRecheck) continue;
                if (costs[newBoard] < min)
                    toCheck.Add(newBoard);
            }

            var pieceToMoveFromRoom = GetValidPieceToMoveFromRoom(board).ToList();
            foreach (var (room, piecePosition) in pieceToMoveFromRoom)
            {
                foreach (var index in GetValidHallwaySpots(board.hallway, room))
                {
                    var fromHallway = false;
                    var piece = (room, piecePosition);
                    var (newBoard, shouldRecheck) = MovePiece(fromHallway, piece, index, board, costs);
                    if (boardsChecked.Contains(board) && !shouldRecheck) continue;
                    if (costs[newBoard] < min)
                        toCheck.Add(newBoard);
                }
            }
            boardsChecked.Add(board);
        }

        return min;
    }

    static IEnumerable<int> GetValidHallwaySpots(string hallway, int room)
    {
        var roomIndex = 2 + (room * 2);
        for (var index = 0; index <= 10; index++)
        {
            if (index == 2 || index == 4 || index == 6 || index == 8) continue;
            var allEmpty = AllEmptyToHallway(hallway, index, roomIndex);
            if (allEmpty)
            {
                yield return index;
            }
        }
    }

    static char[] ExpectedChars = new char[] { 'A', 'B', 'C', 'D' };

    static Dictionary<char, int> Costs = new Dictionary<char, int> {
        {'A', 1},
        {'B', 10},
        {'C', 100},
        {'D', 1000}
    };

    static IEnumerable<(int, int)> GetValidPieceToMoveFromRoom(Board board)
    {
        var rooms = new[] { board.room0, board.room1, board.room2, board.room3 };
        for (var room = 0; room < 4; room++)
        {
            var roomValue = rooms[room];
            var expectedChar = ExpectedChars[room];
            var roomIsValid = IsValid(roomValue, expectedChar);

            if (!roomIsValid)
            {
                var target = roomValue.FirstOrDefault(x => x != '.');
                if (target != default)
                {
                    yield return (room, roomValue.IndexOf(target));
                }
            }
        }
    }

    static IEnumerable<(int, (int, int))> GetValidPieceToMoveFromHallway(Board board)
    {
        var hallway = board.hallway;
        var rooms = new[] { board.room0, board.room1, board.room2, board.room3 };
        for (var hallwayIndex = 0; hallwayIndex < 11; hallwayIndex++)
        {
            var piece = hallway[hallwayIndex];
            if (piece != '.')
            {
                var room = Array.IndexOf(ExpectedChars, piece);
                var roomIndex = 2 + (room * 2);
                var roomValue = rooms[room];
                var allEmpty = AllEmptyFromHallway(hallway, hallwayIndex, roomIndex);
                var target = roomValue.LastIndexOf('.');
                var isValidRoom = IsValid(roomValue, piece);

                if (target >= 0 && isValidRoom && allEmpty)
                    yield return (hallwayIndex, (room, target));
            }
        }
    }

    static bool IsValid(string roomValue, char expectedChar)
    {
        var dotCount = roomValue.TakeWhile(x => x == '.').Count();
        var charCount = roomValue.Where(x => x == expectedChar).Count();
        return roomValue.Length == charCount + dotCount;
    }

    static bool AllEmptyToHallway(string hallway, int hallwayIndex, int roomIndex)
    {
        var range = roomIndex > hallwayIndex ?
            Enumerable.Range(hallwayIndex, roomIndex - hallwayIndex + 1) :
            Enumerable.Range(roomIndex, hallwayIndex - roomIndex + 1);

        var allEmpty = range.Select(x => hallway[x]).All(x => x == '.');
        return allEmpty;
    }

    static bool AllEmptyFromHallway(string hallway, int hallwayIndex, int roomIndex)
    {
        var range = hallwayIndex > roomIndex ?
            Enumerable.Range(roomIndex, hallwayIndex - roomIndex) :
            Enumerable.Range(hallwayIndex + 1, roomIndex - hallwayIndex);

        var allEmpty = range.Select(x => hallway[x]).All(x => x == '.');
        return allEmpty;
    }

    static (Board, bool) MovePiece(bool fromHallway, (int, int) piece, int hallwayIndex, Board board, Dictionary<Board, int> costs)
    {
        var rooms = new[] { board.room0, board.room1, board.room2, board.room3 };
        var hallway = board.hallway;
        var (room, pieceIndex) = piece;
        var charValue = fromHallway ? hallway[hallwayIndex] : rooms[room][pieceIndex];
        var roomIndex = 2 + (room * 2);
        var pieceCost = Costs[charValue];
        var hallwayCost = (Math.Abs(hallwayIndex - roomIndex)) * pieceCost;
        var roomCost = (pieceIndex + 1) * pieceCost;

        var newHallway = hallway.UpdateAtIndex(hallwayIndex, fromHallway ? '.' : charValue);
        var newRoom = rooms[room].UpdateAtIndex(pieceIndex, fromHallway ? charValue : '.');
        rooms[room] = newRoom;
        var newBoard = new Board(newHallway, rooms[0], rooms[1], rooms[2], rooms[3]);
        var newCost = costs[board] + hallwayCost + roomCost;
        var shouldRecheck = UpdateCosts(newBoard, newCost, costs);
        return (newBoard, shouldRecheck);
    }

    static bool UpdateCosts(Board newBoard, int newCost, Dictionary<Board, int> costs)
    {
        if (!costs.TryGetValue(newBoard, out var oldCost))
        {
            costs[newBoard] = newCost;
            return false;
        }
        else
        {
            costs[newBoard] = Math.Min(newCost, oldCost);
            return newCost < oldCost;
        }
    }

    static bool IsValid(Board board, int size)
    {
        var count1 = board.room0.Count(x => x == 'A');
        var count2 = board.room1.Count(x => x == 'B');
        var count3 = board.room2.Count(x => x == 'C');
        var count4 = board.room3.Count(x => x == 'D');

        return count1 == size && count2 == size && count3 == size && count4 == size;
    }
}