type FoundPosition = { found: boolean, position: number | undefined }

function getMarkerPosition(input: string, distinctChars: number): FoundPosition {
  for (var i = distinctChars; i < input.length; i++) {
    const set = new Set([...input.slice(i - distinctChars, i)])
    if (set.size === distinctChars) {
      return { found: true, position: i };
    }
  }
  return { found: false, position: undefined };
}

export function part1(input: string): FoundPosition {
  return getMarkerPosition(input, 4);
}

export function part2(input: string): FoundPosition {
  return getMarkerPosition(input, 14);
}
