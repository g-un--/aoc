namespace _2021;
using static Utils;

public class Day4
{
    public record struct Board((int, bool)[,] values)
    {
        public Board() : this(new (int, bool)[5, 5]) { }
    }

    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day4));
        var numbers = content[0].Split(",").Select(int.Parse).ToArray();
        var boards = ParseBoards(content.Skip(2));

        Board? winnerBoard = null;
        int? winnerNumber = null;

        Play(boards, numbers, (number, board) =>
        {
            UpdateBoard(board, number);
            if (CheckBoard(board))
            {
                winnerBoard = board;
                winnerNumber = number;
                return false;
            }
            return true;
        });

        if (winnerBoard == null || winnerNumber == null)
        {
            throw new Exception("We should have a winner");
        }
        var score = GetScore((Board)winnerBoard, (int)winnerNumber);
        Assert.Equal(74320, score);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day4));
        var numbers = content[0].Split(",").Select(int.Parse).ToArray();
        var boards = ParseBoards(content.Skip(2));

        var winnersBoards = new HashSet<Board>();
        Board? lastWinnerBoard = null;
        int? lastWinnerNumber = null;

        Play(boards, numbers, (number, board) =>
        {
            bool shouldContinue = true;
            if (!winnersBoards.Contains(board))
            {
                UpdateBoard(board, number);
                if (CheckBoard(board))
                {
                    winnersBoards.Add(board);
                    lastWinnerBoard = board;
                    lastWinnerNumber = number;
                    if (winnersBoards.Count == boards.Count)
                    {
                        shouldContinue = false;
                    }
                }
            }
            return shouldContinue;
        });

        if (lastWinnerBoard == null || lastWinnerNumber == null)
        {
            throw new Exception("We should have a winner");
        }
        var score = GetScore((Board)lastWinnerBoard, (int)lastWinnerNumber);
        Assert.Equal(17884, score);
    }

    static List<Board> ParseBoards(IEnumerable<string> input)
    {
        var boards = new List<Board>();
        var currentBoard = new Board();
        var currentLine = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                currentBoard = new Board();
                boards.Add(currentBoard);
                currentLine = 0;
            }
            else
            {
                var lineNumbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (var column = 0; column < 5; column++)
                {
                    currentBoard.values[currentLine, column] = (lineNumbers[column], false);
                }
                currentLine += 1;
            }
        }
        return boards;
    }

    static void UpdateBoard(Board board, int number)
    {
        for (var row = 0; row < 5; row++)
        {
            for (var column = 0; column < 5; column++)
            {
                var (boardNumber, _) = board.values[row, column];
                if (boardNumber == number)
                {
                    board.values[row, column] = (boardNumber, true);
                }
            }
        }
    }

    static void Play(List<Board> boards, int[] numbers, Func<int, Board, bool> handler)
    {
        var shouldContinue = true;
        foreach (var number in numbers)
        {
            if (!shouldContinue)
            {
                break;
            }
            foreach (var board in boards)
            {
                if (!shouldContinue)
                {
                    break;
                }
                shouldContinue = handler(number, board);
            }
        }
    }

    static bool CheckBoard(Board board)
    {
        for (var dimension1 = 0; dimension1 < 5; dimension1++)
        {
            var currentRowHighlightedNumbers = 0;
            var currentColumnHightlightedNumbers = 0;
            for (var dimension2 = 0; dimension2 < 5; dimension2++)
            {
                var (boardNumber1, isHightlighted1) = board.values[dimension1, dimension2];
                if (isHightlighted1)
                {
                    currentRowHighlightedNumbers += 1;
                }
                var (boardNumber2, isHightlighted2) = board.values[dimension2, dimension1];
                if (isHightlighted2)
                {
                    currentColumnHightlightedNumbers += 1;
                }
            }
            if (currentRowHighlightedNumbers == 5 || currentColumnHightlightedNumbers == 5)
            {
                return true;
            }
        }
        return false;
    }

    static int GetScore(Board winnerBoard, int winnerNumber)
    {
        var score = 0;
        for (var row = 0; row < 5; row++)
        {
            for (var column = 0; column < 5; column++)
            {
                var (boardNumber, isHighlighted) = winnerBoard.values[row, column];
                if (!isHighlighted)
                {
                    score += boardNumber;
                }
            }
        }
        score *= winnerNumber;
        return score;
    }
}