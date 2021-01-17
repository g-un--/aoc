import {getPermutations, notEmpty, repeat, range} from '../utils';

type Connection = {from: string; to: string; distance: number}
function parseLine(line: string): Connection | null {
    const match = line.match(/(\w+)\sto\s(\w+)\s=\s(\d+)/);
    if (match != null) {
        return {from: match[1], to: match[2], distance: parseInt(match[3])};
    }
    return null;
}

type CitiesMap = {cityMap: Map<string, number>, distances: number[][]};
function getMapAndDistances(input: string[]): CitiesMap {
    const connections: Connection[] = input.map(parseLine).filter(notEmpty);
    const cityMap = new Map<string, number>();
    for(const connection of connections) {
        if(!cityMap.has(connection.from)) {
            cityMap.set(connection.from, cityMap.size);
        }
        if(!cityMap.has(connection.to)) {
            cityMap.set(connection.to, cityMap.size);
        }
    }
    const distances: number[][] = [...repeat(0, cityMap.size)].map(_ => [...repeat(0, cityMap.size)]);
    for(const connection of connections) {
        const fromIndex = cityMap.get(connection.from) as number;
        const toIndex = cityMap.get(connection.to) as number;
        distances[fromIndex][toIndex] = connection.distance;
        distances[toIndex][fromIndex] = connection.distance;
    }
    return {cityMap, distances};
}

function *getRoutesDistances(map: CitiesMap) {
    for(const permutation of getPermutations(new Set(range(0, map.cityMap.size)))) {
        let distance = 0;
        for(let index=1; index<permutation.length; index++) {
            distance += map.distances[permutation[index-1]][permutation[index]];
        }
        yield distance;
    }
}

export function part1(input: string[]): number {
    const cityMap = getMapAndDistances(input);
    let minDistance = Number.MAX_VALUE;
    for(const distance of getRoutesDistances(cityMap)) {
        if(distance < minDistance) {
            minDistance = distance;
        }
    }
    return minDistance;
}

export function part2(input: string[]): number {
    const cityMap = getMapAndDistances(input);
    let maxDistance = Number.MIN_VALUE;
    for(const distance of getRoutesDistances(cityMap)) {
        if(distance > maxDistance) {
            maxDistance = distance;
        }
    }
    return maxDistance;
}