type Cranes = Map<number, string[]>

type Instruction = { numberOfCrates: number, sourceCrane: number, destinationCrane: number };

function parseCranes(input: string[]): Cranes {
  const cranes = new Map<number, string[]>();

  const craneLinesReversed = input.reverse();
  for (let crane = 1; crane <= 9; crane++) {
    const craneString = crane.toString();
    const index = craneLinesReversed[0].indexOf(craneString);

    if (index < 0) continue;
    
    const items: string[] = [];
    for (const line of craneLinesReversed.slice(1)) {
      if (index < line.length && line.charAt(index) !== " ") {
        items.push(line.charAt(index));
      }
    }
    cranes.set(crane, items);
  }

  return cranes;
}

function parseInstructions(input: string[]): Instruction[] {
  const result: Instruction[] = [];
  const regex = /move (\d+) from (\d) to (\d)/g;
  for (const line of input) {
    var matches = [...line.matchAll(regex)][0];
    result.push({
      numberOfCrates: Number.parseInt(matches[1], 10),
      sourceCrane: Number.parseInt(matches[2], 10),
      destinationCrane: Number.parseInt(matches[3], 10),
    })
  }
  return result;
}

function parseProgram(input: string[]): { cranes: Cranes, instructions: Instruction[] } {
  const cranesString: string[] = [];
  const rest: string[] = [];

  let lineBreakEncounterd = false;
  for (const line of input) {
    if (line === "\r" || line === "") {
      lineBreakEncounterd = true;
      continue;
    }

    if (lineBreakEncounterd) {
      rest.push(line);
    } else {
      cranesString.push(line);
    }
  }

  const cranes = parseCranes(cranesString);
  const instructions = parseInstructions(rest);

  return { cranes, instructions };
}

function removeCrates(cranes: Cranes, numberOfCrates: number, sourceCrane: number) {
  var crane = cranes.get(sourceCrane)!;
  let cratesMoved = [];
  while (cratesMoved.length < numberOfCrates) {
    const crate = crane.pop();
    if (crate) {
      cratesMoved.push(crate);
    }
  }
  return cratesMoved;
}

function addCrates(cranes: Cranes, destinationCrane: number, crates: string[]) {
  var targetCrane = cranes.get(destinationCrane)!;
  targetCrane.push(...crates);
}

function getItemsFromTopOfEachStack(cranes: Cranes) {
  const result: string[] = [];
  cranes.forEach((crane) => {
    if (crane.length > 0)
      result.push(crane[crane.length - 1]);
  });
  return result.join("");
}

export function part1(input: string[]): string {
  const { cranes, instructions } = parseProgram(input);
  for (const instruction of instructions) {
    const { numberOfCrates, sourceCrane, destinationCrane } = instruction;
    const crates = removeCrates(cranes, numberOfCrates, sourceCrane);
    addCrates(cranes, destinationCrane, crates);
  }
  return getItemsFromTopOfEachStack(cranes);
}

export function part2(input: string[]): string {
  const { cranes, instructions } = parseProgram(input);
  for (const instruction of instructions) {
    const { numberOfCrates, sourceCrane, destinationCrane } = instruction;
    const crates = removeCrates(cranes, numberOfCrates, sourceCrane);
    addCrates(cranes, destinationCrane, crates.reverse());
  }
  return getItemsFromTopOfEachStack(cranes);
}
