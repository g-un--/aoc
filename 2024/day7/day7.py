from utils import getLines
from itertools import product

def solve(problem):
    result, numbers = problem
    ops = len(numbers) - 1
    for codes in product(["*", "+"], repeat=ops):
        soFar = numbers[0]
        for i, code in enumerate(codes):
            if code == "*":
                soFar *= numbers[i+1]
            elif code == "+":
                soFar += numbers[i+1]
        if soFar == result:
            return True
    return False

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    problems = []
    for line in lines:
        result, numbers = line.split(":")
        numbers = list(map(int, numbers.split()))
        result = int(result)
        problems.append((result, numbers))

    total = 0
    for problem in problems:
        result = solve(problem)
        if result:
            total +=  problem[0]
    return total

def test_part1_example():
    result = part1("example.txt")
    assert result == 3749

def test_part1():
    result = part1()
    assert result == 4555081946288 