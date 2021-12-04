namespace _2021;
using static Utils;

public class Day4
{
    [Fact]
    public async Task Part1()
    {
        var content = await ReadInputLines(nameof(Day4));
        var numbers = content[0].Split(",").Select(int.Parse).ToArray();
        var boards = ParseBoards(content.Skip(2));

        (int, bool)[,]? winnerBoard = null;
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
        var score = GetScore(winnerBoard, (int)winnerNumber);
        Assert.Equal(74320, score);
    }

    [Fact]
    public async Task Part2()
    {
        var content = await ReadInputLines(nameof(Day4));
        var numbers = content[0].Split(",").Select(int.Parse).ToArray();
        var boards = ParseBoards(content.Skip(2));

        var winnersBoards = new HashSet<(int, bool)[,]>();
        (int, bool)[,]? lastWinnerBoard = null;
        int? lastWinnerNumber = null;

        Play(boards, numbers, (number, board) =>
        {
            UpdateBoard(board, number);
            if (!winnersBoards.Contains(board) && CheckBoard(board))
            {
                winnersBoards.Add(board);
                lastWinnerBoard = board;
                lastWinnerNumber = number;
                if (winnersBoards.Count == boards.Count)
                {
                    return false;
                }
            }
            return true;
        });

        if (lastWinnerBoard == null || lastWinnerNumber == null)
        {
            throw new Exception("We should have a winner");
        }
        var score = GetScore(lastWinnerBoard, (int)lastWinnerNumber);
        Assert.Equal(17884, score);
    }

    static List<(int, bool)[,]> ParseBoards(IEnumerable<string> input)
    {
        var boards = new List<(int, bool)[,]>();
        (int, bool)[,] currentBoard = new (int, bool)[5, 5];
        var currentLine = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                currentBoard = new (int, bool)[5, 5];
                boards.Add(currentBoard);
                currentLine = 0;
            }
            else
            {
                var lineNumbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (var column = 0; column < 5; column++)
                {
                    currentBoard[currentLine, column] = (lineNumbers[column], false);
                }
                currentLine += 1;
            }
        }
        return boards;
    }

    static void UpdateBoard((int, bool)[,] board, int number)
    {
        for (var row = 0; row < 5; row++)
        {
            for (var column = 0; column < 5; column++)
            {
                var (boardNumber, _) = board[row, column];
                if (boardNumber == number)
                {
                    board[row, column] = (boardNumber, true);
                }
            }
        }
    }

    static void Play(List<(int, bool)[,]> boards, int[] numbers, Func<int, (int, bool)[,], bool> handler)
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

    static bool CheckBoard((int, bool)[,] board)
    {
        for (var dimension1 = 0; dimension1 < 5; dimension1++)
        {
            var currentRowHighlightedNumbers = 0;
            var currentColumnHightlightedNumbers = 0;
            for (var dimension2 = 0; dimension2 < 5; dimension2++)
            {
                var (boardNumber1, isHightlighted1) = board[dimension1, dimension2];
                if (isHightlighted1)
                {
                    currentRowHighlightedNumbers += 1;
                }
                var (boardNumber2, isHightlighted2) = board[dimension2, dimension1];
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

    static int GetScore((int, bool)[,] winnerBoard, int winnerNumber)
    {
        var score = 0;
        for (var row = 0; row < 5; row++)
        {
            for (var column = 0; column < 5; column++)
            {
                var (boardNumber, isHighlighted) = winnerBoard[row, column];
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