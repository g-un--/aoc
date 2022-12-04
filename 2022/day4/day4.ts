type Section = { start: number, end: number }

function contains(section: Section, otherSection: Section) {
  return section.start <= otherSection.start && section.end >= otherSection.end;
}

function overlap(section: Section, otherSection: Section) {
  return section.end >= otherSection.start && section.start <= otherSection.end;
}

function parseSection(pair: string): Section {
  var parts = pair.split("-");
  return {
    start: Number.parseInt(parts[0], 10),
    end: Number.parseInt(parts[1], 10)
  };
}

function parsePairs(input: string): Section[] {
  var pairs = input.split(",");
  return [
    parseSection(pairs[0]),
    parseSection(pairs[1])
  ]
}

export function part1(input: string[]): number {
  return input.reduce((sum, input) => {
    if (input === "")
      return sum;

    var pairs = parsePairs(input);
    var firstContainsSecond = contains(pairs[0], pairs[1]);
    var secondContainsFirst = contains(pairs[1], pairs[0]);
    var pairsContainsEachOther = firstContainsSecond || secondContainsFirst;
    return sum + (pairsContainsEachOther ? 1 : 0);
  }, 0);
}

export function part2(input: string[]): number {
  return input.reduce((sum, input) => {
    if (input === "")
      return sum;

    var pairs = parsePairs(input);
    var pairsContainsEachOther = overlap(pairs[0], pairs[1]);
    return sum + (pairsContainsEachOther ? 1 : 0);
  }, 0);
}
