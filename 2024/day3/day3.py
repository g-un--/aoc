from utils import getLines
import re

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    pattern = r"mul\((\d+),(\d+)\)"
    sum = 0
    for line in lines:
        matches = re.finditer(pattern, line)
        for match in matches:
            result = int(match.group(1)) * int(match.group(2))
            sum += result
    return sum

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    patternDo = r"(don't\(\))|(do\(\))"
    pattern = r"mul\((\d+),(\d+)\)"
    sum = 0
    indexes = []
    start = 0
    for index, line in enumerate(lines):
        if index > 0:
            start += len(lines[index-1])
        matchesDo = re.finditer(patternDo, line)
        for match in matchesDo: 
            if match.group(0) == "don't()":
                indexes.append((start + match.start(), 0))
            else:
                indexes.append((start + match.start(), 1))
    
    indexes.sort(reverse=True)
    start = 0
    for index, line in enumerate(lines):
        if index > 0:
            start += len(lines[index-1])
        matchedMul = re.finditer(pattern, line)        
        for match in matchedMul:
            active = next((active for index, active in indexes if match.start() + start > index), 1)
            if active:
                result = int(match.group(1)) * int(match.group(2))
                sum += result
    return sum

def test_part1_example():
    result = part1("example.txt")
    assert result == 161

def test_part1():
    result = part1()
    assert result == 189600467

def test_part2_example():
    result = part2("example2.txt")
    assert result == 48

def test_part2():
    result = part2()
    assert result == 107069718
