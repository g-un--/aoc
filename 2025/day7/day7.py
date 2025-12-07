from utils import getLines


def moveBeam(start, lines, splits):
    y, x = start
    if y == len(lines) or x < 0 or x == len(lines):
        return
    if lines[y][x] != "^":
        lines[y][x] = "|"
        return moveBeam((y + 1, x), lines, splits)
    elif lines[y][x] == "^" and start not in splits:
        splits.add((y, x))
        moveBeam((y, x - 1), lines, splits)
        moveBeam((y, x + 1), lines, splits)
        return

def countTimelines(lines):
    total = 0
    previous = dict()
    for row in range(len(lines) -1, -1, -1):
        beams = dict()
        for column in range(0, len(lines[row])):
            if lines[row][column] == "|":
                beams[column] = 0
        for column in beams:
            if column not in previous:
                if column - 1 >= 0 and (column - 1) in previous: 
                    beams[column] += previous[column - 1]
                if column + 1 < len(lines[row]) and (column + 1) in previous: 
                    beams[column] += previous[column + 1]
                if beams[column] == 0:
                    beams[column] = 1
            else:
                beams[column] = previous[column]
        previous = beams
    total = sum(previous.values())
    return total


def printLines(lines):
    for line in lines:
        print("".join(line))


def part1(input="input.txt"):
    lines = [list(line) for line in getLines(__file__, input)]
    start = (0, len(lines[0]) // 2)
    splits = set()
    moveBeam(start, lines, splits)
    return len(splits)


def part2(input="input.txt"):
    lines = [list(line) for line in getLines(__file__, input)]
    start = (0, len(lines[0]) // 2)
    splits = set()
    moveBeam(start, lines, splits)
    return countTimelines(lines)

def test_part1_example():
    result = part1("example.txt")
    assert result == 21


def test_part1():
    result = part1()
    assert result == 1605


def test_part2_example():
    result = part2("example.txt")
    assert result == 40


def test_part2():
    result = part2()
    assert result == 29893386035180
