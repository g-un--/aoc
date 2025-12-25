from utils import getLines
from itertools import product


def solve(problem):
    result, numbers = problem
    ops = len(numbers) - 1
    for codes in product(["*", "+"], repeat=ops):
        soFar = numbers[0]
        for i, code in enumerate(codes):
            if code == "*":
                soFar *= numbers[i + 1]
            elif code == "+":
                soFar += numbers[i + 1]
            if soFar > result:
                break
        if soFar == result:
            return True
    return False


def solveConcat(problem):
    result, numbers = problem
    n = len(numbers)

    def dfs(i, soFar):
        if soFar > result:
            return False
        if i == n:
            return soFar == result

        number = numbers[i]
        if dfs(i + 1, soFar + number):
            return True
        if dfs(i + 1, soFar * number):
            return True
        concat = int(str(soFar) + str(number))
        if dfs(i + 1, concat):
            return True
        return False

    return dfs(1, numbers[0])


def getProblems(input="input.txt"):
    lines = getLines(__file__, input)
    problems = []
    for line in lines:
        result, numbers = line.split(":")
        numbers = list(map(int, numbers.split()))
        result = int(result)
        problems.append((result, numbers))
    return problems


def part1(input="input.txt"):
    problems = getProblems(input)
    total = 0
    for problem in problems:
        result = solve(problem)
        if result:
            total += problem[0]
    return total


def part2(input="input.txt"):
    problems = getProblems(input)
    total = 0
    for problem in problems:
        result = solveConcat(problem)
        if result:
            total += problem[0]
    return total


def test_part1_example():
    result = part1("example.txt")
    assert result == 3749


def test_part1():
    result = part1()
    assert result == 4555081946288


def test_part2_example():
    result = part2("example.txt")
    assert result == 11387


def test_part2():
    result = part2()
    assert result == 227921760109726
