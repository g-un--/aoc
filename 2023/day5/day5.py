from utils import getLines
import sys

def getSeeds(line):
    seeds = line.split(':')[1]
    seedNumbers = [int(n) for n in seeds.split(' ') if n]
    return seedNumbers

def getMap(lines):
    map = []
    for line in lines[1:]:
        rangeNumbers =  [int(n) for n in line.split(' ') if n]
        map.append({
            "destinationRangeStart": rangeNumbers[0],
            "sourceRangeStart": rangeNumbers[1],
            "rangeLength": rangeNumbers[2]
        })
    return map

def getDestination(number, map):
    for range in map:
        if range["sourceRangeStart"] <= number <= range["sourceRangeStart"] + range["rangeLength"]:
            diff = number - range["sourceRangeStart"]
            return range["destinationRangeStart"] + diff
    return number
    
def splitLinesIntoChunks(lines):
    result = []
    chunk = []
    for line in lines:
        if line:
            chunk.append(line)
        else:
            if len(chunk) > 0:
                result.append(chunk)
            chunk = []   
            
    if len(chunk):
        result.append(chunk)
    
    return result
    
def part1():
    lines = getLines(__file__)
    seeds = getSeeds(lines[0])
    chunks = splitLinesIntoChunks(lines[2:])
    maps = list(map(getMap, chunks))
    min = sys.maxsize
    for number in seeds:
        currentNumber = number
        for currentMap in maps:
            currentNumber = getDestination(currentNumber, currentMap)
        if currentNumber < min:
            min = currentNumber
    return min

def part2():
    lines = getLines(__file__)
    seeds = getSeeds(lines[0])
    chunks = splitLinesIntoChunks(lines[2:])
    maps = list(map(getMap, chunks))
    min = sys.maxsize
    for number in seeds:
        currentNumber = number
        for currentMap in maps:
            currentNumber = getDestination(currentNumber, currentMap)
        if currentNumber < min:
            min = currentNumber
    return min

def test_part_1():
    assert part1() == 174137457
