from utils import getLines, splitLinesIntoChunks


def isInRange(range, item):
    return range[0] <= item <= range[1]


def isIntervalOverlapping(intervalA, intervalB):
    return intervalA[0] <= intervalB[1] and intervalB[0] <= intervalA[1]


def mergeIntervals(intervalA, intervalB):
    minStart = min(intervalA[0], intervalB[0])
    maxEnd = max(intervalA[1], intervalB[1])
    return (minStart, maxEnd)


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    chunks = splitLinesIntoChunks(lines)
    ranges = []
    for line in chunks[0]:
        parts = line.split("-")
        ranges.append((int(parts[0]), int(parts[1])))
    count = 0
    for line in chunks[1]:
        ingredientId = int(line)
        isFresh = any(isInRange(range, ingredientId) for range in ranges)
        if isFresh:
            count += 1
    return count


def part2(input="input.txt"):
    lines = getLines(__file__, input)
    chunks = splitLinesIntoChunks(lines)
    ranges = []
    for line in chunks[0]:
        parts = line.split("-")
        ranges.append((int(parts[0]), int(parts[1])))
    intersectionFound = True
    while intersectionFound:
        intersectionFound = False
        for start in range(len(ranges) - 1):
            for target in range(start + 1, len(ranges)):
                rangeStart = ranges[start]
                rangeTarget = ranges[target]
                if isIntervalOverlapping(rangeStart, rangeTarget):
                    intersectionFound = True
                    mergedInterval = mergeIntervals(rangeStart, rangeTarget)
                    ranges.remove(rangeStart)
                    ranges.remove(rangeTarget)
                    ranges.append(mergedInterval)
                    break
    count = 0
    for nonOverlappingRange in ranges:
        count += nonOverlappingRange[1] - nonOverlappingRange[0] + 1
    return count


def test_part1_example():
    result = part1("example.txt")
    assert result == 3


def test_part1():
    result = part1()
    assert result == 761


def test_part2_example():
    result = part2("example.txt")
    assert result == 14


def test_part2():
    result = part2()
    assert result == 345755049374932
