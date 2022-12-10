type Instr = { type: "noop" } | { type: "addx", value: number }

type ValueAfterCycles = { cycles: number, value: number };

function parseInstr(input: string): Instr | undefined {
  if (input === "") return undefined;
  const parts = input.replace("\r", "").split(' ');
  if (parts.length == 2 && parts[0] === "addx") {
    return { type: "addx", value: Number.parseInt(parts[1], 10) };
  } else {
    return { type: "noop" };
  }
}

function interpretInstr(x: number, instr: Instr): ValueAfterCycles {
  if (instr.type == "noop") {
    return { cycles: 1, value: x };
  } else {
    return { cycles: 2, value: x + instr.value };
  }
}

export function part1(input: string[]): number {
  const targetCycles = [20, 60, 100, 140, 180, 220];
  let targetIndex = 0;
  let xRegister = 1;
  let cycle = 0;
  let result = 0;

  for (const op of input) {
    const instr = parseInstr(op);
    if (!instr) continue;
    const valueAfterCycles = interpretInstr(xRegister, instr);
    cycle += valueAfterCycles.cycles;

    if (cycle >= targetCycles[targetIndex]) {
      result += xRegister * targetCycles[targetIndex];
      targetIndex += 1;
    }

    xRegister = valueAfterCycles.value;
  }
  return result;
}

export function part2(input: string[]): void {
  let xRegister = 1;
  let cycle = 0;
  const result: string[] = Array.from(Array(240), (_, __) => ".");

  for (const op of input) {
    const instr = parseInstr(op);
    if (!instr) continue;
    const valueAfterCycles = interpretInstr(xRegister, instr);
    for (let cycleStep = 1; cycleStep <= valueAfterCycles.cycles; cycleStep++) {
      const position = cycle % 40;
      if (position === xRegister || position === xRegister - 1 || position === xRegister + 1) {
        result[cycle] = "#";
      }
      cycle += 1;
    }
    xRegister = valueAfterCycles.value;
  }

  const output = `
    ${result.slice(0, 40).join(" ")}
    ${result.slice(40, 80).join(" ")}
    ${result.slice(80, 120).join(" ")}
    ${result.slice(120, 160).join(" ")}
    ${result.slice(160, 200).join(" ")}
    ${result.slice(200, 240).join(" ")}
  `;
  console.log(output);
}
