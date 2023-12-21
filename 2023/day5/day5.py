from utils import getLines, splitLinesIntoChunks
import sys
import portion as P

def getSeeds(line):
    seeds = line.split(':')[1]
    seedNumbers = [int(n) for n in seeds.split(' ') if n]
    return seedNumbers

def getMap(lines):
    map = []
    for line in lines[1:]:
        rangeNumbers =  [int(n) for n in line.split(' ') if n]
        map.append({
            "destinationRange": P.closed(rangeNumbers[0], rangeNumbers[0] + rangeNumbers[2] - 1),
            "sourceRange": P.closed(rangeNumbers[1], rangeNumbers[1] + rangeNumbers[2] - 1),
            "rangeLength": rangeNumbers[2]
        })
    return map

def getDestination(number, map):
    for range in map:
        if number in range["sourceRange"]:
            diff = number - range["sourceRange"].lower
            return range["destinationRange"].lower + diff
    return number

def getDestinationIntervals(numberRange, map):
    intersections = P.empty()
    destinations = P.empty()
    for interval in numberRange:
        for range in map:
            intersection = interval & range["sourceRange"]
            if not intersection.empty:
                intersections = intersections | intersection
                diffLower = intersection.lower - range["sourceRange"].lower
                rangeLength = intersection.upper - intersection.lower + 1
                destinationStart = range["destinationRange"].lower + diffLower
                destinationRange = P.closed(destinationStart, destinationStart + rangeLength - 1)
                destinations = destinations | destinationRange
            
    oneToOne = numberRange - intersections
    return destinations | oneToOne

def splitListIntoChunksOfSize(numbers, chunkSize):      
    for i in range(0, len(numbers), chunkSize):  
        yield numbers[i:i + chunkSize] 
    
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
    seedsChunks = list(splitListIntoChunksOfSize(seeds, 2))
    listToInterval = lambda x: P.closed(x[0], x[0] + x[1] - 1)
    seedsIntervals = list(map(listToInterval, seedsChunks))
    chunks = splitLinesIntoChunks(lines[2:])
    maps = list(map(getMap, chunks))
    min = sys.maxsize
    for seedInterval in seedsIntervals:
        currentInterval = seedInterval
        for currentMap in maps:
            currentInterval = getDestinationIntervals(currentInterval, currentMap)
        if currentInterval.lower < min:
            min = currentInterval.lower
    return min

def test_part_1():
    assert part1() == 174137457
    
def test_part_2():
    assert part2() == 1493866
