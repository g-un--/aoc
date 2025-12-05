from utils import getLines


def findStart(grid):
    for y in range(len(grid)):
        for x in range(len(grid)):
            if grid[y][x] == "^":
                return (y, x)


def getXs(grid):
    result = []
    for y in range(len(grid)):
        for x in range(len(grid)):
            if grid[y][x] == "X":
                result.append((y, x))
    return result


def addTuples(a, b):
    return tuple(map(sum, zip(a, b)))


def printGrid(grid):
    for line in grid:
        print(str(line))


def walkGrid(targetGrid):
    grid = [line[:] for line in targetGrid]
    directions = [(-1, 0), (0, 1), (1, 0), (0, -1)]
    seen = set()

    startY, startX = findStart(grid)
    startDirectionIndex = 0
    limit = len(grid)

    while True:
        state = (startY, startX, startDirectionIndex)
        if state in seen:
            return True, grid
        seen.add(state)

        grid[startY][startX] = "X"

        dy, dx = directions[startDirectionIndex]
        nextY, nextX = startY + dy, startX + dx

        if not (0 <= nextY < limit and 0 <= nextX < limit):
            break

        if grid[nextY][nextX] == "#":
            startDirectionIndex = (startDirectionIndex + 1) % len(directions)
        else:
            startY, startX = nextY, nextX

    return False, grid


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    grid = [list(line) for line in lines]
    _, resultGrid = walkGrid(grid)
    # printGrid(grid)
    return len(getXs(resultGrid))


def part2(input="input.txt"):
    lines = getLines(__file__, input)
    grid = [list(line) for line in lines]
    copyGrid = [line[:] for line in grid]
    _, copyGrid = walkGrid(copyGrid)
    path = getXs(copyGrid)
    count = 0
    for y, x in path:
        if grid[y][x] == ".":
            clone = [line[:] for line in grid]
            clone[y][x] = "#"
            cycleFound, _ = walkGrid(clone)
            if cycleFound:
                count += 1
    return count


def test_part1_example():
    result = part1("example.txt")
    assert result == 41


def test_part1():
    result = part1()
    assert result == 4580


def test_part2_example():
    result = part2("example.txt")
    assert result == 6


def test_part2():
    result = part2()
    assert result == 1480
