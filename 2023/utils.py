import os

def getLines(path):
    fileDir = os.path.dirname(os.path.abspath(path))
    inputFile = os.path.join(fileDir, "input.txt")
    lines = []
    with open(inputFile, "r") as file:
        file_iterator = iter(file)
        lines = list(file_iterator)
    return lines
