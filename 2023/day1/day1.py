from utils import getLines

def part1():
    numbers = []
    lines = getLines(__file__)
    for line in lines:
        line = line.strip()
        if line:  
            first_digit = next((char for char in line if char.isdigit()), None)
            last_digit = next((char for char in reversed(line) if char.isdigit()), None)
            if first_digit and last_digit:
                numbers.append(int(first_digit) * 10 + int(last_digit))
       
    return sum(numbers)

def convert(digit):
    match digit:
        case "1" | "one":
            return 1
        case "2" | "two":
            return 2
        case "3" | "three":
            return 3
        case "4" | "four":
            return 4
        case "5" | "five":
            return 5
        case "6" | "six":
            return 6
        case "7" | "seven":
            return 7
        case "8" | "eight":
            return 8
        case "9" | "nine":
            return 9
            
def part2():
    numbers = []
    digits = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "1", "2", "3", "4", "5", "6", "7", "8", "9"]
    lines = getLines(__file__)
    for line in lines:
        line = line.strip()
        if line: 
            min_index = 1000
            max_index = -1
            first_digit = None
            last_digit = None
            for digit in digits:
                lindex = line.find(digit)
                if (lindex < min_index and lindex >= 0):
                    min_index = lindex
                    first_digit = digit
                    
                rindex = line.rfind(digit)
                if (rindex > max_index and rindex >= 0):
                    max_index = rindex
                    last_digit = digit    
            if first_digit and last_digit:
                numbers.append(convert(first_digit) * 10 + convert(last_digit))
        
    return sum(numbers)

def test_part_1():
    result = part1()
    assert result == 55538
    
def test_part_2():
    result = part2()
    assert result == 54875
