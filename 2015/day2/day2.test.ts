import { part1, part2 } from './day2';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2015/day2/part1", () => {

  test('2x3x4', () => {
    expect(part1(["2x3x4"])).toBe("58");
  });

  test('1x1x10', () => {
    expect(part1(["1x1x10"])).toBe("43");
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe("1586300");
  });

});

describe("2015/day2/part2", () => {

  test('2x3x4', () => {
    expect(part2(["2x3x4"])).toBe("34");
  });

  test('1x1x10', () => {
    expect(part2(["1x1x10"])).toBe("14");
  });

  test('garbage', () => {
    expect(part2(["garbage"])).toBe("0");
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe("3737498");
  });

});
