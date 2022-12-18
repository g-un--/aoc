type Cube = { x: number, y: number, z: number };

function parseCube(input: string): Cube {
  const parts = input.split(",").map(Number);
  return { x: parts[0], y: parts[1], z: parts[2] };
}

function getKey(cube: Cube) {
  return `${cube.x},${cube.y},${cube.z}`;
}

function* getFaces({ x, y, z }: Cube): Iterable<Cube> {
  yield { x: x + 1, y, z };
  yield { x: x - 1, y, z };
  yield { x, y: y - 1, z };
  yield { x, y: y + 1, z };
  yield { x, y, z: z - 1 };
  yield { x, y, z: z + 1 };
}


export function part1(input: string[]): number {
  const set = new Set<string>(input.map(line => line.replace("\r", "")));
  const cubes = input.map(parseCube);
  let total = 0;
  for (const cube of cubes) {
    for (const face of getFaces(cube)) {
      const key = getKey(face);
      if (!set.has(key)) {
        total += 1;
      }
    }
  }
  return total;
}

export function part2(input: string[]): number {
  return 0;
}
