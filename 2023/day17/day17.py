from utils import getLines
import numpy as np
import sys
import heapq

def getline(line):
    return [int(c) for c in line]

def getNextDirections(previousDirection):
    match previousDirection:
        case "up":
            return ["up", "left", "right"]
        case "down":
            return ["down", "right", "left"]
        case "left":
            return ["left", "down", "up"]
        case "right":
            return ["right", "up", "down"]
        
def getNextPoint(point, direction):
    row, column = point
    match direction:
        case "up":
            return (row - 1, column)
        case "down":
            return (row + 1, column)
        case "left":
            return (row, column - 1)
        case "right":
            return (row, column + 1)
        
def findDistances(array, validateDirection):
    queue = [(0, 0, 0, "right", 0), (0, 0, 0, "down", 0)]
    rows, columns = np.shape(array)
    seen = {}
    while queue:
        dist, row, column, direction, steps = heapq.heappop(queue)
        if (row, column, direction, steps) in seen:
            continue
        seen[(row, column, direction, steps)] = dist
        nextDirections = getNextDirections(direction)
        for nextDirection in nextDirections:
            nextRow, nextColumn = getNextPoint((row, column), nextDirection)
            nextSteps = steps + 1 if nextDirection == direction else 1
            isValidDirection = validateDirection(direction, steps, nextDirection, nextSteps)
            
            if 0 <= nextRow < rows and 0 <= nextColumn < columns and isValidDirection:
                heapq.heappush(queue, (dist + array[nextRow, nextColumn], nextRow, nextColumn, nextDirection, nextSteps))
    return seen

def findMinFor(distances, rowToFind, columnToFind, stepsCondition):
    min = sys.maxsize
    for (row, column, _, steps), dist in distances.items():
        if stepsCondition(steps) and row == rowToFind and column == columnToFind and dist < min:
            min = dist
    return min

def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    rows, columns = np.shape(array)
    validateDirection = lambda direction, steps, nextDirection, nextSteps: nextSteps <= 3
    result = findDistances(array, validateDirection)
    stepsCondition = lambda steps: True
    min = findMinFor(result, rows - 1, columns - 1, stepsCondition)
    return min

def part2():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    rows, columns = np.shape(array)
    validateDirection = lambda direction, steps, nextDirection, nextSteps: nextSteps <= 10 and ((steps >= 4 and direction != nextDirection) or (direction == nextDirection))
    result = findDistances(array, validateDirection)
    stepsCondition = lambda steps: steps >= 4
    min = findMinFor(result, rows - 1, columns - 1, stepsCondition)
    return min
        
def test_part_1():
    assert part1() == 1039
    
def test_part_2():
    assert part2() == 1201