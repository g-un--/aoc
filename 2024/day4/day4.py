from utils import getLines

def isInRange(interval, gridLength):
    a,b = interval
    if a < gridLength and b < gridLength and a >= 0 and b >= 0:
        return True
    return False

def matchXmas(start, direction, grid, gridLength, targetWord):
    if targetWord == "":
        return True
    if not isInRange(start, gridLength):
        return False
    startX, startY = start
    if targetWord[0] != grid[startX][startY]:
        return False
    directionX, directionY = direction
    newStart = (startX + directionX, startY + directionY)
    return matchXmas(newStart, direction, grid, gridLength, targetWord[1:])

def matchMas(start, grid, gridLength, directions):
    startX, startY = start
    if 'A' != grid[startX][startY]:
        return False
    found = set()
    for direction in directions:
        cornerX, cornerY = direction
        newStartX = startX + cornerX
        newStartY = startY + cornerY
        found.add(grid[newStartX][newStartY])
    if found != {"M", "S"}:
        return False
    return True

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    gridLength = len(lines)
    directions = [(1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (-1, 1), (1, -1), (-1, -1)]
    count = 0
    for x in range(0, gridLength):
        for y in range(0, gridLength):
            for direction in directions:
                if matchXmas((x,y), direction, lines, gridLength, "XMAS"):
                    count += 1
    return count

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    gridLength = len(lines)
    count = 0
    for x in range(1, gridLength-1):
        for y in range(1, gridLength-1):
            if (matchMas((x,y), lines, gridLength, [(1, 1), (-1, -1)]) 
                and matchMas((x,y), lines, gridLength, [(-1, 1), (1, -1)])):
                count += 1
    return count

def test_part1_example():
    result = part1("example.txt")
    assert result == 18

def test_part1():
    result = part1()
    assert result == 2521

def test_part2_example():
    result = part2("example.txt")
    assert result == 9

def test_part2():
    result = part2()
    assert result == 1912
    