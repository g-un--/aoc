from utils import getLines
import re
from math import lcm
from functools import partial
import copy

def parseModule(line):
    parts = re.split("->", line)
    name = parts[0].strip()
    connections = list(map(lambda x: x.strip(), parts[1].split(",")))
    if "broadcaster" in name:
        return ("broadcaster", name, connections)
    elif "%" in name:
        return ("flip-flop", name.lstrip("%"), connections)
    elif "&" in name:
        return ("conjuction", name.lstrip("&"), connections)
    
def receivePulse(modulesDict, pulse, name, fromName, condition):
    queue = []
    low, high = 0, 0
    queue.append((pulse, name, fromName))
    
    while queue:
        receivedPulse, receiverName, senderName = queue.pop(0)
        #print(senderName, receivedPulse, receiverName)
        if receivedPulse:
            high += 1
        else:
            low += 1
        
        if not receiverName in modulesDict:
            continue
        
        moduleType, moduleName, connections, state = modulesDict[receiverName]
        match moduleType:
            case "broadcaster":
                for connection in connections:
                    queue.append((receivedPulse, connection, moduleName))
            case "flip-flop":
                flipFlopState = state.get("flip-flip", False)
                if receivedPulse == False:
                    newState = not flipFlopState
                    state["flip-flip"] = newState
                    for connection in connections:
                        queue.append((newState, connection, moduleName))
            case "conjuction":
                state[senderName] = receivedPulse
                allActive = all(state.values())
                newPulse = not allActive
                if condition(moduleName, newPulse):
                    return True
                for connection in connections:
                    queue.append((newPulse, connection, moduleName))
    
    return low,high

def buildModules():
    lines = getLines(__file__)
    modules = list(map(parseModule, lines))
    modulesDict = {}
    for module in modules:
        moduleType, moduleName, connections = module
        state = {}
        modulesDict[moduleName] = (*module, state)
    for module in modules:
        moduleType, moduleName, connections = module
        for connection in connections:
            if not connection in modulesDict:
                continue
            connectionType, connectionName, _, connectionState = modulesDict[connection]
            if connectionType == "conjuction":
                connectionState[moduleName] = False
    return modulesDict

def part1():
    modulesDict = buildModules()
    totalLow, totalHigh = 0,0
    for _ in range(0, 1000):
        low, high = receivePulse(modulesDict, False, "broadcaster", "button", lambda _,__: False)
        totalLow += low
        totalHigh += high
    return totalHigh * totalLow

def findChildConjuctions(modulesDict, start):
    queue = [start]
    result = set()
    while queue:
        name = queue.pop(0)
        moduleType, moduleName, connections, state = modulesDict[name]
        if moduleType == "conjuction":
            result.add(name)
            result
            for inputName in state.keys():
                queue.append(inputName)
    result.remove(start)
    return result

def part2():
    modulesDict = buildModules()
    link = ""
    for module in modulesDict.values():
        moduleType, moduleName, connections, state = module
        if "rx" in connections:
            link = moduleName
            break 
    setOfInputs = findChildConjuctions(modulesDict, link)
    steps = []
    for input in setOfInputs:
        clonedDict = copy.deepcopy(modulesDict)
        buttonPush = 1
        while receivePulse(clonedDict, False, "broadcaster", "button", partial(lambda toFind, receiver, pulse: receiver == toFind and pulse == True, input)) != True:
            buttonPush += 1
        steps.append(buttonPush)
    return lcm(*steps)

def test_part_1():
    assert part1() == 896998430
    
def test_part_2():
    assert part2() == 236095992539963
