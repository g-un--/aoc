import { posix } from "path";
import { stringify } from "querystring";

type Position = { row: number, column: number }

type GridSize = { rows: number, columns: number }

function* getAdjacentPositions({ row, column }: Position): Iterable<Position> {
  yield { row: row - 1, column };
  yield { row: row + 1, column };
  yield { row, column: column - 1 };
  yield { row, column: column + 1 }
}

function isValidPosition({ row, column }: Position, { rows, columns }: GridSize) {
  return row >= 0 && row < rows && column >= 0 && column < columns;
}

function getHeightCost(position1: Position, position2: Position, grid: string[][]): number {
  const char1 = grid[position1.row][position1.column];
  const char2 = grid[position2.row][position2.column];
  const char1Elevated = char1 === "S" ? "a" : char1 === "E" ? "z" : char1;
  const char2Elevated = char2 === "S" ? "a" : char2 === "E" ? "z" : char2;
  return char2Elevated.charCodeAt(0) - char1Elevated.charCodeAt(0);
}

function find(char: string, grid: string[][]): Position | undefined {
  for (let row = 0; row < grid.length; row++) {
    for (let column = 0; column < grid[row].length; column++) {
      if (grid[row][column] === char) {
        return { row, column };
      }
    }
  }
}

function getKey(position: Position) { return `${position.row}-${position.column}` }

function solve(queue: Position[], grid: string[][], costs: number[][]): Map<string, Position> {
  const paths = new Map<string, Position>();
  const debug = new Set<string>();
  const gridSize = { rows: grid.length, columns: grid[0].length };
  while (queue.length > 0) {
    const position = queue.shift()!;
    for (const adjacentPosition of getAdjacentPositions(position)) {
      if (isValidPosition(adjacentPosition, gridSize)) {
        const heightCost = getHeightCost(position, adjacentPosition, grid);
        if (heightCost > 1) continue;
        const newCost = Math.abs(heightCost) + costs[position.row][position.column] + 1;
        if (newCost < costs[adjacentPosition.row][adjacentPosition.column]) {
          costs[adjacentPosition.row][adjacentPosition.column] = newCost;
          queue.push(adjacentPosition);
          const key = getKey(adjacentPosition);
          paths.set(key, position);
          debug.add(grid[adjacentPosition.row][adjacentPosition.column]);
        }
      }
    }
  }
  return paths;
}

function getPath(paths: Map<string, Position>, from: Position, path: Position[]): Position[] {
  const key = getKey(from);
  const parent = paths.get(key);
  if (parent) {
    path.push(parent);
    return getPath(paths, parent, path);
  } else {
    return path;
  }
}

export function part1(input: string[]): number {
  const grid = input.map(line => line.replace("\r", "").split(""));
  const costs = grid.map(line => line.map(_ => 1_000_000));
  const start = find("S", grid)!;
  const end = find("E", grid)!;
  costs[start.row][start.column] = 0;
  const queue: Position[] = [start];
  const paths = solve(queue, grid, costs);
  const path = getPath(paths, end, [end]).reverse();
  return path.length - 1;
}

export function part2(input: string[]): number {
  const grid = input.map(line => line.replace("\r", "").split(""));
  const end = find("E", grid)!;
  
  let minPath: Position[] = [];
  let minPathLength = 1_000_000;
  for(let row = 0; row < grid.length; row++) {
    for(let column = 0; column < grid[0].length; column++) {
      if (grid[row][column] === "a") {
        const costs = grid.map(line => line.map(_ => 1_000_000));
        costs[row][column] = 0;
        const queue: Position[] = [{row, column}];
        const paths = solve(queue, grid, costs);
        const path = getPath(paths, end, [end]).reverse();
        if (path.length < minPathLength && path[0].row === row && path[0].column === column) {
          minPath = path;
          minPathLength = path.length;
        }
      } 
    }
  }
  
  return minPath.length - 1;
}
