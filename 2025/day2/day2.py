from utils import getLines

def getIntervals(input="input.txt"):
    lines = getLines(__file__, input)
    intervals = []
    for line in lines:
        ranges = [item.split("-") for item in line.split(",")]
        for rangeItem in ranges:
            if(len(rangeItem) == 2):
                intervals.append((int(rangeItem[0]), int(rangeItem[1])))
    return intervals

def part1(input="input.txt"):
    intervals = getIntervals(input)
    sum = 0
    for (a,b) in intervals:
        for test in range(a,b+1):
            asString = str(test)
            if len(asString) % 2 == 0:
                half = len(asString) // 2
                if asString[0: half] == asString[half:len(asString)]:
                    sum += test
    return sum

def part2(input="input.txt"):
    intervals = getIntervals(input)
    sum = 0
    for (a,b) in intervals:
        for test in range(a,b+1):
            asString = str(test)
            lenAsString = len(asString)
            for part in range(1, lenAsString // 2 + 1):
                if lenAsString % part == 0:
                    computed = asString[0:part] * (lenAsString//part)
                    if computed == asString:
                        sum += test
                        break
    return sum

def test_part1_example():
    result = part1("example.txt")
    assert result == 1227775554

def test_part1():
    result = part1()
    assert result == 19128774598

def test_part2_example():
    result = part2("example.txt")
    assert result == 4174379265

def test_part2():
    result = part2()
    assert result == 21932258645