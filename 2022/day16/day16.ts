type Valve = { id: string, rate: number, leadTo: string[] }

function parseLine(input: string): Valve {
  const matches = [...input.matchAll(/Valve (.*) has flow rate=(\d+); .* valve(s?) (.*,?)+/g)][0];
  return {
    id: matches[1],
    rate: Number(matches[2]),
    leadTo: matches[4].split(",").map(item => item.trim()).filter(item => item != ""),
  }
}

function dijkstra(start: string, map: Map<string, Valve>) {
  const queue = [start];
  const costs = new Map<string, number>([...map.entries()].map(([key, _]) => [key, Number.MAX_SAFE_INTEGER]));
  costs.set(start, 0);
  while(queue.length > 0) {
    const item = queue.shift()!;
    const itemCost = costs.get(item)!;
    for(const lead of map.get(item)!.leadTo) {
      if (itemCost + 1 < costs.get(lead)!) {
        costs.set(lead, itemCost + 1);
        queue.push(lead);
      }
    }
  }
  return costs;
}

export const cache = new Map<string, Map<string, number>>();
function memoDijkstra(start: string, map: Map<string, Valve>) {
  const result = cache.get(start);
  if (!result) {
    const newResult = dijkstra(start, map);
    cache.set(start, newResult);
    return newResult;
  }
  return result;
}

function *solve(map: Map<string, Valve>, start: string, opened: Set<string>, time: number, total: number, maxTime: number): Iterable<[number, Set<string>]> {
  const costs = memoDijkstra(start, map);
  const entriesToCheck = [...costs.entries()].filter(([key, value]) => !opened.has(key) && map.get(key)!.rate > 0);
  while (entriesToCheck.length > 0) {
    const [key, value] = entriesToCheck.shift()!;
    const timeAfterKeyIsOpened = time + value + 1;
    const valve = map.get(key);
    const flow = valve!.rate * (maxTime - timeAfterKeyIsOpened);
    if (timeAfterKeyIsOpened < maxTime) {
      const newOpened = new Set<string>(opened).add(key);
      yield [total + flow, newOpened];
      yield* solve(map, key, newOpened, timeAfterKeyIsOpened, total + flow, maxTime);
    } else {
      yield [total, opened];
    }
  }
}

export function part1(input: string[]): number {
  const valves = input.map(parseLine);
  const map = new Map(valves.map(valve => [valve.id, valve]));
  let max = 0;
  for(const [maxItem, _] of solve(map, "AA", new Set<string>(), 0, 0, 30)) {
    if (maxItem > max) {
      max = maxItem;
    }
  }
  return max;
}

export function part2(input: string[]): number {
  const valves = input.map(parseLine);
  const map = new Map(valves.map(valve => [valve.id, valve]));
  const valvesWithFlow = [...map.values()].filter(item => item.rate > 0).length;
  let max = 0;
  for(const [maxItem, set] of solve(map, "AA", new Set<string>(), 0, 0, 26)) {
    if (set.size >= (valvesWithFlow / 2 - 1) && set.size <= (valvesWithFlow / 2 + 1)) {
      for(const [maxItem2, _] of solve(map, "AA", set, 0, 0, 26)) {
        if (maxItem + maxItem2 > max) {
          max = maxItem + maxItem2;
        }
      }
    }
  }
  return max;
}
