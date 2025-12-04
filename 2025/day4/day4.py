from utils import getLines

directions = [(1, 0), (0, 1), (-1, 0), (0, -1), (1, 1), (-1, -1), (1, -1), (-1, 1)]


def check(pos, grid):
    length = len(grid)
    x, y = pos
    count = 0
    for direction in directions:
        newX = x + direction[0]
        newY = y + direction[1]
        if newX < length and newY < length and newX >= 0 and newY >= 0:
            if grid[newX][newY] == "@":
                count += 1
    return count


def getRollsToBeRemoved(grid):
    toBeRemoved = []
    for x in range(len(grid)):
        for y in range(len(grid)):
            if grid[x][y] != "@":
                continue
            neighbors = check((x, y), grid)
            if neighbors < 4:
                toBeRemoved.append((x, y))
    return toBeRemoved


def part1(input="input.txt"):
    grid = getLines(__file__, input)
    toBeRemoved = getRollsToBeRemoved(grid)
    return len(toBeRemoved)


def part2(input="input.txt"):
    grid = [list(item) for item in getLines(__file__, input)]
    total = 0
    toBeRemoved = getRollsToBeRemoved(grid)
    while toBeRemoved:
        total += len(toBeRemoved)
        for x, y in toBeRemoved:
            grid[x][y] = "."
        toBeRemoved = getRollsToBeRemoved(grid)
    return total


def test_part1_example():
    result = part1("example.txt")
    assert result == 13


def test_part1():
    result = part1()
    assert result == 1549


def test_part2_example():
    result = part2("example.txt")
    assert result == 43


def test_part2():
    result = part2()
    assert result == 8887
