function checkUp(grid: number[][], row: number, column: number) {
  let visibile = true;
  let currentRow = row - 1;
  for (; currentRow >= 0; currentRow--) {
    if (grid[currentRow][column] >= grid[row][column]) {
      visibile = false;
      break;
    }
  }
  currentRow = currentRow < 0 ? 0 : currentRow;
  return { visibile, trees: row - currentRow };
}

function checkDown(grid: number[][], row: number, column: number) {
  let visibile = true;
  let currentRow = row + 1;
  for (; currentRow < grid.length; currentRow++) {
    if (grid[currentRow][column] >= grid[row][column]) {
      visibile = false;
      break;
    }
  }
  currentRow = currentRow >= grid.length ? grid.length - 1 : currentRow;
  return { visibile, trees: (currentRow - row) };
}

function checkLeft(grid: number[][], row: number, column: number) {
  let visibile = true;
  let currentColumn = column - 1;
  for (; currentColumn >= 0; currentColumn--) {
    if (grid[row][currentColumn] >= grid[row][column]) {
      visibile = false;
      break;
    }
  }
  currentColumn = currentColumn < 0 ? 0 : currentColumn;
  return { visibile, trees: (column - currentColumn) };
}

function checkRight(grid: number[][], row: number, column: number) {
  let visibile = true;
  let currentColumn = column + 1
  for (; currentColumn <= grid.length; currentColumn++) {
    if (grid[row][currentColumn] >= grid[row][column]) {
      visibile = false;
      break;
    }
  }
  currentColumn = currentColumn >= grid.length ? grid.length - 1 : currentColumn;
  return { visibile, trees: (currentColumn - column) };
}

export function part1(input: string[]): number {
  var grid = input.filter(row => row !== "").map(item => item.replace("\r", "").split("").map(item => Number.parseInt(item, 0)));
  let total = 0;
  for (var row = 0; row < grid.length; row++) {
    for (var column = 0; column < grid.length; column++) {
      const visible =
        checkUp(grid, row, column).visibile ||
        checkDown(grid, row, column).visibile ||
        checkRight(grid, row, column).visibile ||
        checkLeft(grid, row, column).visibile;

      if (visible) {
        total += 1;
      }
    }
  }
  return total;
}

export function part2(input: string[]): number {
  var grid = input.filter(row => row !== "").map(item => item.replace("\r", "").split("").map(item => Number.parseInt(item, 0)));
  let max = -1;
  for (var row = 0; row < grid.length; row++) {
    for (var column = 0; column < grid.length; column++) {
      const scenicScore =
        checkUp(grid, row, column).trees *
        checkDown(grid, row, column).trees *
        checkRight(grid, row, column).trees *
        checkLeft(grid, row, column).trees;

      if (scenicScore > max) {
        max = scenicScore
      }
    }
  }
  return max;
}
