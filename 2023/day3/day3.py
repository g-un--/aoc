from utils import getLines

def isSymbol(i, j, lines, rows, columns, number, gearsWithNumbers):
    if not (0<=i<rows and 0<=j<columns):
        return False
    
    valueToCheck = lines[i][j]
    result = valueToCheck != "." and not valueToCheck.isdigit()
    if result and valueToCheck == "*":
        listOfNumbers = gearsWithNumbers.get((i,j), [])
        listOfNumbers.append(number)
        gearsWithNumbers[(i,j)] = listOfNumbers
    return result

def part1(gearsWithNumbers):
    lines = getLines(__file__)
    rows = len(lines)
    columns = len(lines[0])
    sum = 0
    checkSymbol = lambda i,j,number: isSymbol(i, j, lines, rows, columns, number, gearsWithNumbers)
    for i, line in enumerate(lines):
        start = 0
        j = 0
        while j < columns:
            start = j
            numberAsString = ""
            while j < columns and line[j].isdigit():
                numberAsString += line[j]
                j += 1
                
            if numberAsString == "":
                j += 1
                continue
            
            number = int(numberAsString)
            
            if checkSymbol(i, start-1, number) or checkSymbol(i, j, number):    
                sum += number
                j += 1
                continue
            
            for column in range(start - 1, j + 1):
                if checkSymbol(i-1, column, number) or checkSymbol(i+1, column, number):
                    sum += number
                    break
    return sum

def part2(gearsWithNumbers):
    part1(gearsWithNumbers)
    result = 0
    for numbers in gearsWithNumbers.values():
        print(numbers)
        if len(numbers) == 2:
            result += (numbers[0] * numbers[1])
    return result

def test_part_1():
    assert part1({}) == 533784

def test_part_2():
    assert part2({}) == 78826761
