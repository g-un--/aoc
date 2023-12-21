from utils import getLines
import numpy as np

def getline(line):
    parts = line.split(' ')
    return (parts[0], int(parts[1]))

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
        
def getAreaWithShoelace(path):
    array = np.array(path)
    x = array[:, 0]
    y = array[:, 1]
    i = np.arange(len(path))
    area = np.abs(np.sum(x[i-1]*y[i]-x[i]*y[i-1])) // 2
    return area
        
def part1():
    lines = list(map(getline, getLines(__file__)))
    path = [(0, 0)]
    start = (0, 0)
    perimeter = 0
    for direction, distance in lines:
        start = move(start, direction, distance)
        path.append(start)
        perimeter += distance
    area = getAreaWithShoelace(path)
    i = area - perimeter // 2 + 1
    return i + perimeter

def test_part_1():
    assert part1() == 67891