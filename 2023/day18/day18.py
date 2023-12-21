from utils import getLines
import numpy as np

def getline(line):
    parts = line.split(' ')
    return (parts[0], int(parts[1]), int(line[-7:-2], 16), int(line[-2]))

def move(fromPosition, direction, distance):
    row, column = fromPosition
    match direction:
        case "R":
            return (row, column + distance)
        case "L":
            return (row, column - distance)
        case "U":
            return (row - distance, column)
        case "D":
            return (row + distance, column)
        
def getMapForDirection():
    return {
        0: "R",
        1: "D",
        2: "L",
        3: "U"
    }
        
def getAreaWithShoelace(path):
    array = np.array(path, dtype='int64')
    x = array[:, 0]
    y = array[:, 1]
    i = np.arange(len(path))
    area = np.abs(np.sum(x[i-1]*y[i]-x[i]*y[i-1])) // 2
    return area

def solve(moves):
    path = [(0, 0)]
    start = (0, 0)
    perimeter = 0
    for direction, distance in moves:
        start = move(start, direction, distance)
        path.append(start)
        perimeter += distance
    area = getAreaWithShoelace(path)
    i = area - perimeter // 2 + 1
    return i + perimeter

def part1():
    lines = list(map(getline, getLines(__file__)))
    moves = list(map(lambda x: (x[0], x[1]), lines))
    result = solve(moves)
    return result

def part2():
    lines = list(map(getline, getLines(__file__)))
    dirMap = getMapForDirection()
    moves = list(map(lambda x: (dirMap[x[3]], x[2]), lines))
    result = solve(moves)
    return result

def test_part_1():
    assert part1() == 67891
    
def test_part_2():
    assert part2() == 94116351948493