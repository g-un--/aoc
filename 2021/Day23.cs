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

    static string Room((char, char) chars) => new String(new[] { chars.Item1, chars.Item2 });

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
        var board = new Board(hallway, Room(rooms[0]), Room(rooms[1]), Room(rooms[2]), Room(rooms[3]));
        var min = Play(board);

        Assert.Equal(14350, min);
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
            if (IsValid(board))
            {
                min = costs[board];
                continue;
            }

            var pieceToMoveFromRoom = GetValidPieceToMoveFromRoom(board).ToList();
            foreach (var (room, piecePosition) in pieceToMoveFromRoom)
            {
                foreach (var hallwayPosition in GetValidHallwaySpots(board.hallway, room))
                {
                    var newBoard = MovePieceToHallway((room, piecePosition), hallwayPosition, board, costs);
                    if (boardsChecked.Contains(board)) continue;
                    if (costs[newBoard] < min)
                        toCheck.Add(newBoard);
                }
            }

            var pieceToMoveFromHallway = GetValidPieceToMoveFromHallway(board).ToList();
            foreach (var (index, piece) in pieceToMoveFromHallway)
            {
                var newBoard = MovePieceFromHallway(index, piece, board, costs);
                if (boardsChecked.Contains(board)) continue;
                if (costs[newBoard] < min)
                    toCheck.Add(newBoard);
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
        for (var room = 0; room < 4; room++)
        {
            var expectedChar = ExpectedChars[room];
            var first = GetRoomAt(board, room)[0];
            var second = GetRoomAt(board, room)[1];
            if (second != expectedChar)
            {
                if (first != '.') yield return (room, 0);
                if (first == '.' && second != '.') yield return (room, 1);
            }
            else
            {
                if (first != '.' && first != expectedChar) yield return (room, 0);
            }
        }
    }

    static IEnumerable<(int, (int, int))> GetValidPieceToMoveFromHallway(Board board)
    {
        var hallway = board.hallway;
        for (var hallwayIndex = 0; hallwayIndex < 11; hallwayIndex++)
        {
            var piece = hallway[hallwayIndex];
            if (piece != '.')
            {
                var room = Array.IndexOf(ExpectedChars, piece);
                var roomIndex = 2 + (room * 2);
                var roomValue = GetRoomAt(board, room);
                var allEmpty = AllEmptyFromHallway(hallway, hallwayIndex, roomIndex);
                if (roomValue.All(x => x == '.') && allEmpty)
                {
                    yield return (hallwayIndex, (room, 1));
                }
                else if (roomValue[1] == piece && roomValue[0] == '.' && allEmpty)
                {
                    yield return (hallwayIndex, (room, 0));
                }
            }
        }
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
            Enumerable.Range(roomIndex, hallwayIndex - roomIndex):
            Enumerable.Range(hallwayIndex + 1, roomIndex - hallwayIndex);
        
        var allEmpty = range.Select(x => hallway[x]).All(x => x == '.');
        return allEmpty;
    }

    static Board MovePieceToHallway((int, int) piece, int hallwayIndex, Board board, Dictionary<Board, int> costs)
    {
        var rooms = new[] { board.room0, board.room1, board.room2, board.room3 };
        var hallway = board.hallway;
        var (room, pieceIndex) = piece;
        var charValue = rooms[room][pieceIndex];
        var roomIndex = 2 + (room * 2);
        var pieceCost = Costs[charValue];
        var hallwayCost = (Math.Abs(hallwayIndex - roomIndex)) * pieceCost;
        var roomCost = (pieceIndex + 1) * pieceCost;

        var newHallway = UpdateAtIndex(hallway, hallwayIndex, charValue);
        var newRoom = UpdateAtIndex(rooms[room], pieceIndex, '.');
        rooms[room] = newRoom;
        var newBoard = new Board(newHallway, rooms[0], rooms[1], rooms[2], rooms[3]);
        var newCost = costs[board] + hallwayCost + roomCost;
        UpdateCosts(newBoard, newCost, costs);
        return newBoard;
    }

    static Board MovePieceFromHallway(int hallwayIndex, (int, int) piece, Board board, Dictionary<Board, int> costs)
    {
        var rooms = new[] { board.room0, board.room1, board.room2, board.room3 };
        var hallway = board.hallway;
        var (room, pieceIndex) = piece;
        var charValue = hallway[hallwayIndex];
        var roomIndex = 2 + (room * 2);
        var pieceCost = Costs[charValue];
        var hallwayCost = (Math.Abs(hallwayIndex - roomIndex)) * pieceCost;
        var roomCost = (pieceIndex + 1) * pieceCost;

        var newHallway = UpdateAtIndex(hallway, hallwayIndex, '.');
        var newRoom = UpdateAtIndex(rooms[room], pieceIndex, charValue);
        rooms[room] = newRoom;
        var newBoard = new Board(newHallway, rooms[0], rooms[1], rooms[2], rooms[3]);
        var newCost = costs[board] + hallwayCost + roomCost;
        UpdateCosts(newBoard, newCost, costs);
        return newBoard;
    }

    static void UpdateCosts(Board newBoard, int newCost, Dictionary<Board, int> costs)
    {
        if (!costs.TryGetValue(newBoard, out var oldCost))
        {
            costs[newBoard] = newCost;
        }
        else
        {
            costs[newBoard] = Math.Min(newCost, oldCost);
        }
    }

    static string GetRoomAt(Board board, int index)
    {
        if (index == 0)
        {
            return board.room0;
        }

        if (index == 1)
        {
            return board.room1;
        }

        if (index == 2)
        {
            return board.room2;
        }

        if (index == 3)
        {
            return board.room3;
        }

        throw new ArgumentException(nameof(index));
    }

    static string UpdateAtIndex(string target, int index, char newValue)
    {
        var newTarget = target.ToCharArray();
        newTarget[index] = newValue;
        return new string(newTarget);
    }

    static bool IsValid(Board board)
    {
        var count1 = board.room0.Count(x => x == 'A');
        var count2 = board.room1.Count(x => x == 'B');
        var count3 = board.room2.Count(x => x == 'C');
        var count4 = board.room3.Count(x => x == 'D');

        return count1 == 2 && count2 == 2 && count3 == 2 && count4 == 2;
    }
}