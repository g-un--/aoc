from utils import getLines
from collections import Counter
from functools import reduce
from operator import mul
import heapq


def getDistance(a, b):
    ax, ay, az = a
    bx, by, bz = b
    return (ax - bx) ** 2 + (ay - by) ** 2 + (az - bz) ** 2


def scanDistances(points):
    distances = []
    for index1 in range(0, len(points) - 1):
        for index2 in range(index1, len(points)):
            if index1 != index2:
                distance = getDistance(points[index1], points[index2])
                distances.append((distance, (index1, index2)))
    heapq.heapify(distances)
    return distances


def scanShortestDistance(distances):
    return heapq.heappop(distances)[1]


def findRoot(indexes, i):
    if indexes[i] == i:
        return i

    root = findRoot(indexes, indexes[i])
    indexes[i] = root
    return root


def quickUnion(indexes, pair):
    index1, index2 = pair
    index2Root = findRoot(indexes, index2)
    index1Root = findRoot(indexes, index1)
    indexes[index2Root] = index1Root
        
    for index in range(len(indexes)):
        indexes[index] = findRoot(indexes, index)

def getTop3(indexes):
    counter = Counter(indexes)
    most_common = counter.most_common(3)
    most_common_sizes = list(map(lambda x: x[1], most_common))
    return reduce(mul, most_common_sizes)


def part1(times, input="input.txt"):
    lines = [line.split(",") for line in getLines(__file__, input)]
    points = [(int(part[0]), int(part[1]), int(part[2])) for part in lines]
    distances = scanDistances(points)
    parents = [i for i in range(len(points))]
    for _ in range(times):
        pair = scanShortestDistance(distances)
        quickUnion(parents, pair)
    result = getTop3(parents)
    return result


def part2(input="input.txt"):
    lines = [line.split(",") for line in getLines(__file__, input)]
    points = [(int(part[0]), int(part[1]), int(part[2])) for part in lines]
    distances = scanDistances(points)
    allConnected = False
    result = None
    parents = [i for i in range(len(points))]
    while not allConnected:
        pair = scanShortestDistance(distances)
        quickUnion(parents, pair)
        allConnected = all(index == parents[0] for index in parents)
        if allConnected:
            print(pair)
            result = points[pair[0]][0] * points[pair[1]][0]
    return result


def test_part1_example():
    result = part1(10, "example.txt")
    assert result == 40


def test_part1():
    result = part1(1000)
    assert result == 81536


def test_part2_example():
    result = part2("example.txt")
    assert result == 25272

def test_part2():
    result = part2()
    assert result == 7017750530
