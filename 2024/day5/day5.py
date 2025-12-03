from utils import getLines, splitLinesIntoChunks
from collections import defaultdict

def isValidTopo(pairs, indexes):
    for pair in pairs:
        if(pair[0] in indexes 
           and pair[1] in indexes
           and indexes[pair[0]] > indexes[pair[1]]):
            return False
    return True

def topoDFS(item, adj, stack, visited):
    visited.add(item)
    if item in adj:
        for after in adj[item]:
            if after not in visited:
                topoDFS(after, adj, stack, visited)
    stack.append(item)

def topoSort(adj):
    visited = set()
    stack = []

    for item in adj.keys():
        if item not in visited:
            topoDFS(item, adj, stack, visited)

    stack.reverse()
    return stack

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    chunks = splitLinesIntoChunks(lines)

    pairs = []
    for pair in chunks[0]:
        parts = pair.split("|")
        pairs.append((int(parts[0]), int(parts[1])))

    sum = 0
    for line in chunks[1]:
        items = [int(item) for item in line.split(",")]
        indexes = {node: index for index, node in enumerate(items)}
        if isValidTopo(pairs, indexes):
            middle = items[len(items) // 2]
            sum += middle
    return sum

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    chunks = splitLinesIntoChunks(lines)

    adj = defaultdict(list)
    pairs = []
    for pair in chunks[0]:
        parts = pair.split("|")
        pairs.append((int(parts[0]), int(parts[1])))
        adj[int(parts[0])].append(int(parts[1]))

    sum = 0
    for line in chunks[1]:
        items = [int(item) for item in line.split(",")]
        indexes = {node: index for index, node in enumerate(items)}
        if isValidTopo(pairs, indexes):
            continue

        local_adj = defaultdict(list)
        for a, b in pairs:
            if a in indexes and b in indexes:
                local_adj[a].append(b)

        topoSorted = topoSort(local_adj)
        middle = topoSorted[len(topoSorted) // 2]
        sum += middle
    return sum

def test_part1_example():
    result = part1("example.txt")
    assert result == 143

def test_part1():
    result = part1()
    assert result == 7074

def test_part2_example():
    result = part2("example.txt")
    assert result == 123

def test_part2():
    result = part2()
    assert result == 4828
        