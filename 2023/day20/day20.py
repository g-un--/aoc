from utils import getLines
import re

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
    
def receivePulse(modulesDict, pulse, name, fromName):
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
                for connection in connections:
                    queue.append((not allActive, connection, moduleName))
    
    return low,high

def part1():
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
    totalLow, totalHigh = 0,0
    for _ in range(0, 1000):
        low, high = receivePulse(modulesDict, False, "broadcaster", "button")
        totalLow += low
        totalHigh += high
    return totalHigh * totalLow

def test_part_1():
    assert part1() == 896998430
