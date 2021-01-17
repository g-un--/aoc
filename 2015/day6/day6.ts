import { repeat } from '../utils';

function buildWorld(yCount: number, xCount: number): number[][] {
    const world = [];
    for(let index = 0; index < yCount; index++) {
        world.push(Array.from(repeat(0, xCount)));
    }
    return world;
}

function parseCommand(commandWithParams: string) {
    const match = commandWithParams.match(/(turn on|turn off|toggle)\s(\d+),(\d+)\sthrough\s(\d+),(\d+)/);

    if (match != null) {
        const command = match[1];
        const topLeftY = parseInt(match[2]);
        const topLeftX = parseInt(match[3]);
        const bottomRightY = parseInt(match[4]);
        const bottomRightX = parseInt(match[5]); 
        return {command, topLeftY, topLeftX, bottomRightY, bottomRightX };
    }
    
    return null;
}

type Handler = (array: number[][], y: number, x: number) => void;

function commandHandlerPart1(command: string): Handler {
    let func: Handler = (array, y, x) => {};
    switch(command) {
        case "turn on":
            func = (array, y, x) => array[y][x] = 1;
            break;
        case "turn off":
            func = (array, y, x) => array[y][x] = 0;
            break;
        case "toggle":
            func = (array, y, x) => array[y][x] = array[y][x] ? 0 : 1;
            break;
    }
    return func;
}

function commandHandlerPart2(command: string): Handler {
    let func: Handler = (array, y, x) => {};
    switch(command) {
        case "turn on":
            func = (array, y, x) => array[y][x] += 1;
            break;
        case "turn off":
            func = (array, y, x) => array[y][x] = array[y][x] - 1 >= 0 ? array[y][x] - 1 : 0;
            break;
        case "toggle":
            func = (array, y, x) => array[y][x] += 2;
            break;
    }
    return func;
}

function solve(instructions: string[], yCount: number, xCount: number, commandHandler: (input: string) => Handler) : string {
    const world = buildWorld(yCount, xCount);
    for(const commandWithParams of instructions) {
        const result = parseCommand(commandWithParams);
        if (result) {
            const {command, topLeftY, topLeftX, bottomRightY, bottomRightX} = result;
            const handler = commandHandler(command);
            for(let y=topLeftY; y<=bottomRightY; y++) {
                for(let x=topLeftX; x<=bottomRightX;x++) {
                    handler(world, y, x);
                }
            }
        }
    }

    let result = 0;
    for(let y=0;y<yCount;y++) {
        for(let x=0;x<xCount;x++) {
            if(world[y][x]) {
                result += world[y][x];
            }
        }
    }
    return result.toString();
}

export function part1(instructions: string[], yCount: number, xCount: number) : string {
    return solve(instructions, yCount, xCount, commandHandlerPart1);
}

export function part2(instructions: string[], yCount: number, xCount: number) : string {
    return solve(instructions, yCount, xCount, commandHandlerPart2);
}
