from utils import getLines

def getJoltages(input="input.txt"):
    lines = getLines(__file__, input)
    joltages = []
    for line in lines:
        values = [int(item) for item in line]
        joltages.append(values)
    return joltages

def getMax(line, ofItems):
    itemsIndexes = []
    while len(itemsIndexes) < ofItems:
        max = -1
        indexOfMax = -1
        latest = itemsIndexes[-1] if len(itemsIndexes) > 0 else -1;
        for index, item in enumerate(line):
            if (item > max 
                and index < len(line) - ofItems + len(itemsIndexes) + 1
                and index > latest):
                max = item
                indexOfMax = index
        itemsIndexes.append(indexOfMax)
    maxValue = 0
    for index in itemsIndexes:
        maxValue = maxValue * 10 + line[index]
    return maxValue

def part1(input="input.txt"):
    lines = getJoltages(input)
    sum = 0
    for line in lines:
        max = getMax(line, 2)
        sum += max
    return sum

def part2(input="input.txt"):
    lines = getJoltages(input)
    sum = 0
    for line in lines:
        max = getMax(line, 12)
        sum += max
    return sum

def test_part1_example():
    result = part1("example.txt")
    assert result == 357

def test_part1():
    result = part1()
    assert result == 16973

def test_part2_example():
    result = part2("example.txt")
    assert result == 3121910778619

def test_part2():
    result = part2()
    assert result == 168027167146027