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

function getTotalFacesExposed(input: string[]) {
  const cubesSet = new Set<string>(input.map(line => line.replace("\r", "")));
  const cubes = input.map(parseCube);
  const exposed = new Set<string>();
  let total = 0;
  for (const cube of cubes) {
    for (const face of getFaces(cube)) {
      const key = getKey(face);
      if (!cubesSet.has(key)) {
        exposed.add(key);
        total += 1;
      }
    }
  }
  return { total, cubesSet, cubes, exposed };
}

function isConnectedToExterior(
  cube: Cube,
  cubeInLimits: (cube: Cube) => boolean,
  cubesSet: Set<string>,
  connectedToExterior: Set<string>
) {
  const visited = new Set(getKey(cube));
  const queue = [cube];

  while (queue.length > 0) {
    const next = queue.shift()!;
    if (connectedToExterior.has(getKey(next)) || !cubeInLimits(next)) {
      visited.forEach(key => connectedToExterior.add(key));
      return true;
    }
    const newNeighbors = [...getFaces(next)].filter(cube => {
      const key = getKey(cube);
      return !cubesSet.has(key) && !visited.has(key);
    });
    newNeighbors.forEach(neighbor => {
      visited.add(getKey(neighbor));
      queue.push(neighbor);
    });
  }

  return false;
}


export function part1(input: string[]): number {
  return getTotalFacesExposed(input).total;
}

export function part2(input: string[]): number {
  const { total, cubesSet, cubes, exposed } = getTotalFacesExposed(input);

  const maxX = Math.max(...cubes.map(cube => cube.x));
  const minX = Math.min(...cubes.map(cube => cube.x));
  const maxY = Math.max(...cubes.map(cube => cube.y));
  const minY = Math.min(...cubes.map(cube => cube.y));
  const maxZ = Math.max(...cubes.map(cube => cube.z));
  const minZ = Math.min(...cubes.map(cube => cube.z));

  const cubeInLimits = ({ x, y, z }: Cube) => {
    return minX <= x && x <= maxX && minY <= y && y <= maxY && minZ <= z && z <= maxZ;
  };

  const connectedToExterior = new Set<string>();
  let totalTrapped = 0;
  for (const exposedFace of exposed) {
    const exposedCube = parseCube(exposedFace);
    if (!isConnectedToExterior(exposedCube, cubeInLimits, cubesSet, connectedToExterior)) {
      for (const face of getFaces(exposedCube)) {
        if (cubesSet.has(getKey(face))) {
          totalTrapped += 1;
        }
      }
    }
  }

  return total - totalTrapped;
}
