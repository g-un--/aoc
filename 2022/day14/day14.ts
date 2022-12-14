type Point = { row: number, column: number }

function parseLine(input: string): Point[] {
  return input.split("->").map(item => {
    const parts = item.split(",");
    return { column: Number(parts[0]), row: Number(parts[1]) }
  });
}

function* getNearbyPoints(input: Point) {
  yield { row: input.row + 1, column: input.column - 1 };
  yield { row: input.row + 1, column: input.column + 1 };
}

function fallPoint(input: Point, grid: string[][], maxRow: number) {
  while(grid[input.row + 1][input.column] === "." && input.row < maxRow) {
    input.row += 1;
  }

  let rolledOver = false;
  for(const newPoint of getNearbyPoints(input)) {
    if (grid[newPoint.row][newPoint.column] === "." && newPoint.row <= maxRow) {
      input.row = newPoint.row;
      input.column = newPoint.column;
      rolledOver = true;
      break;
    }
  }

  if (input.row < maxRow && rolledOver)
  {
    fallPoint(input, grid, maxRow);
  }
}

function* generatePointsBetween(points: Point[]) {
  let start = points[0];
  for (const point of points.slice(1)) {
    if (point.row === start.row) {
      var minColumn = Math.min(point.column, start.column);
      var maxColumn = Math.max(point.column, start.column);
      for (let column = minColumn; column <= maxColumn; column++) {
        yield { row: point.row, column };
      }
    } else {
      var minRow = Math.min(point.row, start.row);
      var maxRow = Math.max(point.row, start.row);
      for (let row = minRow; row <= maxRow; row++) {
        yield { row, column: point.column }
      }
    }
    start = point;
  }
}

function createGrid(input: string[]) {
  const grid = Array.from(Array(1000), () => Array.from(Array(1000), () => "."));
  const points = input.map(parseLine).map(points => [...generatePointsBetween(points)]).flatMap(point => point);
  const maxRow = Math.max(...points.map(point => point.row));

  for(const point of points) {
    grid[point.row][point.column] = "#";
  }

  return { grid, maxRow };
}

export function part1(input: string[]): number {
  const { grid, maxRow } = createGrid(input);

  let sandUnits = 0;
  const startPoint = { row: 0, column: 500 };
  let shouldAddSand = true;
  while(shouldAddSand) {
    const newSandUnit = {...startPoint};
    fallPoint(newSandUnit, grid, maxRow + 1);
    if (newSandUnit.row > maxRow) {
      shouldAddSand = false;
    } else {
      grid[newSandUnit.row][newSandUnit.column] = "*";
      sandUnits += 1;
    }
  }

  return sandUnits;
}

export function part2(input: string[]): number {
  const { grid, maxRow } = createGrid(input);

  for(let column = 0; column < grid[0].length; column++) {
    grid[maxRow + 2][column] = "#";
  }

  let sandUnits = 0;
  const startPoint = { row: 0, column: 500 };
  let shouldAddSand = true;
  while(shouldAddSand) {
    const newSandUnit = {...startPoint};
    fallPoint(newSandUnit, grid, maxRow + 1);
    if (newSandUnit.row === startPoint.row && newSandUnit.column === startPoint.column) {
      shouldAddSand = false;
    } else {
      grid[newSandUnit.row][newSandUnit.column] = "*";
    }
    sandUnits += 1;
  }

  return sandUnits;
}
