from utils import getLines
import numpy as np

def getline(line):
    return [c for c in line]

def getDirections(cell, direction):    
    if cell == ".":
        return [direction]
    
    if cell == "\\":
        match direction:
            case "right": 
                return ["down"]
            case "left":
                return ["up"]
            case "up":
                return ["left"]
            case "down":
                return ["right"]
    
    if cell == "/":
        match direction:
            case "right":
                return ["up"]
            case "left":
                return ["down"]
            case "up":
                return ["right"]
            case "down":
                return ["left"]

    if cell == "-":
        match direction:
            case "right" | "left":
                return [direction]
            case "up" | "down":
                return ["left", "right"]
            
    if cell == "|":
        match direction:
            case "up" | "down":
                return [direction]
            case "left" | "right":
                return ["up", "down"]  
            
def moveBeam(array, startRow, startColumn, startDirection, result, seen):
    queue = [(startRow, startColumn, startDirection)]
    rows, columns = np.shape(array)
    
    while len(queue) > 0:
        row, column, direction = queue.pop(0)    
        if (row, column, direction) in seen:
            continue
        
        result.add((row, column))
        seen.add((row, column, direction))

        cellValue = array[row, column]
        directions = getDirections(cellValue, direction)
        for newDirection in directions:
            newRow, newColumn = row, column    
            match newDirection:  
                case "right":
                    newColumn = column + 1
                case "left":
                    newColumn = column - 1
                case "up":
                    newRow = row - 1
                case "down":
                    newRow = row + 1
            
            if not (0 <= newRow < rows and 0 <= newColumn < columns):
                continue
            
            queue.append((newRow, newColumn, newDirection))  
        
def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    result = set()
    seen = set()
    moveBeam(array, 0, 0, "right", result, seen)
    return len(result)

def part2():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    rows, columns = np.shape(array)
    
    max = 0
    for row in range(0, rows):
        result = set()
        seen = set()
        moveBeam(array, row, 0, "right", result, seen)
        result = set()
        seen = set()
        if len(result) > max:
            max = len(result)
        moveBeam(array, row, columns - 1, "left", result, seen)
        if len(result) > max:
            max = len(result)
            
    for column in range(0, columns):
        result = set()
        seen = set()
        moveBeam(array, 0, column, "down", result, seen)
        result = set()
        seen = set()
        if len(result) > max:
            max = len(result)
        moveBeam(array, rows - 1, column, "up", result, seen)
        if len(result) > max:
            max = len(result)    
    return max
        
def test_part_1():
    assert part1() == 6605
    
def test_part_2():
    assert part2() == 51