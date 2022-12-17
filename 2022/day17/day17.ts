type Shape = string[][];
type Grid = string[][];

const horizontal: Shape = [
  ["@", "@", "@", "@"]
];
const cross: Shape = [
  [".", "@", "."],
  ["@", "@", "@"],
  [".", "@", "."]
];
const reverseL: Shape = [
  [".", ".", "@"],
  [".", ".", "@"],
  ["@", "@", "@"]
];
const vertical: Shape = [
  ["@"],
  ["@"],
  ["@"],
  ["@"]
];
const square: Shape = [
  ["@", "@"],
  ["@", "@"]
]

type ShapeWithPosition = { shape: Shape, left: number, top: number }

function canMoveRight(grid: Grid, { shape, left, top }: ShapeWithPosition): boolean {
  if (left + shape[0].length === 7) return false;
  for (let shapeRow = 0; shapeRow < shape.length; shapeRow++) {
    for (let shapeColumn = 0; shapeColumn < shape[shapeRow].length; shapeColumn++) {
      if (shape[shapeRow][shapeColumn] === "@" && grid[top + shapeRow][left + shapeColumn + 1] === "#") {
        return false;
      }
    }
  }
  return true;
}

function canMoveLeft(grid: Grid, { shape, left, top }: ShapeWithPosition): boolean {
  if (left === 0) return false;
  for (let shapeRow = 0; shapeRow < shape.length; shapeRow++) {
    for (let shapeColumn = 0; shapeColumn < shape[shapeRow].length; shapeColumn++) {
      if (shape[shapeRow][shapeColumn] === "@" && grid[top + shapeRow][left + shapeColumn - 1] === "#") {
        return false;
      }
    }
  }
  return true;
}

function applyShapeToGrid(grid: Grid, { shape, left, top }: ShapeWithPosition): void {
  for (let shapeRow = 0; shapeRow < shape.length; shapeRow++) {
    for (let shapeColumn = 0; shapeColumn < shape[shapeRow].length; shapeColumn++) {
      if (shape[shapeRow][shapeColumn] === "@") {
        grid[top + shapeRow][left + shapeColumn] = "#";
      }
    }
  }
}

function shapeLanded(grid: Grid, { shape, left, top }: ShapeWithPosition) {
  const shapeHeight = shape.length;
  let bottomItemIndex = 0;

  if (top + shapeHeight === grid.length)
    return true;

  for (const bottomItem of shape[shapeHeight - 1]) {
    if (
      grid[top + shapeHeight][bottomItemIndex + left] === "#" &&
      bottomItem === "@"
    ) return true;
    bottomItemIndex += 1;
  }
  let middleItemIndex = 0;
  if (shapeHeight > 1) {
    for (const middleItem of shape[shapeHeight - 2]) {
      if (
        grid[top + shapeHeight - 1][middleItemIndex + left] === "#" &&
        middleItem === "@" &&
        shape[shapeHeight - 1][middleItemIndex] !== "@"
      ) return true;
      middleItemIndex += 1;
    }
  }
}

const shapes = [horizontal, cross, reverseL, vertical, square];

function getShape(rockNumber: number): Shape {
  return shapes[(rockNumber - 1) % shapes.length];
}

function getMove(input: string, index: number): string {
  return input[index % input.length];
}

function applyShapesToGrid(input: string, rocks: number) {
  const maxShapeHeight = Math.max(...shapes.map(shape => shape.length));
  const grid = Array.from(Array(rocks * maxShapeHeight), (_) => [".", ".", ".", ".", ".", ".", "."]);

  let highestRock = grid.length;
  let moveIndex = 0;
  for (let rockIndex = 1; rockIndex <= rocks; rockIndex++) {
    const shape = getShape(rockIndex);
    let shapeWithPosition: ShapeWithPosition = { shape: shape, left: 2, top: highestRock - 3 - shape.length }
    let shouldBePushed = true;

    while (shouldBePushed || !shapeLanded(grid, shapeWithPosition)) {
      if (shouldBePushed) {
        const move = getMove(input, moveIndex);
        moveIndex += 1;
        if (move === "<" && canMoveLeft(grid, shapeWithPosition)) {
          shapeWithPosition = { ...shapeWithPosition, left: shapeWithPosition.left - 1 };
        } else if (move === ">" && canMoveRight(grid, shapeWithPosition)) {
          shapeWithPosition = { ...shapeWithPosition, left: shapeWithPosition.left + 1 };
        }
      } else {
        shapeWithPosition = { ...shapeWithPosition, top: shapeWithPosition.top + 1 };
      }
      shouldBePushed = !shouldBePushed;
    }

    applyShapeToGrid(grid, shapeWithPosition);
    if (shapeWithPosition.top < highestRock) {
      highestRock = shapeWithPosition.top;
    }
  }
  return grid.length - highestRock;
  
}

export function part1(input: string): number {
  return applyShapesToGrid(input, 2022);
}

export function part2(input: string): number {
  return 0;
}
