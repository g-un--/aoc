type Position = { row: number, column: number }

const getKey = ({ row, column }: Position) => `${row}-${column}`

type Direction = "Up" | "Down" | "Left" | "Right"

type Move = { direction: Direction, distance: number }

function* applyMove(position: Position, move: Move): Iterable<Position> {
  for (let step = 1; step <= move.distance; step++) {
    switch (move.direction) {
      case "Up": yield { row: position.row - step, column: position.column }; break;
      case "Down": yield { row: position.row + step, column: position.column }; break;
      case "Left": yield { row: position.row, column: position.column - step }; break;
      case "Right": yield { row: position.row, column: position.column + step }; break;
    }
  }
}

function distance({ row: row1, column: column1 }: Position, { row: row2, column: column2 }: Position) {
  return Math.abs(row1 - row2) + Math.abs(column1 - column2);
}

function isDiagonal(position1: Position, position2: Position) {
  return position1.column !== position2.column && position1.row !== position2.row;
}

function followDirection(position: Position, toFollow: Position): Position {
  const columnDiff = toFollow.column - position.column;
  const rowDiff = toFollow.row - position.row;
  const newColumn = columnDiff > 0 ? position.column + 1 : columnDiff < 0 ? position.column - 1 : position.column;
  const newRow = rowDiff > 0 ? position.row + 1 : rowDiff < 0 ? position.row - 1 : position.row;
  return { row: newRow, column: newColumn }
}

function parseMove(input: string): Move | undefined {
  if (input === "") return undefined;
  const parts = input.split(" ");
  const distance = Number.parseInt(parts[1].replace("\r", ""), 10);
  switch (parts[0]) {
    case "U": return { direction: "Up", distance };
    case "D": return { direction: "Down", distance };
    case "R": return { direction: "Right", distance };
    case "L": return { direction: "Left", distance };
  }
}

export function part1(input: string[]): number {
  let headPosition: Position = { row: 0, column: 0 };
  let tailPosition: Position = { row: 0, column: 0 };
  const tailPositions = new Set<string>();
  tailPositions.add(getKey(tailPosition));

  for (const line of input) {
    const move = parseMove(line);
    if (!move) {
      continue;
    }

    for (const newHeadPosition of applyMove(headPosition, move)) {
      headPosition = newHeadPosition;
      const distanceBetween = distance(headPosition, tailPosition);
      if (distanceBetween === 2 && isDiagonal(headPosition, tailPosition)) continue;
      if (distanceBetween >= 2) {
        tailPosition = followDirection(tailPosition, headPosition);
        tailPositions.add(getKey(tailPosition));
      }
    }
  }

  return tailPositions.size;
}

export function part2(input: string[]): number {
  let rope = Array.from(Array(10), (_, __) => ({ row: 0, column: 0 }));
  const tailPositions = new Set<string>();
  tailPositions.add(getKey(rope[9]));

  for (const line of input) {
    const move = parseMove(line);
    if (!move) {
      continue;
    }

    for (const newHeadPosition of applyMove(rope[0], move)) {
      rope[0] = newHeadPosition;
      for (let knot = 1; knot < 10; knot++) {
        const distanceBetween = distance(rope[knot - 1], rope[knot]);
        if (distanceBetween === 2 && isDiagonal(rope[knot - 1], rope[knot])) continue;
        if (distanceBetween >= 2) {
          rope[knot] = followDirection(rope[knot], rope[knot - 1]);
        }
      }
      tailPositions.add(getKey(rope[9]));
    }
  }

  return tailPositions.size;
}
