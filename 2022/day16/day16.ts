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

function *solve(map: Map<string, Valve>, start: string, opened: Set<string>, time: number, total: number): Iterable<number> {
  const costs = dijkstra(start, map);
  const entriesToCheck = [...costs.entries()].filter(([key, _]) => !opened.has(key));
  for(const [key, value] of entriesToCheck) {
    const timeAfterKeyIsOpened = time + value + 1;
    const flow = map.get(key)!.rate * (30 - timeAfterKeyIsOpened);
    if (flow > 0) {
      if (timeAfterKeyIsOpened < 30) {
        const newOpened = new Set<string>(opened).add(key);
        yield* solve(map, key, newOpened, time + value + 1, total + flow);
      } else {
        yield total;
      }
    } else {
      yield total;
    }
  }
}

export function part1(input: string[]): number {
  const valves = input.map(parseLine);
  const map = new Map(valves.map(valve => [valve.id, valve]));
  let max = 0;
  for(const maxItem of solve(map, "AA", new Set<string>(), 0, 0)) {
    if (maxItem > max) {
      max = maxItem;
    }
  }
  return max;
}

export function part2(input: string[]): number {
  return 0;
}
