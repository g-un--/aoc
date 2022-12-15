type Point = { row: number, column: number }
type Pair = { sensor: Point, beacon: Point, distance: number };
type Interval = { start: number, end: number };

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

  const sensor = { row: Number(sensorY), column: Number(sensorX) };
  const beacon = { row: Number(beaconY), column: Number(beaconX) };
  const distance = getDistance(sensor, beacon);
  return { sensor, beacon, distance };
}

function getProjectionOnRow(pair: Pair, row: number): Interval | undefined {
  const rowsDistance = Math.abs(pair.sensor.row - row);
  const projectedDistance = pair.distance - rowsDistance;
  if (projectedDistance >= 0) {
    return { start: pair.sensor.column - projectedDistance, end: pair.sensor.column + projectedDistance };
  }
}

function mergeInterval(intervals: Interval[], newInterval: Interval) {
  let index = 0;
  let found = false;
  for (const interval of intervals) {
    if (interval.start <= newInterval.end && interval.end >= newInterval.start) {
      const start = Math.min(interval.start, newInterval.start);
      const end = Math.max(interval.end, newInterval.end);
      found = true;
      intervals.splice(index, 1);
      mergeInterval(intervals, { start, end });
      break;
    }
    index += 1;
  }
  if (!found) {
    intervals.push(newInterval);
  }
}

function getIntersection(interval1: Interval, interval2: Interval): Interval | undefined {
  if (interval1.start <= interval2.end && interval1.end >= interval2.start) {
    return {
      start: Math.max(interval1.start, interval2.start),
      end: Math.min(interval1.end, interval2.end)
    }
  }
}

export function part1(input: string[], targetRow: number): number {
  const pairs = input.map(parseLine);
  const intervals: Interval[] = [];
  for (const pair of pairs) {
    const projection = getProjectionOnRow(pair, targetRow);
    if (projection) {
      mergeInterval(intervals, projection);
    }
  }
  const total = intervals.reduce((sum, interval) => sum + (interval.end - interval.start + 1), 0);
  const itemsInIntervals =
    pairs
      .map(item => [item.beacon, item.sensor])
      .flatMap(item => item)
      .filter(
        item => item.row === targetRow &&
          intervals.some(interval => interval.start <= item.column && interval.end >= item.column)
      ).map(item => `${item.row}-${item.column}`);
  return total - new Set(itemsInIntervals).size;
}

export function part2(input: string[], target: number): number {
  const pairs = input.map(parseLine);
  const targetInterval = { start: 0, end: target };
  let foundPoint: Point | undefined;
  for (let row = 0; row <= target; row++) {
    const intervals: Interval[] = [];
    for (const pair of pairs) {
      const projection = getProjectionOnRow(pair, row);
      if (projection) {
        mergeInterval(intervals, projection);
      }
    }
    const total = intervals
      .map(interval => getIntersection(interval, targetInterval))
      .filter(item => item !== undefined)
      .reduce((sum, interval) => sum + (interval!.end - interval!.start + 1), 0);
    if (total < target + 1) {
      if (intervals.length > 1) {
        foundPoint = { row, column: intervals[0].end + 1 };
      } else if (intervals[0].start === 0) {
        foundPoint = { row, column: intervals[0].end + 1 };
      } else {
        foundPoint = { row, column: intervals[0].start - 1 };
      }
    }
  }

  return foundPoint!.column * 4000000 + foundPoint!.row;
}
