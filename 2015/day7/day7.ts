type Assign = {type:"Assign", first: string, variable: string};
type And = {type:"And", first: string, second: string, variable: string};
type Or = {type:"Or", first: string, second: string, variable: string};
type LShift = {type:"LShift", first: string, value: number, variable: string};
type RShift = {type:"RShift", first: string, value: number, variable: string};
type Not = {type:"Not", first: string, variable: string};
type Command = Assign | And | Or | LShift | RShift | Not;

function parseCommand(command: string): Command | null {
    const assignCommand = command.match(/^(\w*)\s->\s(.*)$/);
    if (assignCommand) {
        return {type: "Assign", first: assignCommand[1], variable: assignCommand[2]};
    }

    const andCommand = command.match(/(.*)\sAND\s(.*)\s->\s(.*)/);
    if (andCommand) {
        return {type:"And", first: andCommand[1], second: andCommand[2], variable: andCommand[3]};
    }

    const orCommand = command.match(/(.*)\sOR\s(.*)\s->\s(.*)/);
    if (orCommand) {
        return {type:"Or", first: orCommand[1], second: orCommand[2], variable: orCommand[3]};
    }

    const lshiftCommand = command.match(/(.*)\sLSHIFT\s(\d+)\s->\s(.*)/);
    if (lshiftCommand) {
        return {type:"LShift", first: lshiftCommand[1], value: parseInt(lshiftCommand[2]), variable: lshiftCommand[3]};
    }

    const rshiftCommand = command.match(/(.*)\sRSHIFT\s(\d+)\s->\s(.*)/);
    if (rshiftCommand) {
        return {type:"RShift", first: rshiftCommand[1], value: parseInt(rshiftCommand[2]), variable: rshiftCommand[3]};
    }

    const notCommand = command.match(/NOT\s(.*)\s->\s(.*)/);
    if (notCommand) {
        return {type: "Not", first: notCommand[1], variable: notCommand[2]};
    }

    return null;
}

function get(variable: string, variables: Map<string, number>) {
    const numericValue = parseInt(variable);
    if(numericValue || (numericValue===0)) {
        return numericValue;
    } else {
        return variables.get(variable);
    }
}

export function part1(input: string[]): Map<string, number> {
    const variables = new Map<string, number>();
    let commandQ: Command[] = [];
    for(const item of input) {
        const command = parseCommand(item);
        if(command) {
            commandQ.push(command);
        }
    }
    while(commandQ.length > 0) {
        const q: Command[] = [];
        for(const command of commandQ) {
            switch(command.type) {
                case "Assign": 
                    const value = get(command.first, variables);
                    if(value !== undefined) {
                        variables.set(command.variable, value);
                    } else {
                        q.push(command);
                    }
                    break;
                case "And":
                    const and1 = get(command.first, variables);
                    const and2 = get(command.second, variables);
                    if(and1 !== undefined && and2 !== undefined) {
                        variables.set(command.variable, and1 & and2);
                    } else {
                        q.push(command);
                    }
                    break;
                case "Or":
                    const or1 = get(command.first, variables);
                    const or2 = get(command.second, variables);
                    if(or1 !== undefined && or2 !== undefined) {
                        variables.set(command.variable, or1 | or2);
                    } else {
                        q.push(command);
                    }
                    break;
                case "LShift":
                    const valueToShiftLeft = variables.get(command.first);
                    if(valueToShiftLeft !== undefined) {
                        variables.set(command.variable, valueToShiftLeft << command.value);
                    } else {
                        q.push(command);
                    }
                    break;
                case "RShift":
                    const valueToShfitRight = variables.get(command.first);
                    if(valueToShfitRight !== undefined) {
                        variables.set(command.variable, valueToShfitRight >> command.value);
                    } else {
                        q.push(command);
                    }
                    break;
                case "Not":
                    const valueToComplement = variables.get(command.first);
                    if(valueToComplement !== undefined) {
                        variables.set(command.variable, 65535 - valueToComplement);
                    } else {
                        q.push(command);
                    }
                    break;
            }
        }
        
        if(commandQ.length === q.length) {
            console.log(commandQ.length, q.length);
            console.log(q);
            console.log(variables);
            throw "Invalid program";
        }
        commandQ = q;
    }
    return variables;
}

export function part2(input: string[]): Map<string, number> {
    let bSignalIndex = input.findIndex((command) => command.match(/->\sb$/));
    const aValue = part1(input).get("a");
    input[bSignalIndex] = `${aValue} -> b`;
    return part1(input);
}
