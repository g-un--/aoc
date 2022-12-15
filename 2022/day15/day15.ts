import { match } from "assert";

type Point = { row: number, column: number }
type Pair = { sensor: Point, beacon: Point, distance: number };

function getDistance({ row: row1, column: column1 }: Point, { row: row2, column: column2 }: Point) {
  return Math.abs(row1 - row2) + Math.abs(column1 - column2);
}

function equal({ row: row1, column: column1 }: Point, { row: row2, column: column2 }: Point) {
  return row1 === row2 && column1 === column2;
}

function parseLine(input: string): Pair {
  const matches = [...input.matchAll(/x=(-?\d+), y=(-?\d+)/g)];
  const [_, sensorX, sensorY] = matches[0];
  const [__, beaconX, beaconY] = matches[1];

  const sensor =  { row: Number(sensorY), column: Number(sensorX) };
  const beacon =  { row: Number(beaconY), column: Number(beaconX) };
  const distance = getDistance(sensor, beacon);
  return { sensor, beacon, distance };
}

export function part1(input: string[], targetRow: number): number {
  const pairs = input.map(parseLine);
  const columns = pairs.map(pair => [pair.beacon.column - pair.distance, pair.sensor.column + pair.distance]).flatMap(item => item);
  const minColumn = Math.min(...columns);
  const maxColumn = Math.max(...columns);
  let total = 0;
  for (let column= minColumn; column<=maxColumn; column++) {
    const point = { column, row: targetRow };
    const beaconCannotBePresent = pairs.some(pair => {
      const distanceBetweenPointAndSensor = getDistance(point, pair.sensor);
      if (equal(point, pair.sensor) || equal(point, pair.beacon)) return false;
      return distanceBetweenPointAndSensor <= pair.distance;
    });
    if (beaconCannotBePresent) {
      total += 1;
    }
  }
  return total;
}

export function part2(input: string[], target: number): number {
  const pairs = input.map(parseLine);
  let foundPoint: Point | null = null;
  for (let row = 0; row <= target; row++) {
    for (let column= 0; column <= target; column++) {
      const point = { column, row };
      const beaconCanBePresent = pairs.every(pair => {
        const distanceBetweenPointAndSensor = getDistance(point, pair.sensor);
        if (equal(point, pair.sensor) || equal(point, pair.beacon)) return false;
        return distanceBetweenPointAndSensor > pair.distance;
      });
      if (beaconCanBePresent) {
        console.log(`Found point row: ${point.row}, column: ${point.column}`);
        foundPoint = point;
        break;
      }
    }
    if (foundPoint) {
      break;
    }
  }
  return foundPoint!.column * 4000000 + foundPoint!.row;
}
