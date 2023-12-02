import os

def getLines(path):
    fileDir = os.path.dirname(os.path.abspath(path))
    inputFile = os.path.join(fileDir, "input.txt")
    return open(inputFile, "r").read().splitlines()
